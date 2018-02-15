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
    public abstract partial class MainWindow : Window
    {
        public RGB mycolor { get; set; }
        public Color setcolor { get; set; }           

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

        protected abstract void ButtonOpen_Click();
        protected abstract void ButtonSave_Click();
        protected abstract void ButtonExport_Click();
        protected abstract void AboutOpen_Enter();
        protected abstract void AboutSave_Enter();
        protected abstract void AboutExport_Enter();

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

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.Strokes.Clear();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }       
    }
}
