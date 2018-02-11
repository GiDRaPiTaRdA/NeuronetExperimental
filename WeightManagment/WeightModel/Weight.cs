using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TraversalLib;
using WeightManagment.WeightManage;

namespace WeightManagment.WeightModel
{
    //[Serializable]
    //public class Weight
    //{
    //    public int[,] WeightArray { get; set; }

    //    public int sizeX => this.WeightArray.GetLength(0);
    //    public int sizeY => this.WeightArray.GetLength(1);

    //    private Weight() { }

    //    public Weight(byte[,] weightArrayBytes, int? infill = null) : this(ConvertToInt(weightArrayBytes), infill) { }

    //    public Weight(sbyte[,] weightArrayBytes, int? infill = null) : this(ConvertToInt(weightArrayBytes), infill) { }

    //    public Weight(int[,] weightArray, int? infill = null)
    //    {
    //        this.WeightArray = weightArray;

    //        if (infill != null)
    //            WeightManager.InnitWeightBy(this, infill.Value);
    //    }

    //    public int GetSum()
    //    {
    //        int sum = 0;
    //        for (int x = 0; x < this.sizeX; x++)
    //        {
    //            for (int y = 0; y < this.sizeY; y++)
    //            {
    //                sum += this.WeightArray[x, y];
    //            }
    //        }
    //        return sum;
    //    }

    //    public override string ToString()
    //    {
    //        string s = "Weights:";
    //        for (int y = 0; y < this.sizeY; y++)
    //        {
    //            s += "\n";
    //            for (int x = 0; x < this.sizeX; x++)
    //            {
    //                s += this.WeightArray[x, y];
    //            }
    //        }
    //        return s;
    //    }

    //    #region Operator Overload
    //    public static Weight operator +(Weight weight1, Weight weight2)
    //    {
    //        return WeightManager.AddWeights(weight1, weight2);
    //    }
    //    public static Weight operator -(Weight weight1, Weight weight2)
    //    {
    //        return WeightManager.SubstituteWeight(weight1, weight2);
    //    }
    //    public static Weight operator *(Weight weight1, Weight weight2)
    //    {
    //        return WeightManager.Multiply(weight1, weight2);
    //    }

    //    private static int[,] ConvertToInt(Array array)
    //    {
    //        int[,] weightArray = new int[array.GetLength(0), array.GetLength(1)];

    //        array.Traversal((o, ps) => { weightArray[ps[0], ps[1]] = Convert.ToInt32(o); });

    //        return weightArray;
    //    }
    //    #endregion
    //}

    [Serializable]
    public class Weight
    {
        public double[,] WeightArray { get; set; }

        public int sizeX => this.WeightArray.GetLength(0);
        public int sizeY => this.WeightArray.GetLength(1);

        private Weight() { }

        public Weight(double[] weightArray): this(new double[1, weightArray.Length])
        {
            for (int index = 0; index < weightArray.Length; index++)
            {
                this.WeightArray[0, index] = weightArray[index];
            }
        }

        public Weight(double[,] weightArray, double? infill = null)
        {
            this.WeightArray = weightArray;

            if (infill != null)
                WeightManager.InnitWeightBy(this, infill.Value);
        }

        public double GetSum()
        {
            double sum = 0;
            for (int x = 0; x < this.sizeX; x++)
            {
                for (int y = 0; y < this.sizeY; y++)
                {
                    sum += this.WeightArray[x, y];
                }
            }
            return sum;
        }

        public override string ToString()
        {
            string s = "Weights:";
            for (int y = 0; y < this.sizeY; y++)
            {
                s += "\n";
                for (int x = 0; x < this.sizeX; x++)
                {
                    s += this.WeightArray[x, y];
                }
            }
            return s;
        }

        #region Operator Overload
        public static Weight operator +(Weight weight1, Weight weight2)
        {
            return WeightManager.AddWeights(weight1, weight2);
        }
        public static Weight operator -(Weight weight1, Weight weight2)
        {
            return WeightManager.SubstituteWeight(weight1, weight2);
        }
        public static Weight operator *(Weight weight1, Weight weight2)
        {
            return WeightManager.Multiply(weight1, weight2);
        }

        private static int[,] ConvertToInt(Array array)
        {
            int[,] weightArray = new int[array.GetLength(0), array.GetLength(1)];

            array.Traversal((o, ps) => { weightArray[ps[0], ps[1]] = Convert.ToInt32(o); });

            return weightArray;
        }
        #endregion
    }
}
