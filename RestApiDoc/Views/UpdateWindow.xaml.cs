using RestApiDoc.ViewModels;
using System.Windows;

namespace RestApiDoc.Views
{
    public partial class UpdateWindow : Window
    {
        private readonly ChapterViewModel chapterViewModel;

        public UpdateWindow(ChapterViewModel chapterViewModel)
        {
            InitializeComponent();
            DataContext = chapterViewModel;
            partitionText.SetRtf(chapterViewModel.SelectedPartition.Text);
            this.chapterViewModel = chapterViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chapterViewModel.SelectedPartition.Text = partitionText.Rtf;

            chapterViewModel.EditPartitionCommand.Execute(null);
        }
    }
}
