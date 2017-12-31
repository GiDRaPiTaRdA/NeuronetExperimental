using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraversalLib
{
    /// <summary>
    /// Traversal Recursive Extention Class [Multiple]
    /// </summary>
    public static class TREXCM
    {
        /// <summary>
        /// Traversal of multiple arrays extention
        /// </summary>
        /// <param name="enumerable">collection</param>
        /// <param name="act">action params => [objects of current iteration],[indexes of dimention] </param>
        public static void Traversal(this Array[] arrays, Action<object[], int[]> act)
        {
            int rank = arrays[0].Rank;
            int penatration = 0;
            int[] coords = new int[rank];

            Recursive(penatration, arrays, act, coords, rank);
        }

        /// <summary>
        /// Traversal of multiple arrays extention
        /// </summary>
        /// <param name="enumerable">collection</param>
        /// <param name="act">action params => [objects of current iteration],[indexes of dimention] </param>
        public static void Traversal(this IEnumerable<object>[] ienumerable, Action<object[], int[]> act)
        {
            int enumerableCount = ienumerable.Count();

            Array[] arrays = new Array[enumerableCount];

            for (int i = 0; i<enumerableCount;i++)
            {
                arrays[i] = ienumerable.ElementAt(i).ToArray();
            }

            arrays.Traversal(act);
        }

        /// <summary>
        /// Traversal of multiple arrays
        /// </summary>
        /// <param name="enumerable">collection</param>
        /// <param name="act">action params => [objects of current iteration],[indexes of dimention] </param>
        public static void RecursiveTraversal(Array[] enumerable, Action<object[], int[]> act)
        {
            enumerable.Traversal(act);
        }

        /// <summary>
        /// Traversal of multiple arrays with params
        /// </summary>
        /// <param name="enumerable">collection</param>
        /// <param name="act">action params => [objects of current iteration],[indexes of dimention] </param>
        public static void RecursiveTraversal(Action<object[], int[]> act, params Array[] enumerable)
        {
            enumerable.Traversal(act);
        }

        #region BlackMagic
        /// <summary>
        /// Recursive method
        /// </summary>
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
        #endregion

    }
}
