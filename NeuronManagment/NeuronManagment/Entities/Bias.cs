using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronManagment
{
    class Bias : Neuron
    {
        public Bias(double sum = 1)
        {
            this.LastSum = sum;
        }
    }
}
