using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace SimpleCollage.Controllers
{
    public class ImageFileIO
    {
        public static Image ImageFromFile(string fileName)
        {
            Image img = null;
            try
            {
                img = Image.FromFile(fileName);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Invalid Filename or path");
                Console.WriteLine(e.Message);
            }

            return img;
        }

        public static void saveImageToFile(Image image, string filename)
        {
            image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
