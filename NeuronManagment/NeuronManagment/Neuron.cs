
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeightManagment.WeightModel;


namespace NeuronManagment
{
    class Neuron
    {
        public Weight Weight { get; set; }

        const int limit = 9;
        //const int filter = 120;
        public Neuron(Weight weights)
        {
            this.Weight = weights;
        }

        private bool ObtainAnswer(int sum)
        {
            return sum >= limit;
        }

        public bool Recognize(Weight inputWeight)
        {
            return this.ObtainAnswer((this.Weight*inputWeight).GetSum());
        }
    }
}
