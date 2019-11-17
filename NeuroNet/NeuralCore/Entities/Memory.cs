using System;
using System.Collections.Generic;

namespace NeuralCore.Entities
{
    [Serializable]
    public class Memory
    {
        public Memory(int stuctureLength) :this(new List<Neuron>[stuctureLength]){}

        public Memory(List<Neuron>[] layers)
        {
            this.Layers = layers; 
        }

        public List<Neuron>[] Layers { get; set; }
    }
}