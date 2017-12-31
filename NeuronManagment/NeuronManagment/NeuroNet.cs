using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace NeuronManagment
{
    class NeuroNet
    {
        List<Neuron> NeuroNetwork;

        public NeuroNet()
        {
            Inntialize();
        }

        public NeuroNet(List<Neuron> neuronsList)
        {
            Inntialize();
            NeuroNetwork = neuronsList;
        }



        private void Inntialize()
        {
        }
    }
}
