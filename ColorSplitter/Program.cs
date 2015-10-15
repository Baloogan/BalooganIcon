using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ColorSplitter
{
    class ColorSetting
    {
        public string name;
        public Color color;
    }
    class Configuration
    {
        public List<ColorSetting> colors = new List<ColorSetting>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            const string settings_filename = "ColorSplitter.ini";
            if (!File.Exists(settings_filename))
            {
                var default_conf = new Configuration();
                default_conf.colors.Add(new ColorSetting() { name = "hostile", color = Color.Red });
                default_conf.colors.Add(new ColorSetting() { name = "unfriendly", color = Color.FromArgb(255, 254, 104, 1) });
                default_conf.colors.Add(new ColorSetting() { name = "friendly", color = Color.FromArgb(255, 128, 224, 255) });
                default_conf.colors.Add(new ColorSetting() { name = "unknown", color = Color.FromArgb(255, 254, 254, 128) });
                default_conf.colors.Add(new ColorSetting() { name = "neutral", color = Color.FromArgb(255, 136, 254, 136) });
                File.WriteAllText(settings_filename, Newtonsoft.Json.JsonConvert.SerializeObject(default_conf, Newtonsoft.Json.Formatting.Indented));
            }

            Configuration conf = Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(settings_filename));

            var orig_filename = args[0];
            var bmp = new Bitmap(orig_filename);
            var pixels = from x in Enumerable.Range(0, bmp.Width - 1)
                         from y in Enumerable.Range(0, bmp.Height - 1)
                         select new { z = bmp.GetPixel(x, y), x, y };
            foreach (var co in conf.colors)
            {
                foreach (var p in pixels)
                    bmp.SetPixel(p.x, p.y, Color.FromArgb(p.z.A, co.color.R, co.color.G, co.color.B)); // note using original alpha
                string filename = orig_filename.Replace(".png", $"_{co.name}.png");
                bmp.Save(filename);
            }
        }
    }
}
