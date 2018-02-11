
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightManagment.WeightModel;


namespace NeuronManagment
{
    public class Neuron
    {
        public List<Synapse> Next { get; set; }

        public List<Synapse> Previous { get; set; }

        public double LastSum { get; set; }

        public double Delta { get; set; }

        public Neuron()
        {
            this.Next =  new List<Synapse>();
            this.Previous =  new List<Synapse>();
        }


        //public static bool GetAnswer(double value)
        //{
        //    return value >=0.5 ;
        //}

        //// DO NOT TOUCH
        //public static double Recognize(Neuron root)
        //{
        //    if (root.Previous != null)
        //    {
        //        foreach (Neuron neuron in root.Previous)
        //        {
        //            Recognize(neuron);
        //        }

        //        double[] sums = root.Previous.Select(n => n.LastSum).ToArray();
                

        //        //root.LastSum = (root.Weight * new Weight(sums)).GetSum();
        //        double s = 0;
        //        for (int index = 0; index < sums.Length; index++)
        //        {
        //            s += root.Weight.WeightArray[0,index] * sums[index];
        //        }


        //        root.LastSum = NeuronManager.SigmoidFunc((root.Weight * new Weight(sums)).GetSum());

        //    }

        //    return root.LastSum;

        //}


        //public static void ReverseErrorSpread(Neuron root, bool answer)
        //{
        //    int @bool = Convert.ToInt32(!answer);

        //    double errorA = @bool - root.LastFuncSum;

        //    Reverse(new [] {root}, errorA);
        //    Reverse2(root);
        //}

        //private static void Reverse(Neuron[] roots, double error = 1)
        //{
        //    if (roots[0].Previous == null)
        //        return;

        //    for (int i = 0; i < roots.Length; i++)
        //    {
        //        Neuron root = roots[i];

        //        if (root.Next == null)
        //        {
        //            root.Delta = error;
        //        }
        //        else
        //        {
        //            double sum = 0;

        //            for (int j = 0; j < root.Next.Length; j++)
        //            {
        //                var rootNext = root.Next[j];

        //                sum += rootNext.Delta * rootNext.Weight.WeightArray[0, i];
        //            }

        //            root.Delta = sum;
        //        }
        //    }

        //    Reverse(roots[0].Previous);
        //}

        //private static void Reverse2(Neuron root)
        //{
        //    double learningRate = 0.01;

        //    if (root.Previous != null)
        //    {
        //        foreach (Neuron neuron in root.Previous)
        //        {
        //            Reverse2(neuron);
        //        }
        //        // W = w + d*F'(S)*Sp*a
        //        var F = NeuronManager.SigmoidDerivativeFunc(root.LastSum);

        //        for (int i = 0; i < root.Weight.sizeY; i++)
        //        {
        //            double e;

        //            //if (root.Previous[0].Weight == null)
        //            //{
        //            //    e = root.Delta * F * root.LastSum * learningRate;
        //            //}
        //            //else
        //            //{
        //            //    e = root.Delta * F * root.LastFuncSum * learningRate;
        //            //}

        //            if (root.Previous[0].Weight == null)
        //            {
        //                e = root.Delta * F * root.Previous[i].LastSum * learningRate;
        //            }
        //            else
        //            {
        //                e = root.Delta * F * root.Previous[i].LastFuncSum * learningRate;
        //            }

        //            root.Weight.WeightArray[0, i] += e;

        //            //if(root.Previous[i].Weight!=null)
        //            //root.Previous[i].Weight.WeightArray[0, i] += e;
        //        }
        //    }

        //}
    }
}
