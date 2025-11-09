using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ScaryHouse
{
    [Serializable]
    public class RoomConfig
    {
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
                    for (int i = 0; i < 15; i++) def.RoomSeconds.Add(20); // default 20s
                    def.Save();
                    return def;
                }

                using (var fs = File.OpenRead(path))
                {
                    var xs = new XmlSerializer(typeof(RoomConfig));
                    var cfg = (RoomConfig)xs.Deserialize(fs);
                    // ensure size 15
                    if (cfg.RoomSeconds == null) cfg.RoomSeconds = new List<int>();
                    while (cfg.RoomSeconds.Count < 15) cfg.RoomSeconds.Add(20);
                    if (cfg.RoomSeconds.Count > 15) cfg.RoomSeconds = cfg.RoomSeconds.GetRange(0, 15);
                    return cfg;
                }
            }
            catch
            {
                var def = new RoomConfig();
                for (int i = 0; i < 15; i++) def.RoomSeconds.Add(20);
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
