using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NeuralCore.NeuronManagment;

namespace NeuralDemo.Demo
{
    // TEST NEURO TO TRAIN ON, to see the principle

    public class Neuro
    {
        int NeuronsCountC;
        int NeuronsCountB;
        int NeuronsCountA;

        double[] SumsC;
        double[] SumsB;
        double[] SumsA;

        double[,] weightsCnB;
        double[,] weightsBnA;

        public Neuro()
        {
            // plus bias
            this.NeuronsCountC = 3 + 1;
            this.NeuronsCountB = 2;
            this.NeuronsCountA = 1;

            this.SumsC = new double[this.NeuronsCountC];
            this.SumsB = new double[this.NeuronsCountB];
            this.SumsA = new double[this.NeuronsCountA];

            this.weightsCnB = GenarateRandomWeights(this.NeuronsCountC, this.NeuronsCountB);
            this.weightsBnA = GenarateRandomWeights(this.NeuronsCountB, this.NeuronsCountA);
        }

        private static double[,] GenarateRandomWeights(int x, int y)
        {
            Random rand = new Random(0);

            double[,] weights = new double[x, y];

            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    weights[i, j] = (double)rand.Next(-2, 2) / 10;
                }
            }

            return weights;
        }

        public double[] Update(double[] inputs)
        {
            // Fill Inputs into C Layer
            for (int i = 0; i < this.NeuronsCountC - 1; i++)
            {
                this.SumsC[i] = inputs[i];
            }

            // Fill activations into B Layer
            for (int y = 0; y < this.NeuronsCountB; y++)
            {
                double sum = 0;

                for (int x = 0; x < this.NeuronsCountC; x++)
                {
                    sum += this.weightsCnB[x, y] * this.SumsC[x];
                }

                this.SumsB[y] = NeuroNetFunctions.SigmoidFunc(sum);
            }

            // Fill activations into A Layer
            for (int y = 0; y < this.NeuronsCountA; y++)
            {
                double sum = 0;

                for (int x = 0; x < this.NeuronsCountB; x++)
                {
                    sum += this.weightsBnA[x, y] * this.SumsB[x];
                }

                this.SumsA[y] = NeuroNetFunctions.SigmoidFunc(sum);
            }

            return this.SumsA;
        }

        public double BackPropagate(double[] targets, double learningRate)
        {
            // Layer A deltas
            double[] deltasA = new double[this.NeuronsCountA];
            for (int i = 0; i < this.NeuronsCountA; i++)
            {
                double error = targets[i] - this.SumsA[i];
                deltasA[i] = NeuroNetFunctions.SigmoidDerivativeFunc(this.SumsA[i]) * error;
            }

            // Layer B deltas
            double[] deltasB = new double[this.NeuronsCountB];
            for (int i = 0; i < this.NeuronsCountB; i++)
            {
                double error = 0;
                for (int j = 0; j < this.NeuronsCountA; j++)
                {
                    error += deltasA[j] * this.weightsBnA[i, j];
                }
                deltasB[i] = NeuroNetFunctions.SigmoidDerivativeFunc(this.SumsB[i]) * error;
            }

            // Update A weights 
            for (int i = 0; i < this.NeuronsCountB; i++)
            {
                for (int j = 0; j < this.NeuronsCountA; j++)
                {
                    double change = deltasA[j] * this.SumsB[i];
                    this.weightsBnA[i, j] += change * learningRate;
                }
            }

            // Update B weights 
            for (int i = 0; i < this.NeuronsCountC; i++)
            {
                for (int j = 0; j < this.NeuronsCountB; j++)
                {
                    double change = deltasB[j] * this.SumsC[i];
                    this.weightsCnB[i, j] += change * learningRate;
                }
            }

            // Calculate error
            double err = targets.Select((t, i) => 0.5 * (Math.Pow(t, 2) - Math.Pow(this.SumsA[i], 2))).Sum();

            return err;
        }

        public void Train(Dictionary<double[], double[]> patterns, int iterations, double learninRate = 0.5)
        {
            for (int i = 0; i < iterations; i++)
            {
                double error = 0;

                for (int j = 0; j < patterns.Count; j++)
                {
                    this.Update(patterns.ElementAt(j).Key);

                    error += this.BackPropagate(patterns.ElementAt(j).Value, learninRate);
                }
                if (i % 100 == 0)
                    Debug.WriteLine(error);
            }
        }

        public void Test(Dictionary<double[], double[]> patterns)
        {
            for (int j = 0; j < patterns.Count; j++)
            {
                Debug.WriteLine(this.Update(patterns.ElementAt(j).Key)[0]);
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