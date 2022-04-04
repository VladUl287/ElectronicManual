using RestApiDoc.Database.Models;
using RestApiDoc.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RestApiDoc.Views
{
    public partial class AdminWindow : Window
    {
        private readonly ChapterViewModel chapterViewModel;

        public AdminWindow(ChapterViewModel chapterViewModel, UserViewModel userViewModel)
        {
            InitializeComponent();
            DataContext = chapterViewModel;
            chapterViewModel.PropertyChanged += ChapterViewModel_PropertyChanged;

            UsersListParent.DataContext = userViewModel;
            this.chapterViewModel = chapterViewModel;

            SingeRadioBtn.IsChecked = true;
        }

        private void ChapterViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedQuestion")
            {
                if (chapterViewModel.SelectedQuestion.IsMultiple)
                {
                    MultipleRadioBtn.IsChecked = false;
                    MultipleRadioBtn.IsChecked = true;
                }
                else if (chapterViewModel.SelectedQuestion.IsUserAnswer)
                {
                    UserAnswerRadioBtn.IsChecked = false;
                    UserAnswerRadioBtn.IsChecked = true;
                }
                else
                {
                    SingeRadioBtn.IsChecked = false;
                    SingeRadioBtn.IsChecked = true;
                }
            }
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

            new UpdateWindow(chapterViewModel).ShowDialog();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (PanelAnswers is null)
            {
                return;
            }

            var radio = (RadioButton)sender;

            PanelAnswers.Children.Clear();

            if (radio.Name == "UserAnswerRadioBtn")
            {
                var answer = new Answer();
                var textBox = new TextBox();

                var bindingText = new Binding("Text")
                {
                    Source = answer
                };
                textBox.SetBinding(TextBox.TextProperty, bindingText);

                PanelAnswers.Children.Add(textBox);

                chapterViewModel.NewQuestion.Answers.Add(answer);
            }
            else if (radio.Name == "MultipleRadioBtn")
            {
                var listAnswers = new List<Answer>();

                for (int i = 0; i < 4; i++)
                {
                    var answer = new Answer
                    {
                        Text = $"Answer {i + 1} check"
                    };
                    var textBox = new TextBox();
                    var checkBox = new CheckBox
                    {
                        Content = textBox
                    };

                    var bindingText = new Binding("Text")
                    {
                        Source = answer
                    };
                    textBox.SetBinding(TextBox.TextProperty, bindingText);
                    var bindingRight = new Binding("IsRight")
                    {
                        Source = answer
                    };
                    checkBox.SetBinding(CheckBox.IsCheckedProperty, bindingRight);

                    PanelAnswers.Children.Add(checkBox);

                    listAnswers.Add(answer);
                }

                chapterViewModel.NewQuestion.Answers.AddRange(listAnswers);
            }
            else
            {
                var listAnswers = new List<Answer>();

                for (int i = 0; i < 4; i++)
                {
                    var answer = new Answer
                    {
                        Text = $"Answer {i + 1} check"
                    };
                    var textBox = new TextBox();
                    var radioBtn = new RadioButton
                    {
                        GroupName = "RadioAnswers",
                        Content = textBox,
                    };

                    var bindingText = new Binding("Text")
                    {
                        Source = answer
                    };
                    textBox.SetBinding(TextBox.TextProperty, bindingText);
                    var bindingRight = new Binding("IsRight")
                    {
                        Source = answer
                    };
                    radioBtn.SetBinding(RadioButton.IsCheckedProperty, bindingRight);

                    PanelAnswers.Children.Add(radioBtn);

                    listAnswers.Add(answer);
                }

                chapterViewModel.NewQuestion.Answers.Clear();
                chapterViewModel.NewQuestion.Answers.AddRange(listAnswers);
            }
        }
    }
}