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

namespace RdlHelper.Views.EditParameter
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is ParameterVm pvm)
            {
                pvm.DefaultValues.Add(new DefaultValueVm(null));
                pvm.DefaultValueType = Models.ValueProvidingType.Literals;
            }
        }

        private void _orderUp_Click(object sender, RoutedEventArgs e)
        { 
            var allSelectedItems = mainDg.SelectedItems.Cast<ParameterVm>().ToList();
            var latestOrderOccupied = 0;
            foreach (var item in allSelectedItems)
            {
                var ind1 = _pbcwVm.Parameters.IndexOf(item);

                if (ind1 == latestOrderOccupied)
                {
                    latestOrderOccupied++;
                    continue;
                }

                _pbcwVm.Parameters.RemoveAt(ind1);
                var replacedItem = _pbcwVm.Parameters[ind1 - 1];
                _pbcwVm.Parameters.Insert(ind1 - 1,item);
                item.Order--;
                replacedItem.Order++;
                mainDg.SelectedItems.Add(item);
            }

            //var sp = _pbcwVm.SelectedParameter;
            //var ind = _pbcwVm.Parameters.IndexOf(sp);

            //if (ind == 0)
            //{
            //    return;
            //}

            //var spAbove = _pbcwVm.Parameters[ind - 1];
            //spAbove.Order++;

            //sp.Order--;

            //_pbcwVm.Parameters.RemoveAt(ind);
            //_pbcwVm.Parameters.Insert(ind - 1, sp);
            //_pbcwVm.SelectedParameter = sp;
        }

        private void _orderDown_Click(object sender, RoutedEventArgs e)
        {
            var sp = _pbcwVm.SelectedParameter;
            var ind = _pbcwVm.Parameters.IndexOf(sp); 

            if (ind == _pbcwVm.Parameters.Count - 1)
            {
                return;
            }

            var spUnder = _pbcwVm.Parameters[ind  + 1];
            spUnder.Order--;

            sp.Order++;

            _pbcwVm.Parameters.RemoveAt(ind);
            _pbcwVm.Parameters.Insert(ind + 1, sp);
            _pbcwVm.SelectedParameter = sp;
        }

        private void _removeDefaultValue_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DefaultValueVm vpm)
            {
                
            }
        }
    }
}
