using SimpleCollage.Controllers;
using SimpleCollage.Enums;
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
        //private int Image_Scale = (int)ImageSize.Medium;
        private ImageSize ImageSize { get; set; }
        private CollageSize CollageSize { get; set; }
        public CollageImage[] CollageImages { get; set; }
        public CollageTemplateImage Template { get; set; }
        public CollageImage[,] CollageLayout { get; set; }  
        public string[] ImagePaths { get; set; }

        public Collage(string templateImageLocation, string imagesLocation, ImageSize scale = ImageSize.Medium, CollageSize collageSize = CollageSize.Medium)
        {
            ImageSize = scale;
            CollageSize = collageSize;
            Image i = ImageFileIO.ImageFromFile(templateImageLocation);
            //Template = new CollageTemplateImage(i, templateScale);
            Template = new CollageTemplateImage(i, ImageSize, CollageSize);
            
            string[] fileNames = ImageFileIO.GetAllImagesFromFolder(imagesLocation);
            ImagePaths = fileNames.ToArray();
            //Image[] images = new Image[fileNames.Count()];

            //for (int j = 0; j < fileNames.Count(); ++j)
            //{
            //    images[j] = ImageFileIO.ImageFromFile(fileNames.ElementAt(j));
            //}

            CollageImages = new CollageImage[fileNames.Count()];
            Console.WriteLine("Calculating average image values...");
            for (int j = 0; j < fileNames.Count(); ++j)
            {
                //CollageImage ci = new CollageImage(images[j], Image_Scale);
                CollageImage ci = new CollageImage(fileNames[j], (int)ImageSize);
                CollageImages[j] = ci;
                Console.WriteLine("avg value: " + ci.ColorValues.AvgRGB + "   Count = " + j);
            }
            Console.WriteLine("Done with image values");
            Array.Sort(CollageImages);
        }

        /// <summary>
        /// This method will take the list of images and the template image and create a 2d array of CollageImages, the collage, using gray scale
        /// </summary>
        public void GenerateCollageLayout(bool useColor)
        {
            Console.WriteLine("Generating Collage...");
            CollageLayout = new CollageImage[Template.TemplateValues.GetLength(0), Template.TemplateValues.GetLength(1)];

            for (int row = 0; row < Template.TemplateImage.Height; ++row)
            {
                for (int col = 0; col < Template.TemplateImage.Width; ++col)
                {
                    if (useColor)
                    {
                        ColorValue value = Template.TemplateValues[row, col];
                         CollageImage closest = GetClosestImage(value);
                        CollageLayout[row, col] = closest;
                    }
                    else
                    {
                        double value = Template.TemplateValues[row, col].AvgRGB;
                        CollageImage closest = GetClosestImage(value);
                        CollageLayout[row, col] = closest;
                    }
                }
            }
        }

        //public List<Image> GetImages(int maxNumImages = 50)
        //{
        //    List<Image> images = new List<Image>();

        //    for (int j = 0; j < CollageImages.Length && j < maxNumImages; j++)
        //    {
        //        Image i = CollageImages.ElementAt(j).CImage;
        //        images.Add(i);
        //    }

        //    return images;
        //}

        public void BuildCollage(string savePath)
        {
            Image completedCollage = ImageCombiner.Combine(CollageLayout, (int)ImageSize); // need to change for Image_Scale
            ImageFileIO.saveImageToFile(completedCollage, savePath);
        }

        /// <summary>
        /// Gets the closest image using average rbg and the entire collageImages array
        /// </summary>
        /// <param name="templateValue"></param>
        /// <returns></returns>
        private CollageImage GetClosestImage(double templateValue)
        {
            CollageImage closest = GetClosestImage(templateValue, CollageImages);
            return closest;
        }

        /// <summary>
        /// Gets the closest image using averageRbg from a given list of CollageImages
        /// </summary>
        /// <param name="templateValue"></param>
        /// <param name="cImages"></param>
        /// <returns></returns>
        private CollageImage GetClosestImage(double templateValue, IEnumerable<CollageImage> cImages)
        {
            CollageImage closest = null;
            double range = 255;
            for (int j = 0; j < cImages.Count(); ++j)
            {
                double next = Math.Abs(cImages.ElementAt(j).ColorValues.AvgRGB - templateValue);
                if (next < range)
                {
                    range = next;
                    closest = CollageImages[j];
                }
            }
            return closest;
        }

        private CollageImage GetClosestImage(ColorValue templateValue)
        {
            CollageImage closest = null;
            double range = int.MaxValue;
            ColorName dominant = templateValue.GetDominantColor();
            double dominantValue = templateValue.GetDominantValue();
            for (int j = 0; j < CollageImages.Count(); ++j)
            {
                //double next = Math.Abs(CollageImages[j].ColorValues.GetColorValue(dominant) - dominantValue);
                double next = ColorDistance(CollageImages[j].ColorValues, templateValue);
                if (next < range)
                {
                    range = next;
                    closest = CollageImages[j];
                }
            }
            return closest;
        }

        /// <summary>
        /// Gets the distance between 2 colors by treating the colors as points in 3d
        /// Code learned from user - "BlueRaja - Danny Pflughoeft"
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        private double ColorDistance(ColorValue color1, ColorValue color2)
        {
            double rmean = (color1.Red + color2.Red) / 2;
            double r = color1.Red - color2.Red;
            double g = color1.Green - color2.Green;
            double b = color1.Blue - color2.Blue;
            double weightR = 2 + rmean / 256;
            double weightG = 4.0;
            double weightB = 2 + (255 - rmean) / 256;
            return Math.Sqrt(weightR * r * r + weightG * g * g + weightB * b * b);
        }
        private double ColorDistanceHSB(ColorValue color1, ColorValue color2)
        {
            double rmean = (color1.Hue + color2.Hue) / 2;
            double h = color1.Hue - color2.Hue;
            double s = color1.Saturation - color2.Saturation;
            double b = color1.Brightness - color2.Brightness;
            double weightR = 1;
            double weightG = 100;
            double weightB = 100;
            return Math.Sqrt(weightR * h * h + weightG * s * s + weightB * b * b);
        }
    }
}
