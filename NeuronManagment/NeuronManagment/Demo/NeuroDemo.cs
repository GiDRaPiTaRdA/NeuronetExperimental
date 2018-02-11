using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronManagment.Demo
{
    public static class NeuroDemo
    {
        public static NeuroNet DemoStart(int iterations = 10000)
        {
            Debug.WriteLine("Demo starting...");

            NeuroNet neuroNet = new NeuroNet(3, 10, 5, 1);

            Dictionary<double[], double[]> answers = new Dictionary<double[], double[]>
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

            neuroNet.Train(answers, iterations);

            neuroNet.Test(answers);

            return neuroNet;
        }
    }

}
