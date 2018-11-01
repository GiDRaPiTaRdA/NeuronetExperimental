using System;

namespace NeuralCore.NeuronManagment.Entities
{
    [Serializable]
    public class Synapse
    {
        private static readonly Random random = new Random();

         public Neuron Next { get; set; }
         public Neuron Previous { get; set; }

         public double Weight { get; set; }

        public Synapse()
        {
            this.Weight = (double)random.Next(-10, 10)/10;
        }

         //public Weight WeightArray { get; set; }
    }
}