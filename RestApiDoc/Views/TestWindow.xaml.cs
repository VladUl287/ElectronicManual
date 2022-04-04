using RestApiDoc.Database.Models;
using RestApiDoc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestApiDoc.Views
{
    public partial class TestsWindow : Window
    {
        private int seconds = 0;
        private readonly Question[] questions;
        private static readonly Random random = new();
        private readonly Timer timer = new(1000);

        public TestsWindow(ChapterViewModel chapterViewModel)
        {
            InitializeComponent();
            DataContext = chapterViewModel;

            questions = chapterViewModel.SelectedTest.Questions.ToArray();
            Shuffle(questions);
            questions = questions.Take(10).ToArray();

            for (int i = 0; i < questions.Length; i++)
            {
                CreateQuestion(questions[i]);
                if (questions[i].IsMultiple)
                {
                    seconds += 60;
                }
                else if (questions[i].IsUserAnswer)
                {
                    seconds += 90;
                }
                else
                {
                    seconds += 30;
                }
            }
            var button = new Button
            {
                Content = "Завершить"
            };
            button.Click += BtnResult_Click;

            QuestionsPanel.Children.Add(button);

            SetTime();
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void CreateQuestion(Question question)
        {
            var stackPanel = new StackPanel
            {
                DataContext = question
            };

            var label = new Label
            {
                Content = question.Text
            };

            stackPanel.Children.Add(label);

            if (question.IsUserAnswer)
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

                if (question.IsMultiple)
                {
                    var rightAnswers = question.Answers.Select(e => new { e.Text, e.IsRight }).ToArray();

                    for (int i = 0; i < answers.Length; i++)
                    {
                        var check = new CheckBox
                        {
                            DataContext = answers[i],
                            Content = answers[i].Text
                        };

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
                            Content = answers[i].Text
                        };

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

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            seconds--;
            SetTime();
            if (seconds <= 0)
            {
                BtnResult_Click(null, null);
            }
        }

        private void BtnResult_Click(object sender, EventArgs e)
        {
            timer.Stop();

            var result = 5;
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
            MessageBox.Show($"Тест {msg}. Правильных ответов {count}. Ваша оценка {result}.", "Результат", default);

            Close();
        }

        private void CkAnswer_CheckedChanged(object sender, EventArgs e)
        {
            var check = (CheckBox)sender;
            var answer = ((CheckBox)sender).DataContext as Answer;
            var question = (((CheckBox)sender).Parent as StackPanel).DataContext as Question;

            question.SelectAnswers.Add(answer);
            if (question.SelectAnswers.Count(e => e.IsRight) == question.Answers.Count(e => e.IsRight) &&
                question.SelectAnswers.Count == question.Answers.Count(e => e.IsRight))
            {
                question.IsRight = true;
            }
            else
            {
                question.IsRight = false;
            }
        }

        private void CkAnswer_UnCheckedChanged(object sender, EventArgs e)
        {
            var check = (CheckBox)sender;
            var answer = ((CheckBox)sender).DataContext as Answer;
            var question = (((CheckBox)sender).Parent as StackPanel).DataContext as Question;

            question.SelectAnswers.Remove(answer);
            if (question.SelectAnswers.Count(e => e.IsRight) == question.Answers.Count(e => e.IsRight) &&
                question.SelectAnswers.Count == question.Answers.Count(e => e.IsRight))
            {
                question.IsRight = true;
            }
            else
            {
                question.IsRight = false;
            }
        }

        private void RbAnswer_CheckedChanged(object sender, EventArgs e)
        {
            var radio = ((RadioButton)sender).DataContext as Answer;
            var question = (((RadioButton)sender).Parent as StackPanel).DataContext as Question;

            if (radio.IsRight)
            {
                question.IsRight = true;
            }
            else
            {
                question.IsRight = false;
            }
        }

        private void TbAnswer_Checked_Validated(object sender, EventArgs e)
        {
            var textBox = ((TextBox)sender);
            var question = (((TextBox)sender).Parent as StackPanel).DataContext as Question;

            if (question.Answers.FirstOrDefault().Text == textBox.Text)
            {
                question.IsRight = true;
            }
            else
            {
                question.IsRight = false;
            }
        }
    }
}
