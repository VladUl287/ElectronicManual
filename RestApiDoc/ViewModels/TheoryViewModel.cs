using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using System.Windows;

namespace RestApiDoc.ViewModels
{
    public class TheoryViewModel : MainViewModel
    {
        private Question selectedQuestion;
        private Question newQuestion = new();

        public TheoryViewModel(DatabaseContext dbContext) : base(dbContext)
        {
            Initilize();
        }

        public Question NewQuestion
        {
            get { return newQuestion; }
            set
            {
                newQuestion = value;
                OnPropertyChanged("NewQuestion");
            }
        }

        public Question SelectedQuestion
        {
            get { return selectedQuestion; }
            set
            {
                selectedQuestion = value;
                OnPropertyChanged("SelectedQuestion");
            }
        }

        //Chapters commands
        private RelayCommand addChapterCommand;

        public RelayCommand AddChapterCommand
        {
            get
            {
                return addChapterCommand ??= new RelayCommand(async (name) =>
                {
                    var chapterName = name.ToString();
                    if (string.IsNullOrEmpty(chapterName))
                    {
                        return;
                    }

                    try
                    {
                        var chapter = new Chapter
                        {
                            Name = chapterName
                        };

                        await dbContext.Chapters.AddAsync(chapter);
                        await dbContext.SaveChangesAsync();
                        Chapters.Remove(chapter);
                        Chapters.Add(chapter);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка добавления главы.");
                    }
                });
            }
        }

        private RelayCommand editChapterCommand;

        public RelayCommand EditChapterCommand
        {
            get
            {
                return editChapterCommand ??= new RelayCommand(async (_) =>
                {
                    if (SelectedChapter is null)
                    {
                        return;
                    }

                    try
                    {
                        dbContext.Chapters.Update(SelectedChapter);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка обновления главы.");
                    }
                });
            }
        }

        private RelayCommand deleteChapterCommand;

        public RelayCommand DeleteChapterCommand
        {
            get
            {
                return deleteChapterCommand ??= new RelayCommand(async (_) =>
                {
                    if (SelectedChapter is null)
                    {
                        return;
                    }

                    var result = MessageBox.Show("Вы уверены что хотите удалить главу?", "Оповещение", MessageBoxButton.YesNo,
                        MessageBoxImage.Question, MessageBoxResult.No);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }

                    try
                    {
                        dbContext.Chapters.Remove(SelectedChapter);
                        await dbContext.SaveChangesAsync();
                        Chapters.Remove(SelectedChapter);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка удаления главы.");
                    }
                });
            }
        }

        //Partitions commands
        private RelayCommand addPartitionCommand;

        public RelayCommand AddPartitionCommand
        {
            get
            {
                return addPartitionCommand ??= new RelayCommand(async (_) =>
                {
                    try
                    {
                        if (SelectedChapter is null)
                        {
                            return;
                        }

                        var partition = new Partition
                        {
                            Name = "Название",
                            ChapterId = SelectedChapter.Id,
                        };

                        await dbContext.Partitions.AddAsync(partition);
                        await dbContext.SaveChangesAsync();
                        SelectedChapter.Partitions.Remove(partition);
                        SelectedChapter.Partitions.Add(partition);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка создания раздела.");
                    }
                });
            }
        }

        private RelayCommand editPartitionCommand;

        public RelayCommand EditPartitionCommand
        {
            get
            {
                return editPartitionCommand ??= new RelayCommand(async (_) =>
                {
                    try
                    {
                        if (SelectedChapter is null || SelectedPartition is null)
                        {
                            return;
                        }

                        dbContext.Partitions.Update(SelectedPartition);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка обновления раздела.");
                    }
                });
            }
        }

        private RelayCommand deletePartitionCommand;

        public RelayCommand DeletePartitionCommand
        {
            get
            {
                return deletePartitionCommand ??= new RelayCommand(async (partition) =>
                {
                    if (SelectedChapter is null || SelectedPartition is null)
                    {
                        return;
                    }

                    var result = MessageBox.Show("Вы уверены что хотите удалить главу?", "Оповещение", MessageBoxButton.YesNo,
                        MessageBoxImage.Question, MessageBoxResult.No);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }

                    try
                    {
                        dbContext.Partitions.Remove(SelectedPartition);
                        await dbContext.SaveChangesAsync();
                        SelectedChapter.Partitions.Remove(SelectedPartition);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка удаления раздела.");
                    }
                });
            }
        }

        //Tests commands
        private RelayCommand addTestCommand;

        public RelayCommand AddTestCommand
        {
            get
            {
                return addTestCommand ??= new RelayCommand(async (pt) =>
                {
                    try
                    {
                        if (SelectedChapter is null)
                        {
                            return;
                        }

                        var testName = pt.ToString();
                        if (!string.IsNullOrEmpty(testName))
                        {
                            var test = new Test
                            {
                                Name = testName,
                                ChapterId = SelectedChapter.Id
                            };

                            await dbContext.Tests.AddAsync(test);
                            await dbContext.SaveChangesAsync();
                            SelectedChapter.Tests.Remove(test);
                            SelectedChapter.Tests.Add(test);
                        }
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка создания теста.");
                    }
                });
            }
        }

        private RelayCommand editTestCommand;

        public RelayCommand EditTestCommand
        {
            get
            {
                return editTestCommand ??= new RelayCommand(async (pt) =>
                {
                    try
                    {
                        if (SelectedTest is null || SelectedChapter is null)
                        {
                            return;
                        }

                        dbContext.Tests.Update(SelectedTest);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка обновления теста.");
                    }
                });
            }
        }

        private RelayCommand deleteTestCommand;

        public RelayCommand DeleteTestCommand
        {
            get
            {
                return deleteTestCommand ??= new RelayCommand(async (pt) =>
                {
                    try
                    {
                        if (SelectedTest is null || SelectedChapter is null)
                        {
                            return;
                        }

                        dbContext.Tests.Remove(SelectedTest);
                        await dbContext.SaveChangesAsync();
                        SelectedChapter.Tests.Remove(SelectedTest);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка удаления теста.");
                    }
                });
            }
        }

        //Questions commands
        private RelayCommand addQuestionCommand;

        public RelayCommand AddQuestionCommand
        {
            get
            {
                return addQuestionCommand ??= new RelayCommand((pt) =>
                {
                    try
                    {
                        if (SelectedTest is null)
                        {
                            return;
                        }

                        if (NewQuestion.Answers.Count == 0 || string.IsNullOrEmpty(NewQuestion.Text))
                        {
                            return;
                        }

                        NewQuestion.TestId = SelectedTest.Id;

                        var question = NewQuestion;
                        dbContext.Questions.Add(question);
                        dbContext.SaveChanges();
                        SelectedTest.Questions.Remove(question);
                        SelectedTest.Questions.Add(question);
                        NewQuestion = new();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка создания вопроса.");
                    }
                });
            }
        }

        private RelayCommand deleteQuestionCommand;

        public RelayCommand DeleteQuestionCommand
        {
            get
            {
                return deleteQuestionCommand ??= new RelayCommand(async (pt) =>
                {
                    try
                    {
                        if (SelectedQuestion is null || SelectedTest is null)
                        {
                            return;
                        }

                        dbContext.Questions.Remove(SelectedQuestion);
                        await dbContext.SaveChangesAsync();
                        SelectedTest.Questions.Remove(SelectedQuestion);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка удаления вопроса.");
                    }
                });
            }
        }
    }
}