using RdlHelper.ViewModels;
using System.Windows;

namespace RdlHelper.Views
{
    /// <summary>
    /// Interaction logic for FileSelectorWindow.xaml
    /// </summary>
    public partial class FileSelectorWindow : Window
    {
        private FileSelectorVm _fileSelectorVm;

        internal FileSelectorWindow(FileSelectorVm fileSelectorVm)
        {
            _fileSelectorVm = fileSelectorVm;
            DataContext = _fileSelectorVm;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _fileSelectorVm.SelectedItem = null;
            Close();
        }
    }
}
