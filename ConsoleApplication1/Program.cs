using ImageManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TraversalLib;

namespace ConsoleApplication1
{
    class Program
    {
        private static string Path => StaticResourse.StaticResourses.defaultDirectoryForWeightRepository + "\\testing1.jpg";

        static void Main(string[] args)
        {
            traversal();
        }

        private static void colorAnalizeRGB()
        {
            Color[,] arr;
            using (Image img = ImageInnitDialog.InnitImage())
            {
                arr = ImageProcessor.AnalizeRGB(img, 2);
            }

            VisualData.VisualDataStorage.Save(arr, Path);

            Process.Start(Path);
        }

        private static void traversal()
        {
            int i = 0;
            var array = new int[3, 3, 3]
            {
                {
                    { 1, 2,3},
                    { 4, 5 ,6},
                    {7,8,9 }
                },
                                 {
                    { 10, 11,12},
                    { 13, 14,15},
                    {16,17,18 }
                },
                                 {
                    { 19, 20,21},
                    { 22, 23,24},
                    {25,26,27 }
                }
            };

            TRX<int> traversal = new TRX<int>(array, (args) => i++);
            traversal.Recursive();


        }
    }
}
