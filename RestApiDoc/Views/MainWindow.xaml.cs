using RestApiDoc.ViewModels;
using RestApiDoc.Views;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;

namespace RestApiDoc
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel;
        private static double ActualFontSize = 20;

        public MainWindow(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            InitializeComponent();
            DataContext = mainViewModel;
            mainViewModel.PropertyChanged += ChapterViewModel_PropertyChanged;
        }

        private void ChapterViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPartition" && mainViewModel.SelectedPartition is not null)
            {
                SetRtfText(mainViewModel.SelectedPartition.Text);
                mainViewModel.SelectedPartition = null;
                var range = new TextRange(PartitionTextRtb.Selection.Start, PartitionTextRtb.Selection.End);
                range.ApplyPropertyValue(TextElement.FontSizeProperty, ActualFontSize);
            }
            if (e.PropertyName == "SelectedTest" && mainViewModel.SelectedTest is not null)
            {
                IocService.Get<TestsWindow>()?.ShowDialog();
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

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var info = new FileInfo("Data/ReferenceInformation.chm");
                if (info.Exists)
                {
                    System.Windows.Forms.Help.ShowHelp(null, info.FullName);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка открытия справочной информации",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetRtfText(string rtf)
        {
            try
            {
                var stream = new MemoryStream(Encoding.Default.GetBytes(rtf));
                PartitionTextRtb.Selection.Load(stream, DataFormats.Rtf);
            }
            catch
            {
            }
        }

        private async void BtnAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            if (AuthViewModel.AuthUser is null)
            {
                IocService.Get<AuthWindow>()?.ShowDialog();
            }
            else if (AuthViewModel.AuthUser.IsAdmin)
            {
                IocService.Get<AdminWindow>()?.ShowDialog();
                await mainViewModel.Initilize();
            }
        }

        private void BtnInteractive_Click(object sender, RoutedEventArgs e)
        {
            IocService.Get<InteractiveWindow>()?.ShowDialog();
        }

        private async void Window_Initialized(object sender, System.EventArgs e)
        {
            await mainViewModel.Initilize();
            DataLoading.Visibility = Visibility.Collapsed;
        }

        private void BtnLessFontSize_Click(object sender, RoutedEventArgs e)
        {
            if (ActualFontSize < 15)
            {
                return;
            }

            ActualFontSize--;

            var range = new TextRange(PartitionTextRtb.Selection.Start, PartitionTextRtb.Selection.End);
            range.ApplyPropertyValue(TextElement.FontSizeProperty, ActualFontSize);
        }

        private void BtnIncreaseFontSize_Click(object sender, RoutedEventArgs e)
        {
            if (ActualFontSize > 40)
            {
                return;
            }

            ActualFontSize++;

            var range = new TextRange(PartitionTextRtb.Selection.Start, PartitionTextRtb.Selection.End);
            range.ApplyPropertyValue(TextElement.FontSizeProperty, ActualFontSize);
        }
    }
}