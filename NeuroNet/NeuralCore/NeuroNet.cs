using System;
using System.Collections.Generic;
using System.Linq;
using NeuralCore.Entities;
using NeuralCore.NeuronManagment;

namespace NeuralCore
{
    public class NeuroNet
    {
        #region Properies

        protected internal Memory NeuroMemory { get; set; }

        public double LearningRate { get; set; } = 1;

        #endregion

        public NeuroNet(params int[] stucture)
        {
            this.NeuroMemory = new Memory(stucture.Length);

            this.Initialize(stucture);
        }

        /// <summary>
        /// Initializes network with structure
        /// </summary>
        /// <param name="structure">structure of network</param>
        private void Initialize(IReadOnlyList<int> structure)
        {
            for (int i = 0; i < structure.Count; i++)
            {
                this.NeuroMemory.Layers[i] = new List<Neuron>();

                // First receptors
                if (i == 0)
                {
                    for (int j = 0; j < structure[i]; j++)
                    {
                        this.NeuroMemory.Layers[i].Add(new Receptor(0));
                    }
                }

                // Then Neurons
                else
                {
                    for (int j = 0; j < structure[i]; j++)
                    {
                        this.NeuroMemory.Layers[i].Add(new Neuron());

                        // Link with previous
                        for (int k = 0; k < structure[i - 1]; k++)
                        {
                            Neuron next = this.NeuroMemory.Layers[i][j];
                            Neuron previous = this.NeuroMemory.Layers[i - 1][k];

                            Synapse synapse = new Synapse
                            {
                                Next = next,
                                Previous = previous
                            };

                            next.Previous.Add(synapse);
                            previous.Next.Add(synapse);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Take answer from network
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public double[] ForwardPropagation(params double[] inputs)
        {
            this.SetInputs(inputs);

            for (int i = 1; i < this.NeuroMemory.Layers.GetLength(0); i++)
            {
                for (int j = 0; j < this.NeuroMemory.Layers[i].Count; j++)
                {
                    Neuron neuron = this.NeuroMemory.Layers[i][j];

                    neuron.LastSum = this.CountSum(neuron);
                }
            }

            double[] outputs = this.NeuroMemory.Layers[this.NeuroMemory.Layers.GetLength(0) - 1].Select((n) => n.LastSum).ToArray();

            return outputs;
        }

        /// <summary>
        /// Process error
        /// </summary>
        /// <param name="targets"></param>
        /// <returns>error</returns>
        public double BackPropagation(double[] targets)
        {
            for(int l = this.NeuroMemory.Layers.Length-1;l>0; l--)
            {
                // OUTPUT LAYER
                if(l == this.NeuroMemory.Layers.Length - 1)
                {
                    // Layer A deltas
                    for (int i = 0; i < this.NeuroMemory.Layers[l].Count; i++)
                    {
                        Neuron neuron = this.NeuroMemory.Layers[l][i];

                        double error = targets[i] - neuron.LastSum;
                        neuron.Delta = NeuroFuncs.SigmoidDerivativeFunc(neuron.LastSum) * error;
                    }
                }
                // OTHER LAYERS
                else
                {
                    for (int i = 0; i < this.NeuroMemory.Layers[l].Count; i++)
                    {
                        Neuron neuronCurrent = this.NeuroMemory.Layers[l][i];
                       
                        double error = 0;

                        // Neurons from next layer
                        for (int j = 0; j < this.NeuroMemory.Layers[l+1].Count; j++)
                        {
                            Neuron neuronNext = this.NeuroMemory.Layers[l + 1][j];

                            error += neuronNext.Delta * this.GetSynapse(neuronCurrent, neuronNext).Weight;
                        }

                        neuronCurrent.Delta = NeuroFuncs.SigmoidDerivativeFunc(neuronCurrent.LastSum) * error;
                    }
                }
            }

            //FOR EACH LAYER UPDATE WEIGHTS
            for (int i = this.NeuroMemory.Layers.GetLength(0) - 1; i > 0; i--)
            {
                List<Neuron> nextLayer = this.NeuroMemory.Layers[i];
                List<Neuron> previousLayer = this.NeuroMemory.Layers[i - 1];

                foreach (Neuron previous in previousLayer)
                {
                    foreach (Neuron next in nextLayer)
                    {
                        Synapse synapse = this.GetSynapse(previous, next);

                        double change = next.Delta * previous.LastSum;

                        synapse.Weight = synapse.Weight + this.LearningRate * change;
                    }
                }
            }

            // Calculate error
            double err = targets.Select((t, i) => 0.5 * (Math.Pow(t, 2) - Math.Pow(this.NeuroMemory.Layers[this.NeuroMemory.Layers.Length-1][i].LastSum, 2))).Sum();

            return err;
        }

        /// <summary>
        /// Train network once
        /// </summary>
        /// <param name="patterns">inputs/anwers</param>
        /// <returns>error</returns>
        public double Train(Dictionary<double[], double[]> patterns)
        {
            double error = 0;

            for (int j = 0; j < patterns.Count; j++)
            {
                this.ForwardPropagation(patterns.ElementAt(j).Key);

                error += this.BackPropagation(patterns.ElementAt(j).Value);
            }

            return error;
        }

        /// <summary>
        /// Train network once
        /// </summary>
        /// <param name="patterns">inputs/anwers</param>
        /// <param name="iterations">nuber of times the network is trained</param>
        /// <returns>error</returns>
        public void Train(Dictionary<double[], double[]> patterns, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                this.Train(patterns);
            }
        }



        #region Help

        public Neuron[] GetNext(Neuron neuron)
        {
            Neuron[] neurons = neuron.Next.Select(s => s.Next).ToArray();

            return neurons;
        }

        public Neuron[] GetPrevious(Neuron neuron)
        {
            Neuron[] neurons = neuron.Previous.Select(s => s.Previous).ToArray();

            return neurons;
        }

        private void SetInputs(params double[] inputs)
        {
            if (inputs.Length != this.NeuroMemory.Layers[0].Count)
                throw new ArgumentException("Incompatible size of inputs");

            for (int i = 0; i < inputs.Length; i++)
            {
                this.NeuroMemory.Layers[0][i].LastSum = inputs[i];
            }
        }

        private double CountSum(Neuron neuron)
        {
            double sum = neuron.Previous.Sum(s => s.Weight * s.Previous.LastSum);

            double funSum = NeuroFuncs.SigmoidFunc(sum);

            return funSum;
        }

        private Synapse GetSynapse(Neuron previous, Neuron next) =>
            previous.Next.FirstOrDefault(synapse => synapse.Next == next);

        private List<Neuron> GetNeuronsWithBias(List<Neuron> neurons, Bias bias)
        {
            var neuronWithBias = new List<Neuron>(neurons);
            neuronWithBias.Add(bias);
            return neuronWithBias;
        }

        #endregion
    }
}