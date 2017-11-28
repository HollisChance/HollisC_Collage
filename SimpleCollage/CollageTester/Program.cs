using SimpleCollage;
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

namespace CollageTester
{
    public class Program
    {
        static Stopwatch sw = new Stopwatch();
        static void Main(string[] args)
        {

            sw.Start();
            //testCombineImages();
            //testCrop();
            MakeACollage();
            //UseMenu();
            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
        }

        private static void UseMenu()
        {
            SimpleMenu menu = new SimpleMenu();
            menu.RunCollageMenu();
        }

        private static void MakeACollage()
        {
            //string template = @"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\createdImages\Squares.bmp";
            string template = @"C:\Users\chance\Pictures\MyDrawings\spaceneedle.jpg";
            string source = @"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\CollageTester\Images";
            //string source = @"D:\Pictures 1";
            Collage testcollage = new Collage(template, source, SimpleCollage.Enums.ImageSize.MedLarge, SimpleCollage.Enums.CollageSize.MedLarge);
            Console.WriteLine("Current time: " + sw.Elapsed);
            //testcollage.scaleTemplate(.5);
            testcollage.GenerateCollageLayout(true);
            testcollage.BuildCollage(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\CollageTester\createdImages\NewScalingTest3.png");
        }

        private static void test1()
        {
            Image i = ImageFileIO.ImageFromFile(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images\needle.jpg");
            //CollageTemplateImage c = new CollageTemplateImage(i);
            //c.GenerateTemplateValues();
            //c.printValues();

            CollageImage ci = new CollageImage(i);
            //Console.WriteLine("Average rgb: " + ci.AvgRGB);
        }

        private void test2()
        {
            // read in images
            IEnumerable<string> fileNames = Directory.EnumerateFiles(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images");
            Image[] images = new Image[fileNames.Count()];
            for (int j = 0; j < fileNames.Count(); ++j)
            {
                images[j] = ImageFileIO.ImageFromFile(fileNames.ElementAt(j));
            }

            List<CollageImage> cImgs = new List<CollageImage>();
            for (int j = 0; j < images.Count(); ++j)
            {
                CollageImage c = new CollageImage(images[j]);
                cImgs.Add(c);
                //Console.WriteLine(c.AvgRGB);
            }
        }

        private void test3()
        {
            Collage collage = new Collage(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images\Squares.bmp", @"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images");
            collage.GenerateCollageLayout(false);

            //foreach (CollageImage ci in collage.CollageLayout)
            //{
            //    Console.WriteLine(ci.AvgRGB);
            //}
        }

        private static void testCombineImages()
        {
            Image i1 = ImageFileIO.ImageFromFile(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images\needle.jpg");
            Image i2 = ImageFileIO.ImageFromFile(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images\BigName.bmp");
            Image Combined = ImageCombiner.Combine(i1, i2);
            ImageFileIO.saveImageToFile(Combined, @"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\createdImages\test1.png");
        }

        private static void testCrop()
        {
            Image i1 = ImageFileIO.ImageFromFile(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images\test2.bmp");
            Image cropped = ImageFormatter.SquareImage(i1);

            ImageFileIO.saveImageToFile(cropped, @"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\createdImages\test2.png");
        }
    }
}

