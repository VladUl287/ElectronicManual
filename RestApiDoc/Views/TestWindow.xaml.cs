using RestApiDoc.Database.Models;
using RestApiDoc.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace RestApiDoc.Views
{
    public partial class TestsWindow : Window
    {
        private int seconds = 0;
        private readonly Question[] questions;
        private static readonly Random random = new();
        private readonly Timer timer = new(1000);
        private static readonly SolidColorBrush solidColorBrush = new(Color.FromRgb(255, 255, 255));

        public TestsWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;

            questions = mainViewModel.SelectedTest.Questions.ToArray();
            Shuffle(questions);
            questions = questions.Take(10).ToArray();

            for (int i = 0; i < questions.Length; i++)
            {
                CreateQuestion(questions[i], i);

                switch (questions[i].QuestionType)
                {
                    case QuestionType.Single:
                        seconds += 30;
                        break;

                    case QuestionType.Multiple:
                        seconds += 60;
                        break;

                    case QuestionType.UserAnswer:
                        seconds += 90;
                        break;
                }
            }

            SetTime();

            timer.Elapsed += Timer_Elapsed;

            var fileInfo = new FileInfo($"{Directory.GetCurrentDirectory()}\\Data\\theory\\rules.rtf");
            if (fileInfo.Exists)
            {
                var textRange = new TextRange(RulesTestRtf.Document.ContentStart, RulesTestRtf.Document.ContentEnd);

                using var fileStream = new System.IO.FileStream(fileInfo.FullName, System.IO.FileMode.OpenOrCreate);
                textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
            }
        }

        private void CreateQuestion(Question question, int index)
        {
            var stackPanel = new StackPanel
            {
                DataContext = question,
                Margin = new Thickness(0, 2, 0, 5)
            };

            var label = new Label
            {
                Content = $"{++index}) {question.Text}"
            };

            stackPanel.Children.Add(label);

            if (question.QuestionType == QuestionType.UserAnswer)
            {
                var textBox = new TextBox
                {
                    DataContext = question.Answers.FirstOrDefault()
                };
                textBox.LostFocus += TbAnswer_Checked_Validated;

                stackPanel.Children.Add(textBox);
            }
            else
            {
                var answers = question.Answers.ToArray();

                if (question.QuestionType == QuestionType.Multiple)
                {
                    var rightAnswers = question.Answers.Select(e => new { e.Text, e.IsRight }).ToArray();

                    for (int i = 0; i < answers.Length; i++)
                    {
                        var check = new CheckBox
                        {
                            DataContext = answers[i],
                            Content = new TextBlock
                            {
                                Text = answers[i].Text,
                                TextWrapping = TextWrapping.Wrap
                            }
                        };
                        check.Resources.Add("MaterialDesignCheckBoxOff", solidColorBrush);

                        check.Checked += CkAnswer_CheckedChanged;
                        check.Unchecked += CkAnswer_UnCheckedChanged;

                        stackPanel.Children.Add(check);
                    }
                }
                else
                {
                    for (int i = 0; i < answers.Length; i++)
                    {
                        var radio = new RadioButton
                        {
                            DataContext = answers[i],
                            Content = new TextBlock
                            {
                                Text = answers[i].Text,
                                TextWrapping = TextWrapping.Wrap
                            }
                        };
                        radio.Resources.Add("MaterialDesignCheckBoxOff", solidColorBrush);

                        radio.Checked += RbAnswer_CheckedChanged;

                        stackPanel.Children.Add(radio);
                    }
                }
            }

            QuestionsPanel.Children.Add(stackPanel);
        }

        private void SetTime()
        {
            var ts = TimeSpan.FromSeconds(seconds);
            Dispatcher.Invoke(() =>
            {
                lblTimeTest.Content = $"До окончания теста: {ts.Minutes}:{ts.Seconds}";
            });
        }

        private static void Shuffle<T>(T[] arr)
        {
            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                var tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            seconds--;
            SetTime();
            if (seconds <= 0)
            {
                BtnResult_Click(null, EventArgs.Empty);
            }
        }

        private void BtnResult_Click(object? sender, EventArgs e)
        {
            timer.Stop();

            byte result = 5;
            var count = questions.Count(x => x.IsRight);
            var percent = count * 100 / questions.Length;
            if (percent < 60)
            {
                result = 2;
            }
            else if (percent >= 60 && percent <= 70)
            {
                result = 3;
            }
            else if (percent >= 71 && percent <= 90)
            {
                result = 4;
            }

            string msg = result < 3 ? "непройден" : "пройден";
            MessageBox.Show($"Тест {msg}. Правильных ответов {count}. " +
                $"Ваша оценка {result}.", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }

        private void CkAnswer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var answer = (Answer)((CheckBox)sender).DataContext;
                var question = (Question)((StackPanel)((CheckBox)sender).Parent).DataContext;

                question.SelectAnswers.Add(answer);
                var count = question.Answers.Count(e => e.IsRight);
                if (question.SelectAnswers.Count == count && question.SelectAnswers.Count(e => e.IsRight) == count)
                {
                    question.IsRight = true;
                }
                else
                {
                    question.IsRight = false;
                }
            }
            catch
            {
            }
        }

        private void CkAnswer_UnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var answer = (Answer)((CheckBox)sender).DataContext;
                var question = (Question)((StackPanel)((CheckBox)sender).Parent).DataContext;

                question.SelectAnswers.Remove(answer);
                var count = question.Answers.Count(e => e.IsRight);
                if (question.SelectAnswers.Count == count && question.SelectAnswers.Count(e => e.IsRight) == count)
                {
                    question.IsRight = true;
                }
                else
                {
                    question.IsRight = false;
                }
            }
            catch
            {
            }
        }

        private void RbAnswer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var radio = (Answer)((RadioButton)sender).DataContext;
                var question = (Question)((StackPanel)((RadioButton)sender).Parent).DataContext;

                question.IsRight = radio.IsRight;
            }
            catch
            {
            }
        }

        private void TbAnswer_Checked_Validated(object sender, EventArgs e)
        {
            try
            {
                var textBox = ((TextBox)sender);
                var question = (Question)((StackPanel)((TextBox)sender).Parent).DataContext;

                question.IsRight = question.Answers.FirstOrDefault()?.Text == textBox.Text.ToLower();
            }
            catch
            {
            }
        }

        private void BtnTestStart_Click(object sender, RoutedEventArgs e)
        {
            PanelRules.Visibility = Visibility.Collapsed;
            DockPanelTest.Visibility = Visibility.Visible;

            timer.Start();
        }

        private static int ActualFontSize = 12;
        private void BtnLessFontSize_Click(object sender, RoutedEventArgs e)
        {
            if (ActualFontSize < 9)
            {
                return;
            }

            ActualFontSize--;

            FontSize = ActualFontSize;
        }

        private void BtnIncreaseFontSize_Click(object sender, RoutedEventArgs e)
        {
            if (ActualFontSize > 21)
            {
                return;
            }

            ActualFontSize++;

            FontSize = ActualFontSize;
        }
    }
}