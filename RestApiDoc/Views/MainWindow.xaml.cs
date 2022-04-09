using RestApiDoc.ViewModels;
using RestApiDoc.Views;
using System.IO;
using System.Text;
using System.Windows;

namespace RestApiDoc
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            this.mainViewModel = mainViewModel;
            mainViewModel.PropertyChanged += ChapterViewModel_PropertyChanged;
        }

        private void ChapterViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPartition" && mainViewModel.SelectedPartition is not null)
            {
                SetRtfText(mainViewModel.SelectedPartition.Text);
            }
            if (e.PropertyName == "SelectedTest" && mainViewModel.SelectedTest is not null)
            {
                IocService.Get<TestsWindow>()?.ShowDialog();
                //new TestsWindow(mainViewModel).ShowDialog();
                mainViewModel.SelectedTest = null;
            }
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
            }
        }

        private void BtnManagment_Click(object sender, RoutedEventArgs e)
        {
            OpenAdditionalFile("Data/Additional/managment.rtf");
        }

        private void BtnProgrammInfo_Click(object sender, RoutedEventArgs e)
        {
            OpenAdditionalFile("Data/Additional/managment.rtf");
        }

        private void SetRtfText(string rtf)
        {
            var stream = new MemoryStream(Encoding.Default.GetBytes(rtf));
            PartitionTextRtb.Selection.Load(stream, DataFormats.Rtf);
        }

        private void OpenAdditionalFile(string name)
        {
            if (File.Exists(name))
            {
                using var fs = new FileStream(name, FileMode.Open);
                PartitionTextRtb.Selection.Load(fs, DataFormats.Rtf);
            }
        }

        private void BtnAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            IocService.Get<AdminWindow>()?.ShowDialog();
        }
    }
}