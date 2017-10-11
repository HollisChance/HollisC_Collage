using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage.Models
{
    public class CollageImage
    {

        // Properties
        public Image CImage { get; set; }
        public double AvgRGB { get; set; }

        // constants 
        private const double redWeight = 0.33;
        private const double greenWeight = 0.33;
        private const double BlueWeight = 0.33;

        /// <summary>
        /// Recieves an image, then calculates the average rgb for the image to create a collageImage object
        /// </summary>
        /// <param name="img"></param>
        public CollageImage(Image img)
        {
            CImage = img;
            getAvgRGB();
        }

        /// <summary>
        /// Loops over the pixels in the image to get an average rgb across every pixel in the image
        /// 
        /// -- Could be optimized
        /// learned from "stackoverflow.com/questions/1068373/how-to-calculate-the-average-rgb-color-values-of-a-bitmap" user: Loofer
        /// </summary>
        private void getAvgRGB()
        {
            double avgR = 0;
            double avgG = 0;
            double avgB = 0;
            int pixelCount = 0;

            if (CImage != null)
            {
                Bitmap bmp = new Bitmap(CImage);

                for (int row = 0; row < bmp.Height; ++row)
                {
                    for (int col = 0; col < bmp.Width; ++col)
                    {
                        Color pixelColor = bmp.GetPixel(col, row);
                        avgR += pixelColor.R;
                        avgG += pixelColor.G;
                        avgB += pixelColor.B;
                        pixelCount++;
                    }
                }                
            }

            double r = avgR / pixelCount;
            double g = avgG / pixelCount;
            double b = avgB / pixelCount;

            AvgRGB = (r * redWeight) + (g * greenWeight) + (b * BlueWeight);
        }
    }
}
