using BalooganIcon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace IconSeries
{

    class IconSeries
    {
        static void Main(string[] args)
        {
            Configuration conf = Configuration.Load();
            if (args.Length == 0)
            {
                Console.WriteLine("IconSeries.exe by Baloogan (baloogan@gmail.com)");
                Console.WriteLine("DRAG AND DROP TXT FILES ON TO THIS .EXE!");
                Console.WriteLine("Source code: https://github.com/Baloogan/BalooganIcon");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            foreach (string txt_filename in args)
            {
                string[] txt_lines = File.ReadAllLines(txt_filename);
                string img_filename = txt_lines[0];
                string type = txt_lines[1];
                var img = new Bitmap(img_filename);
                var pixels = (from x in Enumerable.Range(0, img.Width - 1)
                              from y in Enumerable.Range(0, img.Height - 1)
                              select new { z = img.GetPixel(x, y), x, y }).ToArray();
                var txt_ids = txt_lines.Skip(2);
                foreach (var id in txt_ids)
                {
                    foreach (var co in conf.colors)
                    {
                        foreach (var p in pixels)
                            img.SetPixel(p.x, p.y, Color.FromArgb(p.z.A, co.color.R, co.color.G, co.color.B)); // note using original alpha
                        string filename = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(img_filename)), $"{type}_{id}_{co.name}.png");
                        img.Save(filename);
                    }
                }
            }
        }
    }
}
