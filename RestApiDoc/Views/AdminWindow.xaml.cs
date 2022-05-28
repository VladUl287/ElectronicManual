using RestApiDoc.Pages;
using RestApiDoc.ViewModels;
using System.Windows;

namespace RestApiDoc.Views
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            BtnChapterPage_Click(null, null);
        }

        private void BtnChapterPage_Click(object? sender, RoutedEventArgs? e)
        {
            var page = IocService.Get<AdminChaptersPage>();
            frame.Navigate(page);
        }

        private void BtnTestsPage_Click(object sender, RoutedEventArgs e)
        {
            var page = IocService.Get<AdminTestsPage>();
            frame.Navigate(page);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var page = IocService.Get<AdminUsersPage>();
            frame.Navigate(page);
        }
    }
}