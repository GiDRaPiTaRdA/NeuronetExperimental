using System;
using System.Collections.Generic;
using System.IO;
using NeuralCore.NeuronManagment;
using NeuralMemory;

namespace NeuralDemo.Demo
{
    public class NeuroDemo
    {
        private const string path = @"Memory\snapshot.nnms";

        public readonly NeuroNet neuroNet;
        private readonly Action<object> outAction;

        private int iterations = 10000;
        private int reportCount = 10;

        public static readonly Dictionary<double[], double[]> answers = new Dictionary<double[], double[]>
        {
            {new double[] {0, 0, 0}, new double[] {0}},
            {new double[] {0, 0, 1}, new double[] {1}},
            {new double[] {0, 1, 0}, new double[] {0}},
            {new double[] {0, 1, 1}, new double[] {0}},
            {new double[] {1, 0, 0}, new double[] {1}},
            {new double[] {1, 0, 1}, new double[] {1}},
            {new double[] {1, 1, 0}, new double[] {0}},
            {new double[] {1, 1, 1}, new double[] {1}}
        };

        public NeuroDemo(Action<object> outAction = null, params int[] dimentions)
        {
            this.neuroNet = new NeuroNet(dimentions);

            this.outAction = outAction ?? Console.WriteLine;
        }

        public NeuroNet DemoStart()
        {
            this.outAction("Demo starting...");

            //outAction(neuroNet.ForwardPropagation(1, 0, 1)[0] > 0.5 ? true : false);

            while (true)
            {
                string command = Console.ReadLine();

                switch (command)
                {
                    case "train":
                        this.Train();
                        break;
                    case "test":
                        this.Test();
                        break;
                    case "save":
                        this.Save();
                        break;
                    case "load":
                        this.Load();
                        break;
                    case "ask":
                        this.Ask();
                        break;
                    case "exit":
                        return this.neuroNet;
                    default:
                        this.outAction("Invalid command");
                        break;

                }
            }
        }

        public void Train()
        {
            this.outAction("[Train]");
            this.neuroNet.TrainDebug(answers, this.iterations, this.outAction, this.reportCount);
        }

        public void Test()
        {
            this.outAction("[Test]");
            this.neuroNet.Test(answers, this.outAction);
        }

        public void Ask()
        {
            this.outAction("[Ask]");

            int count = 3;
            int i = 0;

            double[] inputs = new double[count];

            while (i<count)
            {
                Console.WriteLine($"Input {i} arg");
                string input = Console.ReadLine();

                bool parsed = int.TryParse(input, out int result);

                if (parsed)
                {
                    inputs[i] = result;
                    i++;
                }
                else
                {
                    Console.WriteLine($"Bad input");
                }
            }

            double[] ansver = this.neuroNet.Ask(inputs);

            Console.Write("ansver :");
            
            foreach (double a in ansver)
            {
                Console.Write($"\t{a}");
            }

            Console.WriteLine();
        }

        public void Save()
        {
            try
            {

                this.outAction("Saving...");

                Directory.CreateDirectory(Path.GetDirectoryName(path));
                NeuralMemoryManger.SaveSnapshot(this.neuroNet.TakeMemorySnapshot(), path);
                this.outAction("Saved");
            }
            catch (Exception e)
            {
                this.outAction("Save failed");
                this.outAction(e.Message);
            }


        }

        public void Load(string pathArg=path)
        {
            try
            {
                this.outAction("Loading...");
                this.neuroNet.LoadMemorySnapshot(NeuralMemoryManger.LoadSnapshot(pathArg));
                this.outAction("Loaded");
            }
            catch (Exception e)
            {
                this.outAction("Load failed");
                this.outAction(e.Message);
            }
        }
    }

}
