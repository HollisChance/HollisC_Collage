using SimpleCollage.Controllers;
using SimpleCollage.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageFileIO io = new ImageFileIO();
            Image i = io.ImageFromFile(@"C:\Users\chance\Documents\Visual Studio 2015\Capstone\HollisC_Collage\SimpleCollage\SimpleCollage\images\SmallSquare.bmp");
            CollageTemplateImage c = new CollageTemplateImage(i);
            c.GenerateTemplateValues();
            c.printValues();
        }
    }
}
