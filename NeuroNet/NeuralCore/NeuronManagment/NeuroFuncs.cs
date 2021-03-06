﻿using System;

namespace NeuralCore.NeuronManagment
{
    internal static class NeuroFuncs
    {
        public static double SigmoidFunc(double x)
        {
            double result = 1/(1 + Math.Pow(Math.E, -x));
            return result;
        }
        public static double SigmoidDerivativeFunc(double x)
        {
            double result = x*(1 - x);
            return result;
        }
    }
}
