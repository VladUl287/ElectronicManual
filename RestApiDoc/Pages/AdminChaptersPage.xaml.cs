using RestApiDoc.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace RestApiDoc.Pages
{
    public partial class AdminChaptersPage : Page
    {
        private readonly TheoryViewModel adminViewModel;

        public AdminChaptersPage(TheoryViewModel adminViewModel)
        {
            InitializeComponent();
            DataContext = adminViewModel;
            this.adminViewModel = adminViewModel;
            adminViewModel.PropertyChanged += AdminViewModel_PropertyChanged;
        }

        private void BtnPartitionUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (adminViewModel.SelectedPartition is not null)
            {
                adminViewModel.SelectedPartition.Text = partitionText.Rtf;

                adminViewModel.EditPartitionCommand.Execute(null);
            }
        }

        private void AdminViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPartition" && adminViewModel.SelectedPartition is not null)
            {
                partitionText.SetRtf(adminViewModel.SelectedPartition.Text);
            }
        }
    }
}