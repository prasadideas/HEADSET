using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace Simulator
{
    public partial class Form1 : Form
    {

        private IMqttClient _mqttClient;
        private void InitMqtt()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Handle incoming messages
            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                //string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment.ToArray());


                AppendLog($"[RECV] Topic: {e.ApplicationMessage.Topic}\r\nMessage: {payload}");
                await Task.CompletedTask;
            };
            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string topic = e.ApplicationMessage.Topic;
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment.ToArray());

                if (topic == "music/group1")
                {
                    AppendLog("music/group1 received: " + payload);
                    label1.Text = payload;
                }
                else if (topic == "music/group2")
                {
                    AppendLog("music/group2 received: " + payload);
                    label2.Text = payload;
                }
                else if (topic == "music/group3")
                {
                    AppendLog("music/group3 received: " + payload);
                    label3.Text = payload;
                }


                await Task.CompletedTask;
            };
        }
        public Form1()
        {
            InitializeComponent();
            InitMqtt();
        }

        private async void connectBtn_Click(object sender, EventArgs e)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.5", 1883)
                .WithClientId("WinFormsClient-" + Guid.NewGuid())
                .WithCleanSession()
                .Build();

            try
            {
                await _mqttClient.ConnectAsync(options);
                AppendLog("Connected to MQTT broker ✔");


                string topic = "music/group1";

                await _mqttClient.SubscribeAsync(topic, MqttQualityOfServiceLevel.AtLeastOnce);
                AppendLog($"Subscribed to: {topic}");

                topic = "music/group2";

                await _mqttClient.SubscribeAsync(topic, MqttQualityOfServiceLevel.AtLeastOnce);
                AppendLog($"Subscribed to: {topic}");

                topic = "music/group3";

                await _mqttClient.SubscribeAsync(topic, MqttQualityOfServiceLevel.AtLeastOnce);
                AppendLog($"Subscribed to: {topic}");


            }
            catch (Exception ex)
            {
                AppendLog("Connection Failed: " + ex.Message);
            }
        }
        private void AppendLog(string text)
        {
            //if (txtLog.InvokeRequired)
           // {
           //     txtLog.Invoke(new Action<string>(AppendLog), text);
           // }
           // else
           // {
           //     txtLog.AppendText(text + "\r\n");
           // }
           textBox1.AppendText(text + "\r\n");
        }

        private async void publishToServer(string topic, string message)
        {
           

            var msg = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            await _mqttClient.PublishAsync(msg);
            AppendLog($"Published → Topic: {topic} | {message}");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            publishToServer("status/maindoor","01");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            publishToServer("status/maindoor", "02");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            publishToServer("status/maindoor", "03");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "01");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "02");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "03");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "04");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "05");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "06");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "07");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "08");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "09");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "10");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "11");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "12");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "13");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "14");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "15");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "16");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "17");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "18");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "19");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            publishToServer("status/eachgate", "20");
        }
    }
}
