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
            partitionText.SetRtf(chapterViewModel.SelectedPartition.Text);
        }
    }
}
