using SimpleCollage.Controllers;
using SimpleCollage.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage
{
    public class Collage
    {
        private const int Image_Scale = 100;
        public CollageImage[] CollageImages { get; set; }
        public CollageTemplateImage Template { get; set; }
        public CollageImage[,] CollageLayout { get; set; }  // Maybe change this to array of ints, ids that correspond to collageImages. Might be more efficient

        public Collage(string templateImageLocation, string imagesLocation, double templateScale = 1)
        {
            Image i = ImageFileIO.ImageFromFile(templateImageLocation);
            Template = new CollageTemplateImage(i, templateScale);

            IEnumerable<string> fileNames = Directory.EnumerateFiles(imagesLocation);
            Image[] images = new Image[fileNames.Count()];
            
            for (int j = 0; j < fileNames.Count(); ++j)
            {
                images[j] = ImageFileIO.ImageFromFile(fileNames.ElementAt(j));
            }

            CollageImages = new CollageImage[fileNames.Count()];
            Console.WriteLine("Calculating average image values...");
            for (int j = 0; j < images.Count(); ++j)
            {
                CollageImage ci = new CollageImage(images[j], Image_Scale);
                CollageImages[j] = ci;
                Console.WriteLine(ci.AvgRGB);
            }
            Console.WriteLine("Done with image values");
        }

        /// <summary>
        /// This method will take the list of images and the template image and create a 2d array of CollageImages, the collage
        /// </summary>
        public void GenerateCollageLayout()
        {
            Console.WriteLine("Generating Collage...");
            CollageLayout = new CollageImage[Template.TemplateValues.GetLength(0), Template.TemplateValues.GetLength(1)];

            for (int row = 0; row < Template.TemplateImage.Height; ++row)
            {
                for (int col = 0; col < Template.TemplateImage.Width; ++col)
                {
                    double value = Template.TemplateValues[col, row];
                    CollageImage closest = GetClosestImage(value);
                    CollageLayout[col, row] = closest;
                }
            }
        }

        public void BuildCollage(string savePath)
        {
            Bitmap completedCollage = ImageCombiner.Combine(CollageLayout, Image_Scale); // need to change for Image_Scale
            ImageFileIO.saveImageToFile(completedCollage, savePath);
        }

        private CollageImage GetClosestImage(double templateValue)
        {
            CollageImage closest = null;
            double range = 255;
            for (int j = 0; j < CollageImages.Length; ++j)
            {
                double next = Math.Abs(CollageImages[j].AvgRGB - templateValue);
                if (next < range)
                {
                    range = next;
                    closest = CollageImages[j];
                }
            }
            return closest;
        }
    }
}
