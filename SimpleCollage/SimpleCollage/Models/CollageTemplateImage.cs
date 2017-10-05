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
        public double[,] TemplateValues { get; set; }

        // constants 
        private const double redWeight = 0.33;
        private const double greenWeight = 0.33;
        private const double BlueWeight = 0.33;

        /// <summary>
        /// Recieves an image, generates a table of average rgb values for each pixel
        /// </summary>
        /// <param name="templateImage"></param>
        public CollageTemplateImage(Image templateImage)
        {
            TemplateImage = templateImage;
            TemplateValues = new double[templateImage.Height, templateImage.Width];
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
                        int r = tempBmp.GetPixel(col, row).R;
                        int g = tempBmp.GetPixel(col, row).G;
                        int b = tempBmp.GetPixel(col, row).B;
                        double avgValue = (r * redWeight) + (g * greenWeight) + (b * BlueWeight);
                        TemplateValues[row, col] = avgValue;
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
                    if (TemplateValues[row, col] > 100)
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
