using System;
using System.Drawing;
using System.Windows.Forms;
using StaticResourse;

namespace ImageManager
{
    public static class ImageInnitDialog
    {
        private static string OpenFileDialogInnitImage()
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Title = "Select image";
            ofDialog.InitialDirectory = StaticResourses.innitialDirectoryForOpenFileDialog;
            ofDialog.Multiselect = false;
            ofDialog.ShowDialog();

            return ofDialog.FileName;
        }
        public static Image InnitImage(string path)
        {
            Image image = null;
            try
            {
                image = Image.FromFile(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return image;

        }
        public static Image InnitImage()
        {
            return InnitImage(OpenFileDialogInnitImage());
        }
    }
}
