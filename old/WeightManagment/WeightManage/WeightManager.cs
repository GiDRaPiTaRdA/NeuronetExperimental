﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraversalLib;
using WeightManagment.WeightModel;

namespace WeightManagment.WeightManage
{
    public static class WeightManager
    {
        public static Weight InnitWeightBy(Weight weight, double innitVar)
        {
            for (int x = 0; x < weight.WeightArray.GetLength(0); x++)
                for (int y = 0; y < weight.WeightArray.GetLength(1); y++)
                    weight.WeightArray[x, y] = innitVar;
            return weight;
        }
        public static void InnitArrayBy(Weight weight, double innitVar)
        {
            for (int x = 0; x < weight.WeightArray.GetLength(0); x++)
                for (int y = 0; y < weight.WeightArray.GetLength(1); y++)
                    weight.WeightArray[x, y] = innitVar;
        }

        public static Weight ClearWeight(Weight weight)
        {
            weight = InnitWeightBy(weight, 0);
            return weight;
        }
        public static Weight AddWeights(Weight weight1, Weight weight2)
        {
            Weight result = new Weight(new double[weight1.sizeX, weight1.sizeY]);

            if (!EqualsSize(weight1, weight2))
                throw new IncompatibleWeightsSizesException();

            BinaryArrayTraversal.Traversal(weight1.WeightArray, weight2.WeightArray, (b1, b2, x, y) => result.WeightArray[x, y] = b1 + b2);
            return result;
        }
        public static Weight SubstituteWeight(Weight weight1, Weight weight2)
        {
            Weight result = new Weight(new double[weight1.sizeX, weight1.sizeY]);

            if (!EqualsSize(weight1, weight2))
                throw new IncompatibleWeightsSizesException();

            BinaryArrayTraversal.Traversal(weight1.WeightArray, weight2.WeightArray, (b1, b2, x, y) => result.WeightArray[x, y] =b1 - b2);
            return result;
        }
        public static Weight Multiply(Weight weight1, Weight weight2)
        {
            Weight result = new Weight(new double[weight1.sizeX,weight1.sizeY]);

            if (!EqualsSize(weight1, weight2))
                throw new IncompatibleWeightsSizesException();

            BinaryArrayTraversal.Traversal(weight1.WeightArray, weight2.WeightArray, (b1, b2, x, y) => result.WeightArray[x, y] =b1 * b2);
            return result;
        }

        public static double getSum(Weight weight)
        {
            double sum = 0;
            for (int x = 0; x < weight.WeightArray.GetLength(0); x++)
            {
                for (int y = 0; y < weight.WeightArray.GetLength(1); y++)
                {
                    sum += weight.WeightArray[x, y];
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
