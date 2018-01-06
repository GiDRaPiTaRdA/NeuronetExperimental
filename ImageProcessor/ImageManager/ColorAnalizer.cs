using System;
using System.Drawing;
using StaticResourse;
using TraversalLib;

namespace ImageProcessor
{
    public static class ColorAnalizer
    {
        public static byte[,] AnalizeByte(Bitmap bitmap, byte filter = byte.MaxValue/2)
        {
            byte[,] byteArray = new byte[bitmap.Width, bitmap.Height];

            BinaryArrayTraversal.Traversal(byteArray, (@byte, x, y) =>
                {
                    byteArray[x,y] = Convert.ToByte(GetBrightness(bitmap.GetPixel(x,y)) >= filter ? byte.MaxValue : byte.MinValue);
                });

            return byteArray;
        }

        public static bool[,] AnalizeBoolean(Bitmap bitmap, byte filter = byte.MaxValue / 2)
        {
            bool[,] byteArray = new bool[bitmap.Width, bitmap.Height];

            BinaryArrayTraversal.Traversal(byteArray, (@byte, x, y) =>
            {
                //0 white 1 black
                byteArray[x, y] = GetBrightness(bitmap.GetPixel(x, y)) >= filter;
            });

            return byteArray;
        }

        public static Bitmap AnalizeShades(Bitmap bitmap, byte shadesNumber = byte.MaxValue)
        {
            if (shadesNumber < 2)
                throw new Exception(nameof(shadesNumber) + " must have value in range [2-255]");

            if (shadesNumber == byte.MaxValue)
                return bitmap;

            Bitmap result = ImageManager.PixelTraversal(bitmap, (color, x, y) =>
            {
                byte shadeR = GetShade(color.R, shadesNumber);
                byte shadeG = GetShade(color.G, shadesNumber);
                byte shadeB = GetShade(color.B, shadesNumber);

                return Color.FromArgb(shadeR, shadeG, shadeB);
            });

            return result;
        }

        public static Bitmap AnalizeBackWhite(Bitmap bitmap,double factorR = 1, double factorG = 1, double factorB = 1)
        {
            Bitmap result = ImageManager.PixelTraversal(bitmap, (color, x, y) =>
            {
                byte briteness = GetBrightness(color, factorR, factorG, factorB);
                return Color.FromArgb(briteness,briteness,briteness);
            });

            return result;
        }

        #region Help
        private static byte GetShade(byte brightness,byte shadesNumber)
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
        private static byte GetBrightness(Color color, double factorR = 1, double factorG = 1, double factorB = 1)
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
        #endregion
    }
}
