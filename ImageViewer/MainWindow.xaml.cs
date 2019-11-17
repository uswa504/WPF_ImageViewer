using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;


namespace ImageViewer{

    public partial class MainWindow : Window
    {
        BitmapImage image;
        TransformedBitmap trans_iamge;
        string selectedFileName;
        int Rotate_angle = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "F:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                selectedFileName = dlg.FileName;
                textview.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                image=bitmap;
                image_viewer.Source = bitmap;
            }
        }
      
        private void btn_rotate_Click(object sender, RoutedEventArgs e)
        {
            TransformedBitmap TempImage = new TransformedBitmap();
            if (textview.Text != "")
            {

                TempImage.BeginInit();
                TempImage.Source = image;
                RotateTransform transform = new RotateTransform(Rotate_angle += 90);
                TempImage.Transform = transform;
                TempImage.EndInit();
                trans_iamge = TempImage;
                image_viewer.Source = TempImage;
            }
            else
                message_box.Text = "Open the image!";
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (textview.Text != "")
            {
                Save();
            }
            else
                message_box.Text = "Open the image!";
        }
        public void Save()
        {
            String filePath = @"F:\Pictures\picture.jpg";
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)trans_iamge));
            using (FileStream stream = new FileStream(filePath, FileMode.Create)) encoder.Save(stream);
            message_box.Text = "Image Saved..!";
        }

       
    }
        
}
