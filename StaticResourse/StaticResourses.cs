using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StaticResourse
{
    public class StaticResourses
    {

        public static string SolutionDirectroy => Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        public static string InnitialDirectoryForOpenFileDialog => SolutionDirectroy+@"\YDATA\images";
        public static string DefaultDirectoryForWeightRepository => SolutionDirectroy + @"\YDATA\weights";
    }
}
