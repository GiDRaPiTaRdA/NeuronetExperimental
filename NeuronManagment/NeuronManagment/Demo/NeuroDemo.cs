using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronManagment.NetDebug;

namespace NeuronManagment.Demo
{
    public static class NeuroDemo
    {
        public static NeuroNet DemoStart(int iterations = 10000, Action<object> outAction = null)
        {
            if (outAction == null)
                outAction = Console.WriteLine;

            outAction("Demo starting...");

            NeuroNet neuroNet = new NeuroNet(3,2,1);

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

            outAction("[Train]");

            neuroNet.TrainDebug(answers, iterations, outAction, 10);

            outAction("[Test]");

            neuroNet.Test(answers, outAction);

            return neuroNet;
        }
    }

}
