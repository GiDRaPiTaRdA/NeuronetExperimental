using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraversalLib
{
    public class Array2DTraversal
    {

        public static void Traversal<T>(T[,] binaryArray, Action<T, int, int> action)
        {
            for (int x = 0; x < binaryArray.GetLength(0); x++)
            {
                for (int y = 0; y < binaryArray.GetLength(1); y++)
                {
                    action.Invoke(binaryArray[x, y], x, y);
                }
            }
        }

        public static void Traversal<T>(T[,] binaryArray1, T[,] binaryArray2, Action<T, T, int, int> action)
        {
            for (int x = 0; x < binaryArray1.GetLength(0); x++)
            {
                for (int y = 0; y < binaryArray1.GetLength(1); y++)
                {
                    action.Invoke(binaryArray1[x, y], binaryArray2[x, y], x, y);
                }
            }
        }

    }

    public static class TRXС
    {
        public static void Traversal(this Array binaryArray, Action<object, int[]> act)
        {
            int rank = binaryArray.Rank;
            int penatration = 0;
            int[] coords = new int[rank];

            Recursive(penatration, binaryArray, act, coords, rank);
        }

        public static void RecursiveTraversal(Array binaryArray, Action<object, int[]> act)
        {
            int rank = binaryArray.Rank;
            int penatration = 0;
            int[] coords = new int[rank];

            Recursive(penatration,binaryArray,act,coords,rank);
        }
        private static void Recursive(int penatration, Array binaryArray, Action<object, int[]> act,int[] coords,int rank)
        {
            penatration++;

            for (int i = 0; i < binaryArray.GetLength(penatration - 1); i++)
            {
                coords[penatration - 1] = i;

                if (penatration < rank)
                {
                    Recursive(penatration, binaryArray, act, coords, rank);
                }
                else
                {
                    act(binaryArray.GetValue(coords), coords);
                }
            }

            penatration--;

        }

    }

    public static class TRXСM
    {
        public static void Traversal(this Array[] arrays, Action<object[], int[]> act)
        {
            int rank = arrays[0].Rank;
            int penatration = 0;
            int[] coords = new int[rank];

            Recursive(penatration, arrays, act, coords, rank);
        }


        public static void RecursiveTraversal(Array[] binaryArray, Action<object[], int[]> act)
        {
            int rank = binaryArray.Rank;
            int penatration = 0;
            int[] coords = new int[rank];

            Recursive(penatration, binaryArray, act, coords, rank);
        }
        private static void Recursive(int penatration, Array[] arrays, Action<object[], int[]> act, int[] coords, int rank)
        {
            penatration++;

            for (int i = 0; i < arrays[0].GetLength(penatration - 1); i++)
            {
                coords[penatration - 1] = i;

                if (penatration < rank)
                {
                    Recursive(penatration, arrays, act, coords, rank);
                }
                else
                {

                    object[] objects = new object[arrays.Length];

                    for (int x = 0; x < arrays.Length; x++)
                    {
                        objects[x] = arrays[x].GetValue(coords);
                    }

                    act(objects, coords);
                }
            }

            penatration--;

        }
    }
}

