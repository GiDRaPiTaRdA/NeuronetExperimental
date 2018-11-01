using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using NeuronManagment;
using NeuronManagment.Demo;
using VisualData;
using WeightManagment.WeightModel;
using WeightRepositoryManager;

namespace ConsoleApplication1
{
    class Program
    {
        // private static string RootPath => StaticResourse.StaticResourses.DefaultDirectoryForWeightRepository + "\\testing1.jpg";

        [STAThread]
        static void Main(string[] args)
        {
            var net = NeuroDemo.DemoStart();

            Console.WriteLine(net.ForwardPropagation(1, 0, 1)[0] > 0.5 ? true : false);

            Console.ReadKey();
        }

        //private static void ColorAnalizeRgb()
        //{
        //    using (Image img = ImageInnitDialog.InnitImage())
        //    {
        //        if (img != null)
        //        {
        //            var bitmap = ColorAnalizer.AnalizeBackWhite(img as Bitmap, 2);

        //            VisualDataStorage.Save(bitmap, RootPath);

        //            Process.Start(RootPath);
        //        }
        //    }
        //}

        //private static void ImageSimplify()
        //{
        //    using (Image img = ImageInnitDialog.InnitImage())
        //    {
        //        if (img != null)
        //        {
        //            var bitmap = PixelSimlifier.Simlify(img as Bitmap, 5);

        //            VisualDataStorage.Save(bitmap, RootPath);

        //            Process.Start(RootPath);
        //        }
        //    }

        //}

        //private static void ImageEdge()
        //{
        //    using (Image img = ImageInnitDialog.InnitImage())
        //    {
        //        if (img != null)
        //        {
        //            var bitmap = EdgeDetection.ScanDouble(img as Bitmap);
        //            VisualDataStorage.Save(bitmap, RootPath);


        //            Process.Start(RootPath);
        //        }
        //    }

        //}

        //private static void SaveWeightTest()
        //{
        //    byte[,] bytes;

        //    using (Image img = ImageInnitDialog.InnitImage())
        //    {
        //        bytes = ColorAnalizer.AnalizeByte(img as Bitmap, 2);
        //    }

        //    string path = StorageManager.SaveWeight(new Weight(bytes),"testweight");

        //    Console.WriteLine(StorageManager.LoadWeight(path).ToString());

        //    Console.ReadLine();

        //    Process.Start(path);
        //}

    }
}
