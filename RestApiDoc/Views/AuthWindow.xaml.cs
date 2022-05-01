using RestApiDoc.ViewModels;
using System.Windows;

namespace RestApiDoc.Views
{
    public partial class AuthWindow : Window
    {
        private readonly AuthViewModel authViewModel;

        public AuthWindow(AuthViewModel authViewModel)
        {
            InitializeComponent();
            DataContext = authViewModel;
            this.authViewModel = authViewModel;
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            authViewModel.LoginCommand.Execute(null);
            if (AuthViewModel.AuthUser is not null)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Некорректные данные!", "Ошибка авторизации", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}