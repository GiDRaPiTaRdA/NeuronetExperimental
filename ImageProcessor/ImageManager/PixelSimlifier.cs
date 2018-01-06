using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor
{
    public static class PixelSimlifier
    {
        public static Bitmap Simlify(Bitmap bitmap,int simplificationSquareSide)
        {
            double sizeDX = (double)bitmap.Width / simplificationSquareSide;
            double sizeDY = (double)bitmap.Height / simplificationSquareSide;

            int sizeX =  (int)((int)sizeDX != sizeDX ? (int)sizeDX + 1:sizeDX);
            int sizeY = (int)((int)sizeDY != sizeDY ? (int)sizeDY + 1 : sizeDY);

            Bitmap bitmapResult = new Bitmap(sizeX, sizeY);

            for(int y = 0; y< bitmap.Height ;y+=simplificationSquareSide)
            {
                for (int x = 0; x < bitmap.Width; x +=simplificationSquareSide)
                {
                    Color color = GetPixelFromArea(x, y, bitmap, simplificationSquareSide);

                    bitmapResult.SetPixel(x / simplificationSquareSide, y / simplificationSquareSide, color);  
                }
            }

            return bitmapResult;
        }

        private static Color GetPixelFromArea(int x,int y, Bitmap bitmap, int simplificationSquareSide)
        {
            Color color = new Color();


            for(int iy = 0; iy < simplificationSquareSide; iy++)
            {
                for(int ix = 0; ix < simplificationSquareSide; ix++)
                {
                    int realX = ix + x;
                    int realY = iy + y;



                    if (realX < bitmap.Width && realY < bitmap.Height)
                    {
                        Color colorFromBitmap = bitmap.GetPixel(realX, realY);

                        color = MergeColors(
                            color, colorFromBitmap, (brightness1, brightness2) 
                            => (int)(brightness1 + brightness2) /2);
                    }
                }
            }

            return color;
        }

        private static Color MergeColors(Color color1,Color color2,Func<int,int,int> func)
        {
            int a = func(color1.A, color2.A);
            int r = func(color1.R, color2.R);
            int g = func(color1.G, color2.G);
            int b = func(color1.B, color2.B);

            return Color.FromArgb(a, r, g, b);
        }
    }
}
