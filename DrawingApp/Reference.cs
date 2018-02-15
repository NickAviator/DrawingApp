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
    public class Reference : MainWindow
    {
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
