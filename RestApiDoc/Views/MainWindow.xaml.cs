using RestApiDoc.Controls;
using RestApiDoc.ViewModels;
using RestApiDoc.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RestApiDoc
{
    public partial class MainWindow : Window
    {
        private readonly ChapterViewModel chapterViewModel;
        private readonly LoginViewModel loginViewModel;
        private readonly RegisterViewModel registerViewModel;
        private readonly UserViewModel userViewModel;

        public MainWindow(ChapterViewModel chapterViewModel, LoginViewModel loginViewModel, RegisterViewModel registerViewModel, UserViewModel userViewModel)
        {
            InitializeComponent();
            this.chapterViewModel = chapterViewModel;
            this.loginViewModel = loginViewModel;
            this.registerViewModel = registerViewModel;
            this.userViewModel = userViewModel;
            Initialize();

        }

        private void Initialize()
        {
            for (int i = 0; i < chapterViewModel.Chapters.Count; i++)
            {
                var partitions = chapterViewModel.Chapters[i].Partitions.Select(e => new SubItem(e.Name, new UserControlPartitionText(e.Text)));
                var chapter = new ItemMenu(chapterViewModel.Chapters[i].Name, partitions);
                Menu.Children.Add(new UserControlMenuItem(chapter, this));
            }
        }

        internal void SwitchScreen(object sender)
        {
            var screen = (UserControl)sender;

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }

        private void BtnAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            var adminWindow = new AdminWindow(chapterViewModel, userViewModel);
            //var adminWindow = new AuthWindow(userViewModel, registerViewModel);
            adminWindow.ShowDialog();
        }
    }
}