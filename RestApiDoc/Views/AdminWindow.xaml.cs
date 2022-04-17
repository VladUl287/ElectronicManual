using RestApiDoc.Database.Models;
using RestApiDoc.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace RestApiDoc.Views
{
    public partial class AdminWindow : Window
    {
        private readonly AdminViewModel adminViewModel;

        public AdminWindow(AdminViewModel adminViewModel)
        {
            InitializeComponent();
            DataContext = adminViewModel;
            this.adminViewModel = adminViewModel;
            adminViewModel.PropertyChanged += AdminViewModel_PropertyChanged;

            SingeRadioBtn.IsChecked = true;
        }

        private void AdminViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPartition" && adminViewModel.SelectedPartition is not null)
            {
                partitionText.SetRtf(adminViewModel.SelectedPartition.Text);
            }
        }

        private void BtnPartitionUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (adminViewModel.SelectedPartition is not null)
            {
                adminViewModel.SelectedPartition.Text = partitionText.Rtf;

                adminViewModel.EditPartitionCommand.Execute(null);
            }
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
                var textBox = new TextBox
                {
                    MinWidth = 150
                };

                var bindingText = new Binding("Text")
                {
                    Source = answer
                };
                textBox.SetBinding(TextBox.TextProperty, bindingText);

                PanelAnswers.Children.Add(textBox);

                adminViewModel.NewQuestion.QuestionType = QuestionType.UserAnswer;
                adminViewModel.NewQuestion.Answers.Clear();
                adminViewModel.NewQuestion.Answers.Add(answer);
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
                    checkBox.SetBinding(ToggleButton.IsCheckedProperty, bindingRight);

                    PanelAnswers.Children.Add(checkBox);

                    listAnswers.Add(answer);
                }

                adminViewModel.NewQuestion.QuestionType = QuestionType.Multiple;
                adminViewModel.NewQuestion.Answers.Clear();
                adminViewModel.NewQuestion.Answers.AddRange(listAnswers);
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

                adminViewModel.NewQuestion.QuestionType = QuestionType.Single;
                adminViewModel.NewQuestion.Answers.Clear();
                adminViewModel.NewQuestion.Answers.AddRange(listAnswers);
            }
        }
    }
}