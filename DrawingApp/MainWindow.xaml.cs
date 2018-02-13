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
    public partial class MainWindow : Window
    {
        public RGB mycolor { get; set; }
        public Color setcolor { get; set; }
        public String imgBuf = @"filebuff.png";   //the path to save (not to preview)
        FileStream myfileStream = null;
        Stream myStream = null;
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

        public MainWindow()
        {
            InitializeComponent();
            mycolor = new RGB
            {
                red = 0,
                green = 0,
                blue = 0
            };
            lbl1.Background = new SolidColorBrush(Color.FromRgb(mycolor.red, mycolor.green, mycolor.blue)); //marker showing the current color
        }

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

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.Strokes.Clear();
        }

        private void AboutOpen_Enter(object sender, RoutedEventArgs e)
        {
            ToolTip tOpen = new ToolTip();
            buttonOpen.ToolTip = tOpen;
            tOpen.Content = "Open file and load to InkCanvas.";
        }

        private void AboutSave_Enter(object sender, RoutedEventArgs e)
        {
            ToolTip tSave = new ToolTip();
            buttonSave.ToolTip = tSave;
            tSave.Content = "Save file to initial directory without bitmap coding for further opening in InkCanvas.";
        }

        private void AboutExport_Enter(object sender, RoutedEventArgs e)
        {
            ToolTip tExport = new ToolTip();
            buttonExport.ToolTip = tExport;
            tExport.Content = "Export file to open in Windows. This will not be avaliable to open in InkCanvas.";
        }

        private void sld_Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            string sliderName = slider.Name;
            double sliderValue = slider.Value;
            switch (sliderName)
            {
                case "sld_RedColor":
                    mycolor.red = Convert.ToByte(sliderValue);
                    break;
                case "sld_GreenColor":
                    mycolor.green = Convert.ToByte(sliderValue);
                    break;
                case "sld_BlueColor":
                    mycolor.blue = Convert.ToByte(sliderValue);
                    break;
            }
            setcolor = Color.FromRgb(mycolor.red, mycolor.green, mycolor.blue);
            lbl1.Background = new SolidColorBrush(Color.FromRgb(mycolor.red, mycolor.green, mycolor.blue));
            inkCanvas1.DefaultDrawingAttributes.Color = setcolor;
        }

        private void sld_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            inkCanvas1.DefaultDrawingAttributes.Width = sld_Thickness.Value;
            inkCanvas1.DefaultDrawingAttributes.Height = sld_Thickness.Value;   
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }       
    }
}
