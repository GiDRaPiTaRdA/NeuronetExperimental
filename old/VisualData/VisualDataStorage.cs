﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace VisualData
{
    public static class VisualDataStorage
    {
        public static void Save(byte[,] bytes, string filename)
        {
            Bitmap bitmap = new Bitmap(bytes.GetLength(0), bytes.GetLength(1));

            TraversalLib.BinaryArrayTraversal.Traversal(bytes, (b, x, y) => bitmap.SetPixel(x, y, Color.FromArgb(255,b,b,b)));

            bitmap.Save(filename);
        }

        public static void Save(Color[,] colors, string filename)
        {
            Bitmap bitmap = new Bitmap(colors.GetLength(0), colors.GetLength(1));

            TraversalLib.BinaryArrayTraversal.Traversal(colors, (color, x, y) => bitmap.SetPixel(x, y, Color.FromArgb(255, color.R, color.G, color.B)));

            bitmap.Save(filename);
        }

        public static void Save(Bitmap bitmap, string filename)
        {
            bitmap.Save(filename);
        }
    }
}
