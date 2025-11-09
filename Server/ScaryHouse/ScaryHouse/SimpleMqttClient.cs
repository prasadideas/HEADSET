using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading;

namespace ScaryHouse
{
    internal class SimpleMqttClient : IDisposable
    {
        private TcpClient tcp;
        private NetworkStream stream;
        private CancellationTokenSource readerCts;
        private CancellationTokenSource keepAliveCts;
        private ushort nextPacketId = 1;
        private int keepAliveSeconds = 60;
        private SemaphoreSlim writeLock = new SemaphoreSlim(1, 1);
        public bool IsConnected { get; private set; }

        public event Action<string, byte[]> MessageReceived;
        public event Action<Exception> Disconnected;

        public async Task ConnectAsync(string host, int port, string clientId)
        {
            tcp = new TcpClient();
            await tcp.ConnectAsync(host, port);
            stream = tcp.GetStream();

            var protocolName = Encoding.ASCII.GetBytes("MQTT");
            var payloadClientId = Encoding.UTF8.GetBytes(clientId);

            var variableHeader = new List<byte>();
            variableHeader.AddRange(EncodeString(protocolName));
            variableHeader.Add(0x04); // protocol level
            variableHeader.Add(0x02); // connect flags: clean session
            variableHeader.Add(0); variableHeader.Add((byte)keepAliveSeconds); // keep alive

            var payload = new List<byte>();
            payload.AddRange(EncodeString(payloadClientId));

            var remaining = EncodeRemainingLength(variableHeader.Count + payload.Count);
            var packet = new List<byte>();
            packet.Add(0x10);
            packet.AddRange(remaining);
            packet.AddRange(variableHeader);
            packet.AddRange(payload);

            await WriteAsync(packet.ToArray());

            var header = new byte[4];
            int read = 0;
            while (read < 4)
            {
                int r = await stream.ReadAsync(header, read, 4 - read);
                if (r == 0) throw new Exception("Disconnected while waiting CONNACK");
                read += r;
            }

            if (header[0] != 0x20 || header[3] != 0x00)
            {
                throw new Exception($"CONNACK failed, code={header[3]}");
            }

            IsConnected = true;
            readerCts = new CancellationTokenSource();
            keepAliveCts = new CancellationTokenSource();
            _ = Task.Run(() => ReaderLoopAsync(readerCts.Token));
            _ = Task.Run(() => KeepAliveLoopAsync(keepAliveCts.Token));
        }

        public async Task SubscribeAsync(string[] topics)
        {
            if (!IsConnected) throw new InvalidOperationException("Not connected");
            ushort pid = GetNextPacketId();
            var payload = new List<byte>();
            foreach (var t in topics)
            {
                var tb = Encoding.UTF8.GetBytes(t);
                payload.AddRange(EncodeString(tb));
                payload.Add(0x00); // qos 0
            }

            var variable = new List<byte>();
            variable.Add((byte)((pid >> 8) & 0xFF));
            variable.Add((byte)(pid & 0xFF));
            variable.AddRange(payload);

            var remaining = EncodeRemainingLength(variable.Count);
            var packet = new List<byte>();
            packet.Add(0x82); // SUBSCRIBE
            packet.AddRange(remaining);
            packet.AddRange(variable);

            await WriteAsync(packet.ToArray());
        }

        private async Task ReaderLoopAsync(CancellationToken ct)
        {
            Exception error = null;
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    int b = await ReadByteAsync(ct);
                    if (b == -1) break;
                    byte header = (byte)b;

                    int mul = 1; int remaining = 0;
                    while (true)
                    {
                        int rb = await ReadByteAsync(ct);
                        if (rb == -1) throw new Exception("Unexpected EOF");
                        remaining += (rb & 127) * mul;
                        mul *= 128;
                        if ((rb & 128) == 0) break;
                    }

                    var body = new byte[remaining];
                    int read = 0;
                    while (read < remaining)
                    {
                        int rc = await stream.ReadAsync(body, read, remaining - read, ct);
                        if (rc == 0) throw new Exception("Disconnected");
                        read += rc;
                    }

                    int packetType = (header >> 4) & 0x0F;
                    if (packetType == 3)
                    {
                        int idx = 0;
                        int topicLen = (body[idx] << 8) | body[idx + 1];
                        idx += 2;
                        string topic = Encoding.UTF8.GetString(body, idx, topicLen);
                        idx += topicLen;
                        int payloadLen = remaining - idx;
                        var payload = new byte[payloadLen];
                        Buffer.BlockCopy(body, idx, payload, 0, payloadLen);
                        MessageReceived?.Invoke(topic, payload);
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                IsConnected = false;
                try { readerCts?.Cancel(); } catch { }
                try { keepAliveCts?.Cancel(); } catch { }
                Disconnected?.Invoke(error);
            }
        }

        private async Task KeepAliveLoopAsync(CancellationToken ct)
        {
            try
            {
                int interval = Math.Max(1, keepAliveSeconds / 2);
                while (!ct.IsCancellationRequested && IsConnected)
                {
                    await Task.Delay(interval * 1000, ct);
                    if (ct.IsCancellationRequested) break;
                    try { await SendPingAsync(); } catch { }
                }
            }
            catch { }
        }

        private async Task<int> ReadByteAsync(CancellationToken ct)
        {
            var buf = new byte[1];
            try
            {
                int r = await stream.ReadAsync(buf, 0, 1, ct);
                if (r == 0) return -1;
                return buf[0];
            }
            catch (OperationCanceledException) { return -1; }
        }

        private async Task SendPingAsync()
        {
            var pkt = new byte[] { 0xC0, 0x00 };
            await WriteAsync(pkt);
        }

        public async Task PublishAsync(string topic, byte[] payload)
        {
            if (!IsConnected) throw new InvalidOperationException("Not connected");
            var topicBytes = Encoding.UTF8.GetBytes(topic);
            var variable = EncodeString(topicBytes);
            var remainingLength = variable.Length + payload.Length;
            var remaining = EncodeRemainingLength(remainingLength);
            var packet = new List<byte>();
            packet.Add(0x30);
            packet.AddRange(remaining);
            packet.AddRange(variable);
            packet.AddRange(payload);
            await WriteAsync(packet.ToArray());
        }

        public async Task DisconnectAsync()
        {
            try
            {
                if (!IsConnected) return;
                var pkt = new byte[] { 0xE0, 0x00 };
                await WriteAsync(pkt);
                await stream.FlushAsync();
            }
            catch { }
            finally
            {
                IsConnected = false;
                try { readerCts?.Cancel(); } catch { }
                try { keepAliveCts?.Cancel(); } catch { }
                try { stream?.Close(); } catch { }
                try { tcp?.Close(); } catch { }
                Disconnected?.Invoke(null);
            }
        }

        private async Task WriteAsync(byte[] data)
        {
            await writeLock.WaitAsync();
            try
            {
                if (stream == null) throw new InvalidOperationException("Not connected");
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
            }
            finally { writeLock.Release(); }
        }

        private ushort GetNextPacketId()
        {
            var id = nextPacketId++;
            if (nextPacketId == 0) nextPacketId = 1;
            return id;
        }

        private static byte[] EncodeString(byte[] data)
        {
            var len = data.Length;
            var result = new byte[2 + len];
            result[0] = (byte)((len >> 8) & 0xFF);
            result[1] = (byte)(len & 0xFF);
            Buffer.BlockCopy(data, 0, result, 2, len);
            return result;
        }

        private static byte[] EncodeRemainingLength(int length)
        {
            var bytes = new List<byte>();
            int x = length;
            do
            {
                byte encoded = (byte)(x % 128);
                x = x / 128;
                if (x > 0)
                {
                    encoded = (byte)(encoded | 128);
                }
                bytes.Add(encoded);
            } while (x > 0);
            return bytes.ToArray();
        }

        public void Dispose()
        {
            try { stream?.Dispose(); } catch { }
            try { tcp?.Dispose(); } catch { }
        }
    }
}
