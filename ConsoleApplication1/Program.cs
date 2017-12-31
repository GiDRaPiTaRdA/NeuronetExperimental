using ImageManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using VisualData;

namespace ConsoleApplication1
{
    class Program
    {
        private static string RootPath => StaticResourse.StaticResourses.DefaultDirectoryForWeightRepository + "\\testing1.jpg";

        [STAThread]
        static void Main(string[] args)
        {
            ImageSimplify();
        }

        private static void ColorAnalizeRGB()
        {
            Color[,] arr;
            using (Image img = ImageInnitDialog.InnitImage())
            {
                arr = ColorAnalizer.AnalizeRGB(img, 2);
            }

            VisualDataStorage.Save(arr, RootPath);

            Process.Start(RootPath);
        }

        private static void ImageSimplify()
        {
            Bitmap bitmap;
            using (Image img = ImageInnitDialog.InnitImage())
            {
                bitmap  = PixelSimlifier.Simlify(img as Bitmap, 5);
            }
            VisualDataStorage.Save(bitmap,RootPath);

            Process.Start(RootPath);
        }

        //private static void traversal()
        //{
           
        //    var array = new int[3, 3, 3]
        //    {
        //        {
        //            { 1, 2,3},
        //            { 4, 5 ,6},
        //            {7,8,9 }
        //        },
        //                         {
        //            { 10, 11,12},
        //            { 13, 14,15},
        //            {16,17,18 }
        //        },
        //                         {
        //            { 19, 20,21},
        //            { 22, 23,24},
        //            {25,26,27 }
        //        }
        //    };


        //    int i = 0;

        //    array.Traversal((element, coords) => i += (int)element));
        
        //    var enumerator = (array as Array).GetEnumerator();
        //    int x = 0;
        //    while (enumerator.MoveNext())
        //    {
        //        x += (int)enumerator.Current;
        //    }

        //}
    }
}
