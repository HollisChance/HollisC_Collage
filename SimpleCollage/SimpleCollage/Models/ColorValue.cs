using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollage.Models
{
    public class ColorValue
    {
        public double AvgRGB { get; set; }
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public double Hue { get; set; }
        public double Saturation { get; set; }
        public double Brightness { get; set; }
        private ColorName dominantColor;

        public ColorValue()
        {
            dominantColor = ColorName.EMPTY;
        }

        public ColorName GetDominantColor()
        {
            if (dominantColor == ColorName.EMPTY)
            {
                dominantColor = ColorName.GREEN;
                if (Red > Green && Red > Blue)
                {
                    dominantColor = ColorName.RED;
                }
                else if (Blue > Red && Blue > Green)
                {
                    dominantColor = ColorName.BLUE;
                }
            }
            return dominantColor;
        }
        public double GetDominantValue()
        {
            double dominant = Green;
            if (GetDominantColor() == ColorName.RED)
            {
                dominant = Red;
            }
            else if (GetDominantColor() == ColorName.BLUE)
            {
                dominant = Blue;
            }
            return dominant;
        }
        public double GetColorValue(ColorName color)
        {
            double value = Green;
            if (color == ColorName.RED)
            {
                value = Red;
            }
            else if (color == ColorName.BLUE)
            {
                value = Blue;
            }

            return value;
        }
    }
}
