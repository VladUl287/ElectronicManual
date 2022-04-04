using RestApiDoc.Database.Models;
using RestApiDoc.ViewModels;
using System.Collections.Generic;
using System.Linq;
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
            UsersList.DataContext = userViewModel;
            this.chapterViewModel = chapterViewModel;
        }

        private void ChapterViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedQuestion")
            {
                if (!chapterViewModel.SelectedQuestion.IsMultiple && !chapterViewModel.SelectedQuestion.IsUserAnswer)
                {
                    SingeRadioBtn.IsChecked = false;
                    SingeRadioBtn.IsChecked = true;
                }
                if (chapterViewModel.SelectedQuestion.IsMultiple)
                {
                    MultipleRadioBtn.IsChecked = false;
                    MultipleRadioBtn.IsChecked = true;
                }
                if (chapterViewModel.SelectedQuestion.IsUserAnswer)
                {
                    UserAnswerRadioBtn.IsChecked = false;
                    UserAnswerRadioBtn.IsChecked = true;
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
            var updateWindow = new UpdateWindow(chapterViewModel);
            updateWindow.ShowDialog();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            PanelAnswers.Children.Clear();

            var radio = (RadioButton)sender;
            if (radio.Name == "UserAnswerRadioBtn")
            {
                if (!chapterViewModel.SelectedQuestion.IsUserAnswer)
                {
                    chapterViewModel.SelectedQuestion.IsMultiple = false;
                    chapterViewModel.SelectedQuestion.IsUserAnswer = true;

                    var answer = new Answer
                    {
                        QuestionId = chapterViewModel.SelectedQuestion.Id
                    };

                    var textBox = new TextBox
                    {
                        DataContext = answer
                    };

                    var binding = new Binding("Text")
                    {
                        Source = answer
                    };
                    textBox.SetBinding(TextBlock.TextProperty, binding);

                    PanelAnswers.Children.Add(textBox);

                    var ans = chapterViewModel.SelectedQuestion.Answers.ToList();
                    chapterViewModel.DeleteAnswersCommand.Execute(ans);

                    chapterViewModel.SelectedQuestion.Answers.Clear();
                    chapterViewModel.SelectedQuestion.Answers.Add(answer);
                }
                else
                {
                    var answer = chapterViewModel.SelectedQuestion.Answers.FirstOrDefault();

                    var textBox = new TextBox
                    {
                        Text = answer.Text,
                        DataContext = answer
                    };

                    PanelAnswers.Children.Add(textBox);
                }
            }
            else if (radio.Name == "MultipleRadioBtn")
            {
                if (!chapterViewModel.SelectedQuestion.IsMultiple)
                {
                    chapterViewModel.SelectedQuestion.IsMultiple = true;
                    chapterViewModel.SelectedQuestion.IsUserAnswer = false;

                    var listAnswers = new List<Answer>();

                    for (int i = 0; i < 4; i++)
                    {
                        var answer = new Answer
                        {
                            Text = $"Answer {i + 1} check",
                            QuestionId = chapterViewModel.SelectedQuestion.Id
                        };

                        var textBox = new TextBox();

                        var checkBox = new CheckBox
                        {
                            DataContext = answer,
                            Content = textBox,
                        };

                        var binding = new Binding("Text")
                        {
                            Source = answer
                        };
                        textBox.SetBinding(TextBox.TextProperty, binding);

                        checkBox.Checked += CheckBox_Checked;
                        checkBox.Unchecked += CheckBox_UnChecked;

                        PanelAnswers.Children.Add(checkBox);

                        listAnswers.Add(answer);
                    }

                    var ans = chapterViewModel.SelectedQuestion.Answers.ToList();
                    chapterViewModel.DeleteAnswersCommand.Execute(ans);

                    chapterViewModel.SelectedQuestion.Answers.Clear();
                    chapterViewModel.SelectedQuestion.Answers.AddRange(listAnswers);
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var answer = chapterViewModel.SelectedQuestion.Answers[i];

                        var textBox = new TextBox();

                        var checkBox = new CheckBox()
                        {
                            DataContext = answer,
                            Content = textBox,
                            IsChecked = answer.IsRight
                        };

                        var binding = new Binding("Text")
                        {
                            Source = answer
                        };
                        textBox.SetBinding(TextBox.TextProperty, binding);

                        checkBox.Checked += CheckBox_Checked;
                        checkBox.Unchecked += CheckBox_UnChecked;

                        PanelAnswers.Children.Add(checkBox);
                    }
                }
            }
            else
            {
                if (chapterViewModel.SelectedQuestion.IsMultiple || chapterViewModel.SelectedQuestion.IsUserAnswer)
                {
                    chapterViewModel.SelectedQuestion.IsMultiple = false;
                    chapterViewModel.SelectedQuestion.IsUserAnswer = false;

                    var listAnswers = new List<Answer>();

                    for (int i = 0; i < 4; i++)
                    {
                        var answer = new Answer
                        {
                            Text = $"Answer {i + 1} radio",
                            QuestionId = chapterViewModel.SelectedQuestion.Id
                        };

                        var textBox = new TextBox();

                        var checkBox = new RadioButton
                        {
                            Content = textBox,
                            DataContext = answer,
                            GroupName = "RadioAnswers",
                        };

                        var binding = new Binding("Text")
                        {
                            Source = answer
                        };
                        textBox.SetBinding(TextBox.TextProperty, binding);

                        checkBox.Checked += RadioBtn_Checked;

                        PanelAnswers.Children.Add(checkBox);

                        listAnswers.Add(answer);
                    }

                    var ans = chapterViewModel.SelectedQuestion.Answers.ToList();
                    chapterViewModel.DeleteAnswersCommand.Execute(ans);

                    chapterViewModel.SelectedQuestion.Answers.Clear();
                    chapterViewModel.SelectedQuestion.Answers.AddRange(listAnswers);
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var answer = chapterViewModel.SelectedQuestion.Answers[i];

                        var textBox = new TextBox();

                        var checkBox = new RadioButton()
                        {
                            GroupName = "RadioAnswers",
                            DataContext = answer,
                            Content = textBox,
                            IsChecked = answer.IsRight
                        };

                        var binding = new Binding("Text")
                        {
                            Source = answer
                        };
                        textBox.SetBinding(TextBox.TextProperty, binding);

                        checkBox.Checked += RadioBtn_Checked;

                        PanelAnswers.Children.Add(checkBox);
                    }
                }
            }
        }

        private void RadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            var answer = ((RadioButton)sender).DataContext as Answer;

            for (int i = 0; i < chapterViewModel.SelectedQuestion.Answers.Count; i++)
            {
                chapterViewModel.SelectedQuestion.Answers[i].IsRight = false;
            }

            answer.IsRight = true;
        }

        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            var answer = ((CheckBox)sender).DataContext as Answer;

            answer.IsRight = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var answer = ((CheckBox)sender).DataContext as Answer;

            answer.IsRight = true;
        }
    }
}