using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ScaryHouse
{
    [Serializable]
    public class RoomConfig
    {
        public int NumberOfRooms { get; set; } = 15; // default
        public List<int> RoomSeconds { get; set; } = new List<int>();

        private static string GetConfigPath()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ScaryHouse");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            return Path.Combine(dir, "roomtimes.xml");
        }

        public static RoomConfig Load()
        {
            try
            {
                var path = GetConfigPath();
                if (!File.Exists(path))
                {
                    var def = new RoomConfig();
                    if (def.NumberOfRooms < 1) def.NumberOfRooms = 15;
                    if (def.NumberOfRooms > 20) def.NumberOfRooms = 20;
                    for (int i = 0; i < def.NumberOfRooms; i++) def.RoomSeconds.Add(20); // default 20s
                    def.Save();
                    return def;
                }

                using (var fs = File.OpenRead(path))
                {
                    var xs = new XmlSerializer(typeof(RoomConfig));
                    var cfg = (RoomConfig)xs.Deserialize(fs);
                    if (cfg == null) cfg = new RoomConfig();
                    // normalize NumberOfRooms
                    if (cfg.NumberOfRooms < 1) cfg.NumberOfRooms = 1;
                    if (cfg.NumberOfRooms > 20) cfg.NumberOfRooms = 20;
                    // ensure list length matches NumberOfRooms
                    if (cfg.RoomSeconds == null) cfg.RoomSeconds = new List<int>();
                    while (cfg.RoomSeconds.Count < cfg.NumberOfRooms) cfg.RoomSeconds.Add(20);
                    if (cfg.RoomSeconds.Count > 20) cfg.RoomSeconds = cfg.RoomSeconds.GetRange(0, 20);
                    // trim to NumberOfRooms
                    if (cfg.RoomSeconds.Count > cfg.NumberOfRooms) cfg.RoomSeconds = cfg.RoomSeconds.GetRange(0, cfg.NumberOfRooms);
                    return cfg;
                }
            }
            catch
            {
                var def = new RoomConfig();
                if (def.NumberOfRooms < 1) def.NumberOfRooms = 15;
                if (def.NumberOfRooms > 20) def.NumberOfRooms = 20;
                for (int i = 0; i < def.NumberOfRooms; i++) def.RoomSeconds.Add(20);
                return def;
            }
        }

        public void Save()
        {
            try
            {
                var path = GetConfigPath();
                using (var fs = File.Create(path))
                {
                    var xs = new XmlSerializer(typeof(RoomConfig));
                    xs.Serialize(fs, this);
                }
            }
            catch { }
        }
    }
}
