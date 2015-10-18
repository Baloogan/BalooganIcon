using BalooganIcon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ColorSplitter
{
    class ColorSplitter
    {
        static void Main(string[] args)
        {
            Configuration conf = Configuration.Load();
            if (args.Length == 0)
            {
                Console.WriteLine("ColorSplitter.exe by Baloogan (baloogan@gmail.com)");
                Console.WriteLine("DRAG AND DROP IMAGES ON TO THIS .EXE!");
                Console.WriteLine("Source code: https://github.com/Baloogan/BalooganIcon");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            foreach (var img_filename in args)
            {
                var img = new Bitmap(img_filename);
                var pixels = from x in Enumerable.Range(0, img.Width - 1)
                             from y in Enumerable.Range(0, img.Height - 1)
                             select new { z = img.GetPixel(x, y), x, y };
                foreach (var co in conf.colors)
                {
                    foreach (var p in pixels)
                        img.SetPixel(p.x, p.y, Color.FromArgb(p.z.A, co.color.R, co.color.G, co.color.B)); // note using original alpha
                    string filename = img_filename.Replace(".png", $"_{co.name}.png");
                    img.Save(filename);
                }
            }
        }
    }
}
