using RestApiDoc.ViewModels;
using RestApiDoc.Views;
using System;
using System.Diagnostics;
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
            var stream = new MemoryStream(Encoding.Default.GetBytes(rtf));
            PartitionTextRtb.Selection.Load(stream, DataFormats.Rtf);
        }

        private void BtnAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            if (LoginViewModel.AuthUser is null)
            {
                IocService.Get<AuthWindow>()?.ShowDialog();
            }
            else if(LoginViewModel.AuthUser.IsAdmin)
            {
                IocService.Get<AdminWindow>()?.ShowDialog();
            }
        }
    }
}