using System;

namespace NeuralCore.Entities
{
    [Serializable]
    public class Receptor : Neuron
    {
        public Receptor(double sum)
        {
            this.LastSum = sum;
        }
    }
}