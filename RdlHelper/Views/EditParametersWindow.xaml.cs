using System;
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
using RdlHelper.ViewModels.ReportParameterViewModels;

namespace RdlHelper.Views
{
    /// <summary>
    /// Interaction logic for ParametersBatchCommandsWindow.xaml
    /// </summary>
    public partial class EditParametersWindow : Window
    {
        private EditParametersVm _pbcwVm;

        internal EditParametersWindow(EditParametersVm vm)
        {
            _pbcwVm = vm;
            DataContext = vm;
            InitializeComponent();
            Height = .9 * SystemParameters.PrimaryScreenHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
         
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                var dc = btn.DataContext;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            _pbcwVm.ApplyChanges();
        }

        private void AddDefaultValue_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                var dc = (ParameterVm)btn.DataContext;
                dc.DefaultValues.Add(new DefaultValueVm(""));
            }
        }
    }
}
