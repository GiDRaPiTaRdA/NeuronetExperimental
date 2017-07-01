using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightManagment.WeightManage;

namespace WeightManagment.WeightModel
{
    public class Weight
    {
        public byte[,] WeightArray { get; set; }

        public int sizeX => WeightArray.GetLength(0);
        public int sizeY => WeightArray.GetLength(1);

        public Weight(byte[,] weightArray,byte infill = 0)
        {
            this.WeightArray = weightArray;
            WeightManager.InnitWeightBy(this, infill);
        }

        public int GetSum()
        {
            int sum = 0;
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    sum += WeightArray[x, y];
                }
            }
            return sum;
        }

        public override string ToString()
        {
            string s = "Weights:";
            for (int y = 0; y < sizeY; y++)
            {
                s += "\n";
                for (int x = 0; x < sizeX; x++)
                {
                    s += WeightArray[x, y];
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
        #endregion
    }
}
