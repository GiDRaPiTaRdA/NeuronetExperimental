using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NeuralCore.NeuronManagment;
using NeuralDemo.Demo;
using NeuralMemory;


namespace ConsoleApplication1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            NeuroDemo demo = new NeuroDemo(Console.WriteLine, 3,2,1);

            if (args.Length != 0)
            {
                Console.WriteLine("Args loaded");
                Console.WriteLine(args[0]);

                demo.Load();
            }

            demo.DemoStart();
        }
    }
}
