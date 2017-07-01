
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImageManager;

using System.Runtime.InteropServices;
using WeightManagment.WeightModel;
using System.Diagnostics;
using TraversalLib;

namespace ViewControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static string Path => StaticResourse.StaticResourses.defaultDirectoryForWeightRepository + "\\testing1.jpg";

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Color[,] arr;
            using (Image img = ImageInnitDialog.InnitImage())
            {
                arr = ImageProcessor.AnalizeRGB(img,2);
            }

            VisualData.VisualDataStorage.Save(arr,Path);

            Close();
            Process.Start(Path);
            
        }


    }
}
