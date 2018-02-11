
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using ImageProcessor;
using NeuronManagment;
using WeightManagment.WeightModel;

namespace ViewControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsChecked1
        {
            get { return this.isChecked1; }
            set
            {
                this.isChecked1 = value;
                this.inputsWeight.WeightArray[0, 0] = value ? 1 : 0;
                this.receptorC1.LastSum = this.inputsWeight.WeightArray[0, 0];
            }
        }

        public bool IsChecked2
        {
            get { return this.isChecked2; }
            set
            {
                this.isChecked2 = value;
                this.inputsWeight.WeightArray[0, 1] = value ? 1 : 0;
                this.receptorC2.LastSum = this.inputsWeight.WeightArray[0, 1];
            }
        }

        public bool IsChecked3
        {
            get { return this.isChecked3; }
            set
            {
                this.isChecked3 = value;
                this.inputsWeight.WeightArray[0, 2] = value ? 1 : 0;
                this.receptorC3.LastSum = this.inputsWeight.WeightArray[0, 2];
            }
        }

        public MainWindow()
        {
            this.InitializeComponent();

            this.DataContext = this;

            this.Initialize();
        }

        // Droup C receptors
        private Weight inputsWeight;

        // Group B hidden layer
        private Weight weightB1;
        private Weight weightB2;
        // Group A decision
        private Weight weightA1;

        // Group C receptors
        private Receptor receptorC1;
        private Receptor receptorC2;
        private Receptor receptorC3;
        // Group B hidden layer
        private Neuron neuronB1;
        private Neuron neuronB2;
        // Group A decision
        private Neuron neuronA1;

        // bool answer = false;
        private bool isChecked1;
        private bool isChecked2;
        private bool isChecked3;

        readonly Dictionary<double[], bool> answers = new Dictionary<double[], bool>
            {
                {new double[] {0, 0, 0}, false},
                {new double[] {0, 0, 1}, true},
                {new double[] {0, 1, 0}, false},
                {new double[] {0, 1, 1}, false},
                {new double[] {1, 0, 0}, true},
                {new double[] {1, 0, 1}, true},
                {new double[] {1, 1, 0}, false},
                {new double[] {1, 1, 1}, false}
            };

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var answer = this.Ask();
            OutputAnswer(answer);
        }

        private void button_Click1(object sender, RoutedEventArgs e)
        {
            var result = this.Ask();

            this.TakeAccountErrors(result.Item1);

            result = this.Ask();

            OutputAnswer(result);


            //this.OutputArray(this.weightA1);
            //this.OutputArray(this.weightB1);
            //this.OutputArray(this.weightB2);
        }

        private void OutputArray(Weight weight)
        {
            Debug.WriteLine(weight.ToString());
        }

        private void Initialize()
        {
            // Init inputs
            this.inputsWeight = new Weight(new double[,] { { 0, 0, 0 } });

            // Init receptors
            this.receptorC1 = new Receptor(this.inputsWeight.WeightArray[0, 0]);
            this.receptorC2 = new Receptor(this.inputsWeight.WeightArray[0, 1]);
            this.receptorC3 = new Receptor(this.inputsWeight.WeightArray[0, 2]);

            //this.weightB1 = new Weight(new double[1, 2] { { 0.25, -0.20 } });
            //this.weightB2 = new Weight(new double[1, 2] { { 0.07, 0.32} });

            //this.weightA1 = new Weight(new double[1, 2] { { -0.41, 0.12 } });

            this.weightB1 = new Weight(new double[1, 3] { { 0.79, 0.44, 0.43 } });
            this.weightB2 = new Weight(new double[1, 3] { { 0.85, 0.43, 0.29 } });
            this.weightA1 = new Weight(new double[1, 2] { { 0.5, 0.52 } });

            //this.weightB1 = new Weight(new double[1, 3] { { 1.92542631, -3.86526729, 1.95739156 } });
            //this.weightB2 = new Weight(new double[1, 3] { { -2.27707213, 5.15421872, -2.26037546 } });
            //this.weightA1 = new Weight(new double[1, 2] { { 4.19855106, -8.7723603 } });

            // Init neurons
            this.neuronB1 = new Neuron(this.weightB1);
            this.neuronB2 = new Neuron(this.weightB2);

            this.neuronA1 = new Neuron(this.weightA1);

            // Init links

            // Group C Links
            this.receptorC1.Previous = null;
            this.receptorC2.Previous = null;
            this.receptorC3.Previous = null;
            this.receptorC1.Next = new[] { this.neuronB1, this.neuronB2 };
            this.receptorC2.Next = new[] { this.neuronB1, this.neuronB2 };
            this.receptorC3.Next = new[] { this.neuronB1, this.neuronB2 };

            // Group B Links
            this.neuronB1.Previous = new[] { this.receptorC1, this.receptorC2, this.receptorC3 };
            this.neuronB2.Previous = new[] { this.receptorC1, this.receptorC2, this.receptorC3 };
            this.neuronB1.Next = new[] { this.neuronA1 };
            this.neuronB2.Next = new[] { this.neuronA1 };

            // Group A Links
            this.neuronA1.Previous = new[] { this.neuronB1, this.neuronB2 };
            this.neuronA1.Next = null;



            this.checkBox.IsChecked = Convert.ToBoolean(this.inputsWeight.WeightArray[0, 0]);
            this.checkBox_Copy.IsChecked = Convert.ToBoolean(this.inputsWeight.WeightArray[0, 1]);
            this.checkBox_Copy1.IsChecked = Convert.ToBoolean(this.inputsWeight.WeightArray[0, 2]);

            this.Teach();
            this.Test();

            this.Ask();
        }

        private void Teach()
        {
            int iterations = 5000;

            Debug.WriteLine("[TEACH : " + iterations + "]");

            this.Ask();

            Random random = new Random();

            for (int i = 0; i < iterations; i++)
            {
                var rnd = random.Next(8);

                var keyValue = this.answers.ElementAt(rnd);

                this.IsChecked1 = keyValue.Key[0] == 1;
                this.IsChecked2 = keyValue.Key[1] == 1;
                this.IsChecked3 = keyValue.Key[2] == 1;

                this.inputsWeight.WeightArray[0, 0] = Convert.ToDouble(keyValue.Key[0] == 1);
                this.inputsWeight.WeightArray[0, 1] = Convert.ToDouble(keyValue.Key[1] == 1);
                this.inputsWeight.WeightArray[0, 2] = Convert.ToDouble(keyValue.Key[2] == 1);

                var result = this.Ask();

                OutputAnswer(result);

                if (result.Item1 != keyValue.Value)
                {
                    this.TakeAccountErrors(result.Item1);
                }
            }

        }

        private void Test()
        {
            Debug.WriteLine("[TEST]");

            bool total = true;
            List<double> predictions = new List<double>();

            foreach (var answer in this.answers)
            {
                this.IsChecked1 = answer.Key[0] == 1;
                this.IsChecked2 = answer.Key[1] == 1;
                this.IsChecked3 = answer.Key[2] == 1;

                var ask = this.Ask();


                bool isCorrectAnwer = ask.Item1 == answer.Value;
                predictions.Add(ask.Item2);


                total &= isCorrectAnwer;

                Debug.WriteLine(ask.Item2 + " [RESULT " + isCorrectAnwer + "]");
            }


            Debug.WriteLine("[TOTAL RESULT : " + total + " MMSE : " + this.Mmse(predictions.ToArray()) + "]");
        }

        private double Mmse(double[] predictions)
        {
            double qubicErrorSum = 0;
            for (int i = 0; i < predictions.Length; i++)
            {
                double qubicError = Math.Pow(predictions[i] - (this.answers.ElementAt(i).Value ? 1 : 0), 2);
                qubicErrorSum += qubicError;
            }

            double minimaMeanSquareError = qubicErrorSum / predictions.Length;
            return minimaMeanSquareError;
        }

        private Tuple<bool, double> Ask()
        {
            var result = Neuron.Recognize(this.neuronA1);

            var answer = Neuron.GetAnswer(result);

            return new Tuple<bool, double>(answer, result);
        }

        private void TakeAccountErrors(bool answer)
        {
            Neuron.ReverseErrorSpread(this.neuronA1, answer);

            this.Ask();
        }

        private void OutputAnswer(Tuple<bool, double> answer)
        {
            Debug.Write(answer.Item2 + " ");
            Debug.WriteLine(answer.Item1);

            this.tB1.Text = this.weightB1.ToString();
            this.tB2.Text = this.weightB2.ToString();
            this.tA1.Text = this.weightA1.ToString();
        }
    }

   
}
