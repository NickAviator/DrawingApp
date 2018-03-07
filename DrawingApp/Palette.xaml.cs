using System;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class Palette : Window
    {
        public RGB mycolor { get; set; }
        public Color setcolor { get; set; }

        public Palette()
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
            MainWindow.SelfRef.inkCanvas1.DefaultDrawingAttributes.Color = setcolor;
        }

        private void sld_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            MainWindow.SelfRef.inkCanvas1.DefaultDrawingAttributes.Width = sld_Thickness.Value;
            MainWindow.SelfRef.inkCanvas1.DefaultDrawingAttributes.Height = sld_Thickness.Value;
        }
    }
}
