using System.Drawing;
using System.Linq;

namespace ColorSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            var colors = new[] {
                new { n = "hostile", z=Color.Red },
                new { n = "unfriendly", z=Color.Orange },
                new { n = "friendly", z=Color.FromArgb(255, 82, 255, 255)},
                new { n = "unknown", z=Color.Yellow},
                new { n = "neutral", z=Color.LightGreen},
            };
            
            var orig_filename = args[0];
            var bmp = new Bitmap(orig_filename);
            var pixels = from x in Enumerable.Range(0, bmp.Width - 1)
                         from y in Enumerable.Range(0, bmp.Height - 1)
                         select new { z = bmp.GetPixel(x, y), x, y };
            foreach (var co in colors)
            {
                foreach (var p in pixels)
                    bmp.SetPixel(p.x, p.y, Color.FromArgb(p.z.A, co.z.R, co.z.G, co.z.B)); // note using original alpha
                string filename = orig_filename.Replace(".png", $"_{co.n}.png");
                bmp.Save(filename);
            }

        }
    }
}
