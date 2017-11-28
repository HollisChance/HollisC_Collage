using SimpleCollage.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SimpleCollage.Models
{
    public class CollageImage : IComparable
    {
        // Properties
        public Image CImage { get; set; }
        public ColorValue ColorValues { get; set; }
        public string ImageAddress { get; set; }
        public int ImageSize { get; set; }

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
            ColorValues = new ColorValue();
            img = ImageFormatter.SquareImage(img);
            GetAvgRGBFast(img);
        }

        public CollageImage(string fileName, int size)
        {
            ImageSize = size;
            ImageAddress = fileName;
            ColorValues = new ColorValue();
            Image img = ImageFileIO.ImageFromFile(fileName);
            Image scaled = ImageFormatter.ScaleAndSquare(img, size);
            img.Dispose();
            CImage = scaled;
            GetAvgRGBFast(scaled);
        }

        public CollageImage(Image img, int size)
        {
            ColorValues = new ColorValue();
            img = ImageFormatter.ScaleAndSquare(img, size);
            GetAvgRGBFast(img);
        }

        /// <summary>
        /// Loops over the pixels in the image to get an average rgb across every pixel in the image
        /// 
        /// -- Could be optimized
        /// learned from "stackoverflow.com/questions/1068373/how-to-calculate-the-average-rgb-color-values-of-a-bitmap" user: Loofer
        /// </summary>
        private void GetAvgRGB(Image img)
        {
            double avgR = 0;
            double avgG = 0;
            double avgB = 0;
            double avgHue = 0;
            double avgSat = 0;
            double avgVal = 0;
            int pixelCount = 0;

            if (img != null)
            {
                Bitmap bmp = new Bitmap(img);

                for (int row = 0; row < bmp.Height; ++row)
                {
                    for (int col = 0; col < bmp.Width; ++col)
                    {
                        Color pixelColor = bmp.GetPixel(col, row);
                        avgHue = pixelColor.GetHue();
                        avgSat = pixelColor.GetSaturation();
                        avgVal = pixelColor.GetBrightness();
                        avgR += pixelColor.R;
                        avgG += pixelColor.G;
                        avgB += pixelColor.B;
                        pixelCount++;
                    }
                }
            }

            ColorValues.Hue = (avgHue / pixelCount);
            ColorValues.Saturation = (avgSat / pixelCount);
            ColorValues.Brightness = (avgVal / pixelCount);
            ColorValues.Red = (avgR / pixelCount);
            ColorValues.Green = (avgG / pixelCount);
            ColorValues.Blue = (avgB / pixelCount);
            ColorValues.AvgRGB = (ColorValues.Red * redWeight) + (ColorValues.Green * greenWeight) + (ColorValues.Blue * BlueWeight);
        }

        /// <summary>
        /// Gets the average rgb of an image using a byte array of the image and unsafe code to use pointers
        /// 
        /// learned at https://stackoverflow.com/questions/1068373/how-to-calculate-the-average-rgb-color-values-of-a-bitmap from user: Philippe Leybaert
        /// </summary>
        /// <param name="img"></param>
        public void GetAvgRGBFast(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            BitmapData srcData = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb);

            int stride = srcData.Stride;

            IntPtr Scan0 = srcData.Scan0;

            long[] totals = new long[] { 0, 0, 0 };

            int width = bmp.Width;
            int height = bmp.Height;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int color = 0; color < 3; color++)
                        {
                            int idx = (y * stride) + x * 4 + color;

                            totals[color] += p[idx];
                        }
                    }
                }
            }

            ColorValues.Blue = totals[0] / (width * height);
            ColorValues.Green = totals[1] / (width * height);
            ColorValues.Red = totals[2] / (width * height);
            ColorValues.AvgRGB = (ColorValues.Red * redWeight) + (ColorValues.Green * greenWeight) + (ColorValues.Blue * BlueWeight);
        }

        public Image GetBaseImage()
        {
            Image img = ImageFileIO.ImageFromFile(ImageAddress);
            img = ImageFormatter.ScaleAndSquare(img, ImageSize);
            return img;
        }

        public int CompareTo(object obj)
        {
            int compare = 0;
            if (obj == null)
            {
                compare = 1;
            }
            CollageImage toCompare = obj as CollageImage;
            if (toCompare != null)
            {
                compare = this.ColorValues.AvgRGB.CompareTo(toCompare.ColorValues.AvgRGB);
            }
            else
            {
                throw new ArgumentException("Object is not a collage Image");
            }
            return compare;
        }
    }
}
