using System;
using System.Drawing;
using StaticResourse;
using TraversalLib;

namespace ImageManager
{
    public static class ImageProcessor
    {
        /// <summary>
        /// USES FILTER TO DIVIDE IMG INTO BLACK AND WHITE 
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="filter">less - white; more - black</param>
        /// <returns></returns>
        public static byte[,] AnalizeBoolean(Image image, byte filter = byte.MaxValue/2)
        {
            Bitmap bitmap = image as Bitmap;
            byte[,] byteArray = new byte[bitmap.Width, bitmap.Height];

            Array2DTraversal.Traversal(byteArray, (@byte, x, y) =>
                {
                    //0 white 1 black
                    byteArray[x,y] = Convert.ToByte(GetBrightness(bitmap.GetPixel(x,y)) >= filter ? byte.MaxValue : byte.MinValue);
                });

            return byteArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="shadesNumber">the number of shades to use to present image[2-255]</param>
        /// <returns></returns>
        public static byte[,] AnalizeBackWhite(Image image, byte shadesNumber = byte.MaxValue)
        {
            if (shadesNumber < 2)
                throw new Exception(nameof(shadesNumber)+" must have value in range [2-255]");


            Bitmap bitmap = image as Bitmap;
            byte[,] byteArray = new byte[bitmap.Width, bitmap.Height];


            // direct conversion
            if (shadesNumber == byte.MaxValue)
            {
                Array2DTraversal.Traversal(byteArray, (@byte, x, y) =>
                {
                    byte shade = GetBrightness(bitmap.GetPixel(x, y));
                    byteArray[x, y] = shade;
                });

            }

            // shades analizing conversion
            else
            {
                float range = byte.MaxValue / (shadesNumber - 1);

                Array2DTraversal.Traversal(byteArray, (@byte, x, y) =>
                {
                    byte shade = GetShade(GetBrightness(bitmap.GetPixel(x, y)), shadesNumber);

                    byteArray[x, y] = shade;
                });

            }
            return byteArray;
        }

        /// <summary>
        /// USES DIVIDE IMG INTO colours
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="filter">less - white; more - black</param>
        /// <returns></returns>
        public static Color[,] AnalizeRGB(Image image, byte shadesNumber)
        {
            Bitmap bitmap = image as Bitmap;
            Color[,] colorArray = new Color[bitmap.Width, bitmap.Height];

            // shades analizing conversion
            float range = byte.MaxValue / (shadesNumber - 1);

            Array2DTraversal.Traversal(colorArray, (@byte, x, y) =>
            {
                byte shadeR = GetShade(bitmap.GetPixel(x, y).R, shadesNumber);
                byte shadeG = GetShade(bitmap.GetPixel(x, y).G, shadesNumber);
                byte shadeB = GetShade(bitmap.GetPixel(x, y).B, shadesNumber);

                colorArray[x, y]= Color.FromArgb(shadeR,shadeG,shadeB);
            });

            return colorArray;
        }

        public static byte GetShade(byte brightness,byte shadesNumber)
        {
            if (shadesNumber < 2)
                throw new Exception(nameof(shadesNumber) + " must have value in range [2-255]");

            float range = byte.MaxValue / (shadesNumber - 1);

            double floatingStep = brightness / range;
            int step = (int)Math.Round(floatingStep);

            byte shade;
            if (step * range <= byte.MaxValue)
                shade = Convert.ToByte(step * range);
            else
                shade = byte.MaxValue;

            return shade;
        }
        public static byte GetBrightness(Color color)
        {
            byte r = color.R;
            byte g = color.G;
            byte b = color.B;

            byte brightness = Convert.ToByte((r + g + b) / 3);

            return brightness;
        }

        public static string ToString(byte[,] bitmas)
        {
            string result = "";
            for (int x = 0; x < bitmas.GetLength(0); x++)
            {
                if (result != "")
                    result += '\t';
                for (int y = 0; y < bitmas.GetLength(1); y++)
                    result += "[" + bitmas[x, y] + "]";
            }
            result += '\n';
            return result;
        }
        public static string ToPatern(byte[,] bitmas)
        {
            string result = "";
            for (int y = 0; y < bitmas.GetLength(1); y++)
            {
                if (result != "")
                    result += '\n';
                for (int x = 0; x < bitmas.GetLength(0); x++)
                {
                    if (bitmas[x, y] > 0)
                        result += "[☻]";
                    else
                        result += "[☺]";
                }
            }
            result += '\n';
            return result;
        }
    }
}
