using System;

namespace NeuralCore.NeuronManagment.Entities
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