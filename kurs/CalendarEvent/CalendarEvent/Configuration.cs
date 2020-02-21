using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CalendarEvent
{
    public class Configuration
    {
        public class Config
        {
            private string notificationSoundPath = "";
            private Color notificationBackgroundColor=Color.Purple;
            public string NotificationSoundPath {
                get => notificationSoundPath;
                set {
                    notificationSoundPath = value;
       
                }
            }
            public Color NotificationBackgroundColor {
                get => notificationBackgroundColor;
                set {
                    notificationBackgroundColor = value;
                    
                }
            }
        }
        public static Config config = new Config();
        public static void SaveConfig()
        {
            using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/config.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                new JsonSerializer().Serialize(writer, config);
            }
        }
        public static void LoadConfig(string path)
        {
            var serializer = new JsonSerializer();
            try
            {
                var sr = new StreamReader(path);
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    config = serializer.Deserialize<Config>(jsonTextReader);
                }
            }
            catch
            {
                SaveConfig();
                LoadConfig(path);
            }
        }

    }
}
