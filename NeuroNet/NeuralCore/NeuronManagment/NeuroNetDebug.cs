using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NeuralCore.NeuronManagment
{
    public static class NeuroNetDebug
    {
        public static void TrainDebug(this NeuroNet neuroNet, Dictionary<double[], double[]> patterns, int iterations, Action<object> outAction = null, int showProgress = 10)
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
            outAction?.Invoke($"time : {(double)timer.ElapsedMilliseconds/1000} count : {iterations}");
        }

        public static void Test(this NeuroNet neuroNet, Dictionary<double[], double[]> patterns, Action<object> outAction)
        {
            for (int j = 0; j < patterns.Count; j++)
            {
                var a = (patterns.ElementAt(j).Key);

                outAction(neuroNet.ForwardPropagation(a)[0]);
            }
        }
    }
}