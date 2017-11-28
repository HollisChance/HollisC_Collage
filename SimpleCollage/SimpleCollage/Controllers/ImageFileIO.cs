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
        
        /// <summary>
        /// This solution was found here https://stackoverflow.com/questions/2953254/cgetting-all-image-files-in-folder
        /// from user: Marek Bar
        /// </summary>
        /// <param name="searchFolder"></param>
        /// <param name="filters"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        public static string[] GetAllImagesFromFolder(string searchFolder, bool isRecursive = true)
        {
            string[] filters = new string[] { "jpg", "jpeg", "png", "gif", "bmp" };
            List<string> filesFound = new List<string>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, string.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        public static void saveImageToFile(Image image, string filename)
        {
            Bitmap bmp = new Bitmap(image);
            image.Dispose();
            bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
