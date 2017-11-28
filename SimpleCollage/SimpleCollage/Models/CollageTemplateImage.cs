using SimpleCollage.Controllers;
using SimpleCollage.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage.Models
{
    public class CollageTemplateImage
    {
        // Properties
        public Image TemplateImage { get; set; }
        public ColorValue[,] TemplateValues { get; set; }

        // constants 
        private const double redWeight = 0.33;
        private const double greenWeight = 0.33;
        private const double BlueWeight = 0.33;
        private const int MaxHeightWidth = 150;

        /// <summary>
        /// Recieves an image, generates a table of average rgb values for each pixel
        /// </summary>
        /// <param name="template"></param>
        //public CollageTemplateImage(Image templateImage)
        //{
        //    TemplateImage = templateImage;
        //    TemplateValues = new double[templateImage.Height, templateImage.Width];
        //    GenerateTemplateValues();
        //}

        public CollageTemplateImage(Image template, ImageSize imageSize, CollageSize size = CollageSize.Medium)
        {
            int longSide = Math.Max(template.Height, template.Width);
            int maxPixels = (int)((double)size / (double)imageSize);
            double scaleby = ((double)maxPixels / (double)longSide);
            Image scaled = ImageFormatter.ScaleImage(template, scaleby, scaleby);
            //if (scaled.Height > MaxHeightWidth || scaled.Width > MaxHeightWidth)
            //{
            //    double scaleBy = Math.Max(scaled.Height, scaled.Width);
            //    double newScale = MaxHeightWidth / scaleBy;
            //    scaled = ImageFormatter.ScaleImage(scaled, newScale, newScale);
            //}
            TemplateImage = scaled;
            TemplateValues = new ColorValue[TemplateImage.Height, TemplateImage.Width];
            GenerateTemplateValues();
        }

        /// <summary>
        /// Generates table of avg rgb values based on the templateImage, this table can be used to create a collage
        /// </summary>
        public void GenerateTemplateValues()
        {
            if (TemplateImage != null)
            {
                Bitmap tempBmp = new Bitmap(TemplateImage);
                for (int row = 0; row < TemplateImage.Height; ++row)
                {
                    for (int col = 0; col < TemplateImage.Width; ++col)
                    {
                        ColorValue current = new ColorValue();
                        Color px = tempBmp.GetPixel(col, row);
                        current.Red = px.R;
                        current.Green = px.G;
                        current.Blue = px.B;
                        current.Hue = px.GetHue();
                        current.Saturation = px.GetSaturation();
                        current.Brightness = px.GetBrightness();
                        double avgValue = (current.Red * redWeight) + (current.Green * greenWeight) + (current.Blue * BlueWeight);
                        current.AvgRGB = avgValue;
                        TemplateValues[row, col] = current;
                    }
                }
            }
        }

        public void printValues()
        {
            for (int row = 0; row < TemplateValues.GetLength(0); ++row)
            {
                for (int col = 0; col < TemplateValues.GetLength(1); ++col)
                {
                    //Console.Write((int)TemplateValues[row, col] + " ");
                    if (TemplateValues[row, col].AvgRGB > 100)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
