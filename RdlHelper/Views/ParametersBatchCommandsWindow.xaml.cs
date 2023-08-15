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

namespace RdlHelper.Views
{
    /// <summary>
    /// Interaction logic for ParametersBatchCommandsWindow.xaml
    /// </summary>
    public partial class DefaultParamsWindow : Window
    {
        private EditParametersVm _pbcwVm;

        internal DefaultParamsWindow(EditParametersVm vm)
        { 
            DataContext = vm;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                var dc = (ReportParameterVm)btn.DataContext;
                dc.DefaultValues.Add(new ParameterDefaultValue(""));
            }
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
            _pbcwVm.ApplyChanges();
        }
    }
}
