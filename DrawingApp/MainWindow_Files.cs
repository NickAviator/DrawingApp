using System;
using System.Windows;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Runtime;

namespace WpfApp1
{
    public partial class MainWindow 
    {
        FileStream myfileStream = null;
        Stream myStream = null;
        public String imgBuf = @"filebuff.png";   //the path to save (not to preview)  

        OpenFileDialog openFileDialog1 = new OpenFileDialog
        {
            InitialDirectory = Directory.GetCurrentDirectory(),
            Filter = "All files (*.*)|*.*|PNG (*.png)|*.png",
            FilterIndex = 2,
            RestoreDirectory = true
        };

        SaveFileDialog saveFileDialog1 = new SaveFileDialog
        {
            InitialDirectory = Directory.GetCurrentDirectory(),
            Filter = "JPEG (*.jpg)|*.jpg|BMP (*.bmp*)|*.bmp|PNG (*.png)|*.png|GIF (*.gif)|*.gif",
            FilterIndex = 2,
            RestoreDirectory = true
        };

        private void ItemOpen_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    myStream = openFileDialog1.OpenFile();
                    StrokeCollection strokes = new StrokeCollection(myStream);
                    inkCanvas1.Strokes = strokes;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Unable to upload file. Original error: " + ex.Message);
                }
                finally
                {
                    myStream.Close();
                }
            }
        }

        private void ItemSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myfileStream = new FileStream(imgBuf, FileMode.Create);
                inkCanvas1.Strokes.Save(myfileStream);
                MessageBox.Show("Picture was successfully saved!");
            }
            catch
            {
                MessageBox.Show("Unable to save file. Check the path.");
            }
            finally
            {
                myfileStream.Close();
            }
        }

        private void ItemExport_Click(object sender, RoutedEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == true)
            {
                try
                {
                    myfileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas1.ActualWidth, (int)inkCanvas1.ActualHeight, 96d, 96d, PixelFormats.Default);
                    rtb.Render(inkCanvas1);

                    PngBitmapEncoder pngEnc = new PngBitmapEncoder();
                    pngEnc.Frames.Add(BitmapFrame.Create(rtb));
                    pngEnc.Save(myfileStream);
                    MessageBox.Show("Picture was successfully exported!");
                }
                catch
                {
                    MessageBox.Show("Unable to export file. Check the path.");
                }
                finally
                {
                    myfileStream.Close();
                }
            }
        }
    }
}
