using SimpleCollage.Controllers;
using SimpleCollage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            ImageFileIO io = new ImageFileIO();
            //Image i = io.ImageFromFile(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images\needle.jpg");
            //CollageTemplateImage c = new CollageTemplateImage(i);
            //c.GenerateTemplateValues();
            //c.printValues();

            //CollageImage ci = new CollageImage(i);
            //Console.WriteLine("Average rgb: " + ci.AvgRGB);

            // read in images
            IEnumerable<string> fileNames = Directory.EnumerateFiles(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images");
            Image[] images = new Image[fileNames.Count()];
            for (int j = 0; j < fileNames.Count(); ++j)
            {
                images[j] = io.ImageFromFile(fileNames.ElementAt(j));
            }

            List<CollageImage> cImgs = new List<CollageImage>();
            for (int j = 0; j < images.Count(); ++j)
            {
                CollageImage c = new CollageImage(images[j]);
                cImgs.Add(c);
                Console.WriteLine(c.AvgRGB);
            }

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
        }
    }
}
