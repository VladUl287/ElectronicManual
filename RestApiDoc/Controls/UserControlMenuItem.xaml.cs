using RestApiDoc.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace RestApiDoc.Controls
{
    public partial class UserControlMenuItem : UserControl
    {
        readonly MainWindow? _context;
        public UserControlMenuItem(ItemMenu itemMenu)
        {
            InitializeComponent();

            Init(itemMenu);
        }

        public UserControlMenuItem(ItemMenu itemMenu, MainWindow context)
        {
            InitializeComponent();

            _context = context;

            Init(itemMenu);
        }

        private void Init(ItemMenu itemMenu)
        {
            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            DataContext = itemMenu;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _context?.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
        }
    }
}
