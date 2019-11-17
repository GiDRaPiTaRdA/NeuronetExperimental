using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NeuralCore.NeuronManagment
{
    public static class NeuroDebug
    {
        public static void Train(this NeuroNet neuroNet, Dictionary<double[], double[]> patterns, int iterations, Action<string> outAction = null)
        {
            Stopwatch timer = Stopwatch.StartNew();

            const int numberOfErrorsToShow = 10;

            for (int i = 0; i < iterations; i++)
            {
                double error = neuroNet.Train(patterns);

                if (outAction != null && (i % (int)(iterations / numberOfErrorsToShow) == 0))
                    outAction($"{error} i=[{i}]");
            }

            timer.Stop();
            outAction?.Invoke($"trained in {(double)timer.ElapsedMilliseconds / 1000} ms count : {iterations}");
        }

        public static void Test(this NeuroNet neuroNet, Dictionary<double[], double[]> patterns, Action<object> outAction)
        {
            for (int j = 0; j < patterns.Count; j++)
            {
                double[] a = patterns.ElementAt(j).Key;

                outAction(neuroNet.ForwardPropagation(a)[0]);
            }
        }

        public static double[] Ask(this NeuroNet neuroNet,double [] inputs)
        {
           return neuroNet.ForwardPropagation(inputs);
        }
    }
}