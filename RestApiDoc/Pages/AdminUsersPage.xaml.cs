using RestApiDoc.ViewModels;
using System.Windows.Controls;

namespace RestApiDoc.Pages
{
    public partial class AdminUsersPage : Page
    {
        public AdminUsersPage(UserViewModel userViewModel)
        {
            InitializeComponent();
            DataContext = userViewModel;
        }
    }
}