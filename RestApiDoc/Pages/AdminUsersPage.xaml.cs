using RestApiDoc.ViewModels;
using System.Windows.Controls;

namespace RestApiDoc.Pages
{
    public partial class AdminUsersPage : Page
    {
        public AdminUsersPage()
        {
            InitializeComponent();
            var userViewModel = IocService.Get<UserViewModel>();
            DataContext = userViewModel;
            userViewModel?.Initilize();
        }
    }
}