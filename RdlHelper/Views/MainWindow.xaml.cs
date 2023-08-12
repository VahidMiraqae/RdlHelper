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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using RdlHelper.ViewModels;

namespace RdlHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainVm _mainVm;

        public MainWindow()
        {
            InitializeComponent();

            _mainVm = new MainVm();
            DataContext = _mainVm;
        }
          
        private void WrapPanel_Drop(object sender, DragEventArgs e)
        {
            var droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

            var bbb = droppedFiles.Where(aa => System.IO.Path.GetExtension(aa) == ".rdl");

            var control = (FrameworkElement)e.OriginalSource;
            if (control.DataContext is RdlCommand rdlCommand)
            {
                rdlCommand.Perform(bbb);
                _mainVm.Message = rdlCommand.Notify();
            }
        }
    }
}
