using RestApiDoc.Database.Models;
using RestApiDoc.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace RestApiDoc.Pages
{
    public partial class AdminTestsPage : Page
    {
        private readonly TheoryViewModel adminViewModel;

        public AdminTestsPage(TheoryViewModel adminViewModel)
        {
            InitializeComponent();
            DataContext = adminViewModel;
            this.adminViewModel = adminViewModel;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
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
                var listAnswers = new List<Answer>(4);

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
                var listAnswers = new List<Answer>(4);

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            adminViewModel.AddQuestionCommand.Execute(null);
            SingeRadioBtn.IsChecked = false;
            SingeRadioBtn.IsChecked = true;
        }
    }
}