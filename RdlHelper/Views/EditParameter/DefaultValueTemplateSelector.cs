using RdlHelper.ViewModels.ReportParameterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RdlHelper.Views.EditParameter
{
    public class DefaultValueTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = (FrameworkElement)container;
            if (item != null && item is ParameterVm pvm)
            {
                switch (pvm.DefaultValueType)
                {
                    case Models.ValueProvidingType.None:
                        return element.TryFindResource("NoDefaultValueTemplate") as DataTemplate;
                        break;
                    case Models.ValueProvidingType.Literals:
                        return element.TryFindResource("LiternalsTemplate") as DataTemplate;
                        break;
                    case Models.ValueProvidingType.FromQuery:
                        return element.TryFindResource("QueryDVTemplate") as DataTemplate;
                        break;
                    default:
                        break;
                } 
            }
            return null;
        }
    }
}
