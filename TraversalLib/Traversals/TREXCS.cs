using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraversalLib
{
    /// <summary>
    /// Traversal Recursive Extention Class[Single]
    /// </summary>
    public static class TREXCS
    {
        /// <summary>
        /// Traversal of single array extention
        /// </summary>
        public static void Traversal(this Array array, Action<object, int[]> act)
        {
            int rank = array.Rank;
            int penatration = 0;
            int[] coords = new int[rank];

            Recursive(penatration, array, act, coords, rank);
        }

        /// <summary>
        /// Traversal of single array extention
        /// </summary>
        public static void Traversal(this IEnumerable<object> ienumerable, Action<object, int[]> act)
        {
            Array array = ienumerable.ToArray();

            array.Traversal(act);
        }

        /// <summary>
        /// Traversal of single array
        /// </summary>
        public static void RecursiveTraversal(Array ienumerable, Action<object, int[]> act)
        {
            ienumerable.Traversal(act);
        }

        #region BlackMagic
        /// <summary>
        /// Recursive method
        /// </summary>
        private static void Recursive(int penatration, Array binaryArray, Action<object, int[]> act, int[] coords, int rank)
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
        #endregion


        #region LinQ
        public static bool Any<T>(this Array array, Predicate<T> pregicate = null)
        {
            bool any = false;

            foreach (T current in array)
            {
                if (pregicate?.Invoke(current) ?? false)
                {
                    any = true;
                    break;
                }
            }

            return any;
        }

        public static bool All<T>(this Array array, Predicate<T> pregicate = null)
        {
            bool all = true;

            foreach (T current in array)
            {
                if (!pregicate?.Invoke(current) ?? true)
                {
                    all = false;
                    break;
                }
            }

            return all;
        }

        public static T FirstOrDefault<T>(this Array array, Predicate<T> pregicate = null)
        {
            T first = default(T);

            foreach (T current in array)
            {
                if (pregicate?.Invoke(current) ?? true)
                {
                    first = current;
                    break;
                }
            }

            return first;
        }

        public static T First<T>(this Array array, Predicate<T> pregicate = null)
        {
            T first = array.FirstOrDefault(pregicate);

            if (first == null)
            {
                throw new Exception("Collection does not contain element");
            }

            return first;
        }

        public static IEnumerable<T> Where<T>(this Array array, Predicate<T> pregicate)
        {
            List<T> collection = new List<T>();

            foreach (T current in array)
            {
                if (pregicate.Invoke(current))
                {
                    collection.Add(current);
                }
            }

            return collection;
        }

        public static void ForEach<T>(this Array array, Action<T> action)
        {
            foreach (T current in array)
            {
                action?.Invoke(current);
            }
        }
        #endregion
    }
}
