using RestApiDoc.Controls;
using RestApiDoc.ViewModels;
using RestApiDoc.Views;
using System.IO;
using System.Linq;
using System.Text;
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
            DataContext = chapterViewModel;
            chapterViewModel.PropertyChanged += (e, args) =>
            {
                if (args.PropertyName == "SelectedPartition")
                {
                    var stream = new MemoryStream(Encoding.Default.GetBytes(chapterViewModel.SelectedPartition.Text));
                    //PartitionText.Selection.Load(stream, DataFormats.Rtf);
                }
            };
            chapterViewModel.PropertyChanged += (e, args) =>
            {
                if (args.PropertyName == "SelectedTest" && chapterViewModel.SelectedTest is not null)
                {
                    var stream = new TestsWindow(chapterViewModel);
                    stream.ShowDialog();
                    chapterViewModel.SelectedTest = null;
                }
            };
            this.loginViewModel = loginViewModel;
            this.registerViewModel = registerViewModel;
            this.userViewModel = userViewModel;

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