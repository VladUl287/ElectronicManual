using RestApiDoc.Models;
using RestApiDoc.ViewModels;
using System.Windows;

namespace RestApiDoc.Views
{
    public partial class CreateWindow : Window
    {
        private readonly ChapterViewModel chapterViewModel;

        public CreateWindow(ChapterViewModel chapterViewModel)
        {
            InitializeComponent();
            DataContext = chapterViewModel;
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
            var updateWindow = new UpdateWindow(chapterViewModel);
            updateWindow.ShowDialog();
        }
    }
}