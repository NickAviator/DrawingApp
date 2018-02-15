using System;
using System.Windows;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace WpfApp1
{
    public class FileDialog : MainWindow
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

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
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

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
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

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == true)
            {
                try
                {
                    myfileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas1.ActualWidth, (int)inkCanvas1.ActualHeight, 96d, 96d, PixelFormats.Default);
                    rtb.Render(inkCanvas1);

                    //This encoding makes unavaliable to open image in inkCanvas1 
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

        protected override void ButtonOpen_Click()
        {
            throw new NotImplementedException();
        }

        protected override void ButtonSave_Click()
        {
            throw new NotImplementedException();
        }

        protected override void ButtonExport_Click()
        {
            throw new NotImplementedException();
        }

        protected override void AboutOpen_Enter()
        {
            throw new NotImplementedException();
        }

        protected override void AboutSave_Enter()
        {
            throw new NotImplementedException();
        }

        protected override void AboutExport_Enter()
        {
            throw new NotImplementedException();
        }
    }
}
