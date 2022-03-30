using RestApiDoc.ViewModels;
using System.Windows;

namespace RestApiDoc.Views
{
    public partial class UpdateWindow : Window
    {
        public UpdateWindow(ChapterViewModel chapterViewModel)
        {
            InitializeComponent();
            DataContext = chapterViewModel;
            partitionName.Text = chapterViewModel.SelectedPartition.Name;
            partitionText.SetRtf(chapterViewModel.SelectedPartition.Text);
        }
    }
}
