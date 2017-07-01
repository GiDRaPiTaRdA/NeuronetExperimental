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

    public class TRX<T>
    {

        public static void TraversalX<T>(T[,] binaryArray, Action<T, int, int> action)
        {
            int rank = binaryArray.Rank;


            for (int x = 0; x < binaryArray.GetLength(0); x++)
            {
                for (int y = 0; y < binaryArray.GetLength(1); y++)
                {
                    for (int z = 0; y < binaryArray.GetLength(1); y++)
                    {
                        action.Invoke(binaryArray[x, y], x, y);
                    }
                }
            }


        }

        private void Cycle(Action<int> action, int repeatTimes)
        {
            for (int i = 0; i < repeatTimes; i++)
            {
                action.Invoke(i);
            }
        }


        private T[,,] binaryArray;
        private int rank;
        private int penatration = 0;
        Action<int[]> act;
        public Int32[] coords;
        private int index = 0;

        public TRX(T[,,] binaryArray, Action<int[]> act)
        {
            this.binaryArray = binaryArray;
            this.rank = binaryArray.Rank;
            this.act = act;
            this.coords = new Int32[rank];

        }

        public void Recursive()
        {
            penatration++;

            for (int i = 0; i < binaryArray.GetLength(penatration - 1); i++)
            {
                if (rank > penatration)
                {
                    Recursive();

                   if(penatration == rank)
                        act(coords);
                }
                else
                {
                    if (penatration == rank)
                        act(coords);
                }
            }

            penatration--;
            rank--;
        }
    }
}

