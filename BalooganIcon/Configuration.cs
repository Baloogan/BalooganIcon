using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalooganIcon
{
    public class ColorSetting
    {
        public string name;
        public Color color;
    }
    public class Configuration
    {
        public List<ColorSetting> colors = new List<ColorSetting>();
        public static Configuration Load()
        {
            const string settings_filename = "BalooganIcon.ini";
            if (!File.Exists(settings_filename))
            {
                var default_conf = new Configuration();
                default_conf.colors.Add(new ColorSetting() { name = "hostile", color = Color.Red });
                default_conf.colors.Add(new ColorSetting() { name = "unfriendly", color = Color.FromArgb(255, 254, 104, 1) });
                default_conf.colors.Add(new ColorSetting() { name = "friendly", color = Color.FromArgb(255, 128, 224, 255) });
                default_conf.colors.Add(new ColorSetting() { name = "unknown", color = Color.FromArgb(255, 254, 254, 128) });
                default_conf.colors.Add(new ColorSetting() { name = "neutral", color = Color.FromArgb(255, 136, 254, 136) });
                File.WriteAllText(settings_filename, Newtonsoft.Json.JsonConvert.SerializeObject(default_conf, Newtonsoft.Json.Formatting.Indented));
                return default_conf;
            }
            else
            {
                Configuration conf = Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(settings_filename));
                return conf;
            }
        }

    }
}
