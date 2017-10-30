using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage.Controllers
{
    public class ImageFormatter
    {
        public static Image ScaleImage(Image image, double heightScale = 1, double widthScale = 1)
        {
            Image scaled = new Bitmap(image, height: (int)(image.Height * heightScale), width: (int)(image.Width * widthScale));
            return scaled;
        }

        public static Image ScaleImage(Image image, int heightPx, int widthPx)
        {
            Image scaled = new Bitmap(image, heightPx, widthPx);
            return scaled;
        }

        public static Image ScaleAndSquare(Image image, int desiredHeight)
        {
            Image modified = SquareImage(image);
            int scale = desiredHeight;
            modified = ScaleImage(modified, scale, scale);
            return modified;
        }

        public static Image SquareImage(Image image)
        {
            Image squared = null;
            if (image.Width > image.Height)
            {
                int cropVert = (image.Width - image.Height);
                Console.WriteLine(cropVert);
                squared = CropBitmap(image as Bitmap, cropVert / 2, 0, image.Width - cropVert, image.Height);
            }
            else if (image.Width < image.Height)
            {
                int cropHorizon = image.Height - image.Width;
                squared = CropBitmap(image as Bitmap, 0, cropHorizon / 2, image.Width, image.Height - cropHorizon);
            }
            else // the image is already square
            {
                squared = image;
            }
            return squared;
        }

        private static Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Console.WriteLine("test");
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }
    }
}
