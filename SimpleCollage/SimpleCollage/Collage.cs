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
        public CollageImage[] CollageImages { get; set; }
        public CollageTemplateImage Template { get; set; }
        public CollageImage[,] CollageLayout { get; set; }

        public Collage(string templateImageLocation, string imagesLocation)
        {
            ImageFileIO io = new ImageFileIO();

            Image i = io.ImageFromFile(templateImageLocation);
            CollageTemplateImage c = new CollageTemplateImage(i);
            c.GenerateTemplateValues();

            IEnumerable<string> fileNames = Directory.EnumerateFiles(imagesLocation);
            Image[] images = new Image[fileNames.Count()];
            
            for (int j = 0; j < fileNames.Count(); ++j)
            {
                images[j] = io.ImageFromFile(fileNames.ElementAt(j));
            }

            CollageImages = new CollageImage[fileNames.Count()];
            for (int j = 0; j < images.Count(); ++j)
            {
                CollageImage c = new CollageImage(images[j]);
                CollageImages[j] = c;
                Console.WriteLine(c.AvgRGB);
            }
        }

        /// <summary>
        /// This method will take the list of images and the template image and create a 2d array of CollageImages, the collage
        /// </summary>
        public void GenerateCollage()
        {
            CollageLayout = new CollageImage[Template.TemplateValues.GetLength(0), Template.TemplateValues.GetLength(1)];

            for (int row = 0; row < Template.TemplateImage.Height; ++row)
            {
                for (int col = 0; col < Template.TemplateImage.Width; ++col)
                {

                }
            }
        }
    }
}
