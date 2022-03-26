using System.Collections.Generic;
using System.Windows.Controls;

namespace RestApiDoc.ViewModels
{
    public class ItemMenu
    {
        public ItemMenu(string header, IEnumerable<SubItem> subItems)
        {
            Header = header;
            SubItems = subItems;
        }

        public string Header { get; private set; }
        public IEnumerable<SubItem> SubItems { get; private set; }
        public UserControl Screen { get; private set; }
    }
}