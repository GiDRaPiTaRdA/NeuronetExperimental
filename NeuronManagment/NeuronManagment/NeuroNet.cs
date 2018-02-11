﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Linq;
using System.Diagnostics;

namespace NeuronManagment
{
    public class NeuroNet
    {
        List<Neuron>[] NeuroLayers { get; set; }

        public double LearningRate { get; set; }

        public NeuroNet(params int[] stucture)
        {
            this.NeuroLayers = new List<Neuron>[stucture.Length];

            this.LearningRate = 1;
            this.Inntialize(stucture);
        }

        private void Inntialize(int[] structure)
        {
            for (int i = 0; i < structure.Length; i++)
            {
                this.NeuroLayers[i] = new List<Neuron>();

                // First receptors
                if (i == 0)
                {
                    for (int j = 0; j < structure[i]; j++)
                    {
                        this.NeuroLayers[i].Add(new Receptor(0));
                    }
                }

                // Then Neurons
                else
                {
                    for (int j = 0; j < structure[i]; j++)
                    {
                        this.NeuroLayers[i].Add(new Neuron());

                        // Link with previous
                        for (int k = 0; k < structure[i - 1]; k++)
                        {
                            Neuron next = this.NeuroLayers[i][j];
                            Neuron previous = this.NeuroLayers[i - 1][k];

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

        public double ForwardPropagation(params double[] inputs)
        {
            this.SetInputs(inputs);

            for (int i = 1; i < this.NeuroLayers.GetLength(0); i++)
            {
                for (int j = 0; j < this.NeuroLayers[i].Count; j++)
                {
                    Neuron neuron = this.NeuroLayers[i][j];

                    neuron.LastSum = this.CounSum(neuron);
                }
            }

            double result = this.NeuroLayers[this.NeuroLayers.GetLength(0) - 1][0].LastSum;

            return result;
        }

        public double BackPropagation(double[] targets)
        {

            for(int l = this.NeuroLayers.Length-1;l>0; l--)
            {
                // OUTPUT LAYER
                if(l == this.NeuroLayers.Length - 1)
                {
                    // Layer A deltas
                    for (int i = 0; i < this.NeuroLayers[l].Count; i++)
                    {
                        Neuron neuron = this.NeuroLayers[l][i];

                        double error = targets[i] - neuron.LastSum;
                        neuron.Delta = NeuronManager.SigmoidDerivativeFunc(neuron.LastSum) * error;
                    }
                }
                // OTHER LAYERS
                else
                {
                    for (int i = 0; i < this.NeuroLayers[l].Count; i++)
                    {
                        Neuron neuronCurrent = this.NeuroLayers[l][i];
                       
                        double error = 0;

                        // Neurons from next layer
                        for (int j = 0; j < this.NeuroLayers[l+1].Count; j++)
                        {
                            Neuron neuronNext = this.NeuroLayers[l + 1][j];

                            error += neuronNext.Delta * this.GetSynapse(neuronCurrent, neuronNext).Weight;
                        }

                        neuronCurrent.Delta = NeuronManager.SigmoidDerivativeFunc(neuronCurrent.LastSum) * error;
                    }
                }
            }

            //FOR EACH LAYER UPDATE WEIGHTS
            for (int i = this.NeuroLayers.GetLength(0) - 1; i > 0; i--)
            {
                List<Neuron> nextLayer = this.NeuroLayers[i];
                List<Neuron> previousLayer = this.NeuroLayers[i - 1];

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
            double err = targets.Select((t, i) => 0.5 * (Math.Pow(t, 2) - Math.Pow(this.NeuroLayers[this.NeuroLayers.Length-1][i].LastSum, 2))).Sum();

            return err;
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
            if (inputs.Length != this.NeuroLayers[0].Count)
                throw new Exception("Incompatible size of inputs");

            for (int i = 0; i < inputs.Length; i++)
            {
                this.NeuroLayers[0][i].LastSum = inputs[i];
            }
        }

        private double CounSum(Neuron neuron)
        {
            double sum = neuron.Previous.Sum(s => s.Weight * s.Previous.LastSum);

            double funSum = NeuronManager.SigmoidFunc(sum);

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

        public void Train(Dictionary<double[], double[]> patterns, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                double error = 0;

                for (int j = 0; j < patterns.Count; j++)
                {
                    this.ForwardPropagation(patterns.ElementAt(j).Key);

                    error += this.BackPropagation(patterns.ElementAt(j).Value);
                }
                if (i % 100 == 0)
                    Debug.WriteLine(error);
            }
        }

        public void Test(Dictionary<double[], double[]> patterns)
        {
            Debug.WriteLine("Test");

            for (int j = 0; j < patterns.Count; j++)
            {
                var a = (patterns.ElementAt(j).Key);

                Debug.WriteLine(this.ForwardPropagation(a));
            }
        }

        public void demo()
        {
            Dictionary<double[], double[]> answers = new Dictionary<double[], double[]>
            {
                {new double[] {0, 0, 0}, new double[] {0}},
                {new double[] {0, 0, 1}, new double[] {1}},
                {new double[] {0, 1, 0}, new double[] {0}},
                {new double[] {0, 1, 1}, new double[] {0}},
                {new double[] {1, 0, 0}, new double[] {1}},
                {new double[] {1, 0, 1}, new double[] {1}},
                {new double[] {1, 1, 0}, new double[] {0}},
                {new double[] {1, 1, 1}, new double[] {1}}
            };

            this.Train(answers, 10000);

            this.Test(answers);
        }

    }
}
