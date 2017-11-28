using CSC160_ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage.Controllers
{
    public class SimpleMenu
    {
        public void RunCollageMenu()
        {
            Console.WriteLine("This is no longer functional, and is unneccessary...");
            bool keepGoing = true;
            Console.WriteLine("To create a collage follow the on-screen directions. \nTo create a collage you need a template image (this is what the collage will look like) and a folder " +
                "of images \nyou may also enter a value to scale the template to make your collage larger or smaller\n");
            while (keepGoing)
            {
                string templatePath = GetTemplateImagePath();
                string imagesFolder = GetImagesLocation();
                double scale = GetScale();

                MakeCollage(templatePath, imagesFolder, scale);

                CIO.PromptForBool("To exit program enter \"exit\", to continue enter \"yes\"", "yes", "exit");
            }
        }

        public string GetTemplateImagePath()
        {
            string path = CIO.PromptForInput("Enter the filepath to the image you want to use as the collage template", false);
            return path;
        }

        public string GetImagesLocation()
        {
            string path = CIO.PromptForInput("Enter the filepath to the folder of images you want to use to make the collage", false);
            return path;
        }

        public double GetScale()
        {
            double scale = 1;
            //if (CIO.PromptForBool("Would you like to change the template image scale? (y/n)", "y", "n"))
            //{
            //    scale = CIO.PromptForDouble("Enter the new scale of the image (greater than 0 and less than 10", 0.0001, 10);
            //}

            return scale;
        }

        public void MakeCollage(string tempPath, string folderPath, double scale)
        {
            if (CIO.PromptForBool("Would you like to create a collage with the following settings?(y/n)\nTemplate Path: " + tempPath + "\nImages Folder Path: " + folderPath + "\nScale: " + scale, "y", "n"))
            {
                Console.WriteLine("This is no longer working or needed...");
                //string collageName = CIO.PromptForInput("Enter the name you want the collage saved as", false);
                //collageName = @"C:\Users\chance\Pictures\Collages\" + collageName + ".png";
                //Console.WriteLine("Your collage will be saved to " + collageName);
                //Collage testcollage = new Collage(tempPath, folderPath, scale);
                //testcollage.GenerateCollageLayout(true);
                //testcollage.BuildCollage(collageName);
            }
        }
    }
}
