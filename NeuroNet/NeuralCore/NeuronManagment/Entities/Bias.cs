using System;

namespace NeuralCore.NeuronManagment.Entities
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
