using RestApiDoc.Controls;
using RestApiDoc.ViewModels;
using RestApiDoc.Views;
using System.IO;
using System.Text;
using System.Windows;

namespace RestApiDoc
{
    public partial class MainWindow : Window
    {
        private readonly ChapterViewModel chapterViewModel;
        private readonly UserViewModel userViewModel;

        public MainWindow(ChapterViewModel chapterViewModel, UserViewModel userViewModel)
        {
            InitializeComponent();
            DataContext = chapterViewModel;
            chapterViewModel.PropertyChanged += ChapterViewModel_PropertyChanged;

            this.chapterViewModel = chapterViewModel;
            this.userViewModel = userViewModel;
        }

        private void ChapterViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPartition")
            {
                var stream = new MemoryStream(Encoding.Default.GetBytes(chapterViewModel.SelectedPartition.Text));
                PartitionText.Selection.Load(stream, DataFormats.Rtf);
            }
            if (e.PropertyName == "SelectedTest" && chapterViewModel.SelectedTest is not null)
            {
                new TestsWindow(chapterViewModel).ShowDialog();
                chapterViewModel.SelectedTest = null;
            }
        }

        private void BtnAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow(chapterViewModel, userViewModel).ShowDialog();
        }

        private void BtnSideBarToggle_Click(object sender, RoutedEventArgs e)
        {
            switch (SideBarMenu.Visibility)
            {
                case Visibility.Visible:
                    SideBarMenu.Visibility = Visibility.Collapsed;
                    break;
                case Visibility.Collapsed:
                    SideBarMenu.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void BtnManagment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProgrammInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}