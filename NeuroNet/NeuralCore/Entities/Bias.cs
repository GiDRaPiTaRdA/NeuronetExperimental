using System;

namespace NeuralCore.Entities
{
    [Serializable]
    class Bias : Neuron
    {
        public Bias(double sum = 1)
        {
            this.LastSum = sum;
        }
    }
}
