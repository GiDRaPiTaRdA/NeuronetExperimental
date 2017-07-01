using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraversalLib;
using WeightManagment.WeightModel;

namespace WeightManagment.WeightManage
{
    public class WeightManager
    {
        public static Weight InnitWeightBy(Weight weight, byte innitVar)
        {
            for (int x = 0; x < weight.WeightArray.GetLength(0); x++)
                for (int y = 0; y < weight.WeightArray.GetLength(1); y++)
                    weight.WeightArray[x, y] = innitVar;
            return weight;
        }
        public static void InnitArrayBy(Weight weight, byte innitVar)
        {
            for (int x = 0; x < weight.WeightArray.GetLength(0); x++)
                for (int y = 0; y < weight.WeightArray.GetLength(1); y++)
                    weight.WeightArray[x, y] = innitVar;
        }

        public Weight ClearWeight(Weight weight)
        {
            weight = InnitWeightBy(weight, 0);
            return weight;
        }
        public static Weight AddWeights(Weight weight1, Weight weight2)
        {
            if (!EqualsSize(weight1, weight2))
                throw new IncompatibleWeightsSizesException();

            Array2DTraversal.Traversal(weight1.WeightArray, weight2.WeightArray, (b1, b2, x, y) => weight1.WeightArray[x, y] = Convert.ToByte(b1 + b2));
            return weight1;
        }
        public static Weight SubstituteWeight(Weight weight1, Weight weight2)
        {
            if (!EqualsSize(weight1, weight2))
                throw new IncompatibleWeightsSizesException();

            Array2DTraversal.Traversal(weight1.WeightArray, weight2.WeightArray, (b1, b2, x, y) => weight1.WeightArray[x, y] = Convert.ToByte(b1 - b2));
            return weight1;
        }
        public static Weight Multiply(Weight weight1, Weight weight2)
        {
            if (!EqualsSize(weight1, weight2))
                throw new IncompatibleWeightsSizesException();

            Array2DTraversal.Traversal(weight1.WeightArray, weight2.WeightArray, (b1, b2, x, y) => weight1.WeightArray[x, y] = Convert.ToByte(b1 * b2));
            return weight1;
        }

        public static int getSum(byte[,] array)
        {
            int sum = 0;
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    sum += array[x, y];
                }
            }
            return sum;
        }

        public static bool EqualsSize(Weight weight1, Weight weight2)
        {
            return weight1.sizeX == weight2.sizeX && weight1.sizeY == weight2.sizeY;
        }
    }
}
