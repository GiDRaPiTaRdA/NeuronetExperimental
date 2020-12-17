using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using NeuralCore;
using NeuralCore.NeuronManagment;
using OfficeOpenXml;

namespace NeuralDemo
{
    public class NeuroDemo
    {
        private const string path = @"Memory\snapshot.nnms";

        public NeuroNet NeuroNet { get; }
        private readonly Action<object> outAction;

        private static readonly Dictionary<double[], double[]> answers = new Dictionary<double[], double[]>
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
            string s = @"data\Термистор 100кОм.xlsx";

            //string[,] content = this.ReadData();

            DataTable a = this.ReadExcel(s);

            this.NeuroNet = new NeuroNet(dimentions);

            this.outAction = outAction ?? Console.WriteLine;
        }

        public DataTable ReadExcel(string fileName)
        {
            //string connectionString = Path.GetExtension(fileName).CompareTo(".xls") == 0 ? 
            //    $@"provider=Microsoft.Jet.OLEDB.4.0;Data Source={fileName};Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';" : 
            //    $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={fileName};Extended Properties='Excel 12.0;HDR=NO';";

            //OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);

            //DataSet ds = new DataSet();

            //adapter.Fill(ds, "anyNameHere");

            //DataTable data = ds.Tables["anyNameHere"];

            //return data;

            using (ExcelPackage package = new ExcelPackage(new FileInfo("MyWorkbook.xlsx")))
            {
                ExcelWorksheet firstSheet = package.Workbook.Worksheets["First Sheet"];

                firstSheet.Cells.End.Column;
                firstSheet.Cells.End.Row;

                firstSheet.
            }
        }

        public NeuroNet DemoStart()
        {
            this.outAction("Demo starting...");

            //outAction(NeuroNet.ForwardPropagation(1, 0, 1)[0] > 0.5 ? true : false);

            while (true)
            {
                Help();

                string command = Console.ReadLine();

                switch (command)
                {
                    case "train":
                        this.Train(10000);
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
                    case "answers":
                        this.ShowAnswers();
                        break;
                    case "exit":
                        return this.NeuroNet;
                    default:
                        this.outAction("Invalid command");
                        break;

                }
            }

            void Help()
            {
                this.outAction("Commands: train test save load ask answers exit");
            }
        }

        public void Train(int iterations)
        {
            this.outAction("[Train]");
            this.NeuroNet.Train(answers, iterations, this.outAction);
        }

        public void Test()
        {
            this.outAction("[Test]");
            this.outAction("output errors");
            this.NeuroNet.Test(answers, this.outAction);
        }

        public void Ask()
        {
            this.outAction("[Ask]");

            int count = this.NeuroNet.TakeMemorySnapshot().Layers[0].Count;
            int i = 0;

            double[] inputs = new double[count];

            while (i < count)
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

            double[] ansver = this.NeuroNet.Ask(inputs);

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

                this.outAction($"Saving... {path}");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                NeuroMemory.SaveSnapshot(this.NeuroNet.TakeMemorySnapshot(), path);
                this.outAction("Saved");
            }
            catch (Exception e)
            {
                this.outAction("Save failed");
                this.outAction(e.Message);
            }
        }

        public void Load(string pathArg = path)
        {
            try
            {
                this.outAction($"Loading... {pathArg}");
                this.NeuroNet.LoadMemorySnapshot(NeuroMemory.LoadSnapshot(pathArg));
                this.outAction("Loaded");
            }
            catch (Exception e)
            {
                this.outAction("Load failed");
                this.outAction(e.Message);
            }
        }

        public void ShowAnswers()
        {
            for (int j = 0; j < answers.Count; j++)
            {
                string inputsAndAnswers = answers.ElementAt(j).Key.Aggregate("inputs ", (current, d) => current + $"[{d}] ") + answers.ElementAt(j).Value.Aggregate("outputs ", (current, d) => current + $"[{d}] ");

                this.outAction(inputsAndAnswers);
            }
        }
    }

}
