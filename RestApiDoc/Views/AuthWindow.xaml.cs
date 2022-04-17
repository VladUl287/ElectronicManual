using RestApiDoc.ViewModels;
using System.Windows;

namespace RestApiDoc.Views
{
    public partial class AuthWindow : Window
    {
        public AuthWindow(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            loginForm.DataContext = loginViewModel;
        }
    }
}