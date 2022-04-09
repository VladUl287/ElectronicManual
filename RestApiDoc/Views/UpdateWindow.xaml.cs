using RestApiDoc.ViewModels;
using System.Windows;

namespace RestApiDoc.Views
{
    public partial class UpdateWindow : Window
    {
        private readonly AdminViewModel chapterViewModel;

        public UpdateWindow(AdminViewModel chapterViewModel)
        {
            InitializeComponent();
            if (chapterViewModel.SelectedPartition is null)
            {
                Close();
            }

            DataContext = chapterViewModel;
            partitionText.SetRtf(chapterViewModel.SelectedPartition!.Text);
            this.chapterViewModel = chapterViewModel;
        }

        private void BtnUpdatePartition_Click(object sender, RoutedEventArgs e)
        {
            if (chapterViewModel.SelectedPartition is not null)
            {
                chapterViewModel.SelectedPartition.Text = partitionText.Rtf;

                chapterViewModel.EditPartitionCommand.Execute(null);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (chapterViewModel.SelectedPartition is not null)
            {
                chapterViewModel.DeletePartitionCommand.Execute(null);
            }
            Close();
        }
    }
}
