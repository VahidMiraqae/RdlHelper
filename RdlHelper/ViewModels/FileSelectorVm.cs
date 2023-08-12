using System.Collections.Generic;
using System.Linq;

namespace RdlHelper.ViewModels
{
    internal class FileSelectorVm : BaseVm
    {
        private string _selectedItem;

        public FileSelectorVm(IEnumerable<string> filePaths)
        {
            Items = filePaths.ToList();
            SelectedItem = Items.FirstOrDefault();
        }

        public List<string> Items { get; set; }
        public string SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropChanged(); } }
    }
}