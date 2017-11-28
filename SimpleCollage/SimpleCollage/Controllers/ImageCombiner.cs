using SimpleCollage.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage.Controllers
{
    public class ImageCombiner
    {
        /// <summary>
        /// Combines two images, placing them side by side on a new bitmap image
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns></returns>
        public static Bitmap Combine(Image image1, Image image2)
        {
            Bitmap bitmap = new Bitmap(image1.Width + image2.Width, Math.Max(image1.Height, image2.Height));
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(image1, 0, 0);
                g.DrawImage(image2, image1.Width, 0);
            }
            return bitmap;
        }

        public static Image Combine(CollageImage[,] collageLayout, int SingleImageSize)
        {
            int totalWidth = SingleImageSize * collageLayout.GetLength(1);
            int totalHeight = SingleImageSize * collageLayout.GetLength(0);
            Bitmap bitmap = new Bitmap(totalWidth, totalHeight);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int currentWidth = 0;
                int currentHeight = 0;
                Console.WriteLine("Test info");
                Console.WriteLine(collageLayout.GetLength(0));
                Console.WriteLine(collageLayout.GetLength(1));

                for (int row = 0; row < collageLayout.GetLength(0) - 1; ++row)
                {
                    for (int col = 0; col < collageLayout.GetLength(1) - 1; ++col)
                    {
                        //Image currentImage = collageLayout[row, col].GetBaseImage();
                        g.DrawImage(collageLayout[row, col].CImage, currentWidth, currentHeight);
                        //currentImage.Dispose();                     
                        currentWidth += SingleImageSize;
                    }
                    currentHeight += SingleImageSize;
                    currentWidth = 0;
                }
            }

            return bitmap;
        }
    }
}
