using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualData
{
    public class VisualDataStorage
    {
        public static bool Save(byte[,] bytes, string filename)
        {
            Bitmap bitmap = new Bitmap(bytes.GetLength(0), bytes.GetLength(1));

            TraversalLib.Array2DTraversal.Traversal(bytes, (b, x, y) => bitmap.SetPixel(x, y, Color.FromArgb(255,b,b,b)));

            bitmap.Save(filename);

            return false;
        }

        public static bool Save(Color[,] colors, string filename)
        {
            Bitmap bitmap = new Bitmap(colors.GetLength(0), colors.GetLength(1));

            TraversalLib.Array2DTraversal.Traversal(colors, (color, x, y) => bitmap.SetPixel(x, y, Color.FromArgb(255, color.R, color.G, color.B)));

            bitmap.Save(filename);

            return false;
        }
    }
}
