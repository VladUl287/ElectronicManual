using RestApiDoc.Database.Models;
using RestApiDoc.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace RestApiDoc.Views
{
    public partial class AdminWindow : Window
    {
        private readonly ChapterViewModel chapterViewModel;

        public AdminWindow(ChapterViewModel chapterViewModel, UserViewModel userViewModel)
        {
            InitializeComponent();
            DataContext = chapterViewModel;
            UsersListParent.DataContext = userViewModel;
            UsersList.DataContext = userViewModel;
            this.chapterViewModel = chapterViewModel;
        }

        private void BtnCreatePartition_Click(object sender, RoutedEventArgs e)
        {
            var partition = new Partition
            {
                Name = partitionName.Text,
                Text = partitionText.Rtf,
                ChapterId = chapterViewModel.SelectedChapter.Id
            };

            chapterViewModel.AddPartitionCommand.Execute(partition);
        }

        private void BtnPartition_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ListBoxItem)partitionsList.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;
            var updateWindow = new UpdateWindow(chapterViewModel);
            updateWindow.ShowDialog();
        }
    }
}