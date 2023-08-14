using RdlHelper.ViewModels;
using RdlHelper.ViewModels.RdlCommands;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private void CommandCard_Drop(object sender, DragEventArgs e)
        {
            var droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

            var filteredFilePaths = droppedFiles.Where(aa => System.IO.Path.GetExtension(aa) == ".rdl");

            if (!filteredFilePaths.Any())
            {
                _mainVm.Message = "no .rdl files";
                return;
            }

            var control = (FrameworkElement)e.OriginalSource;
            if (control.DataContext is RdlCommand rdlCommand)
            {
                var result = rdlCommand.Perform(filteredFilePaths);
                _mainVm.Message = result;
            }
        }
         
         
    }
}
