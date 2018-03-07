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
        public MainWindow()
        {
            SelfRef = this;
            InitializeComponent();
        }

        public static MainWindow SelfRef    // link on this private components to other classes
        {
            get; set;
        }

        private void AboutOpen_Enter(object sender, RoutedEventArgs e)
        {
            ToolTip tOpen = new ToolTip();
            ItemOpen.ToolTip = tOpen;
            tOpen.Content = "Open file and load to InkCanvas.";
        }

        private void AboutSave_Enter(object sender, RoutedEventArgs e)
        {
            ToolTip tSave = new ToolTip();
            ItemSave.ToolTip = tSave;
            tSave.Content = "Save file to initial directory without bitmap coding for further opening in InkCanvas.";
        }

        private void AboutExport_Enter(object sender, RoutedEventArgs e)
        {
            ToolTip tExport = new ToolTip();
            ItemExport.ToolTip = tExport;
            tExport.Content = "Export file to open in Windows. This will not be avaliable to open in InkCanvas.";
        }        

        private void EditClear_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas1.Strokes.Clear();
        }

        private void ItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }       

        private void ToolsOpen_Click(object sender, RoutedEventArgs e)
        {
            Palette obj = new Palette();
            obj.Show();
        }
    }
}
