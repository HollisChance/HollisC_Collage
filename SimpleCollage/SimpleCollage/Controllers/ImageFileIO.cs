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
        public Image ImageFromFile(string fileName)
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

        public bool saveImageToFile(string image, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(image);
            }

            return true;
        }
    }
}
