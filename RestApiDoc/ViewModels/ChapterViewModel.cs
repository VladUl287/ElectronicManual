using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace RestApiDoc.ViewModels
{
    public class ChapterViewModel : INotifyPropertyChanged
    {
        private Test selectedTest;
        private Test selectedTestAdmin;
        private Chapter selectedChapter;
        private Question selectedQuestion;
        private Partition selectedPartition;
        private Question newQuestion = new();
        public ObservableCollection<Chapter> Chapters { get; private set; }

        private readonly DatabaseContext dbContext;

        public ChapterViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;

            var chapters = dbContext.Chapters
               .Include(e => e.Partitions)
               .Include(e => e.Tests)
               .ThenInclude(e => e.Questions)
               .ThenInclude(e => e.Answers)
               .AsNoTracking();

            Chapters = new ObservableCollection<Chapter>(chapters);
        }

        public Chapter SelectedChapter
        {
            get { return selectedChapter; }
            set
            {
                selectedChapter = value;
                OnPropertyChanged("SelectedChapter");
            }
        }

        public Test SelectedTestAdmin
        {
            get { return selectedTestAdmin; }
            set
            {
                selectedTestAdmin = value;
                OnPropertyChanged("SelectedTestAdmin");
            }
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

        public Test SelectedTest
        {
            get { return selectedTest; }
            set
            {
                selectedTest = value;
                OnPropertyChanged("SelectedTest");
            }
        }

        public Partition SelectedPartition
        {
            get { return selectedPartition; }
            set
            {
                selectedPartition = value;
                OnPropertyChanged("SelectedPartition");
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

        //Chapters
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand(async (name) =>
                {
                    var chapterName = (string)name;
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
                        Chapters.Add(chapter);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка добавления главы.");
                    }
                });
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??= new RelayCommand(async (_) =>
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

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??= new RelayCommand(async (_) =>
                {
                    if (SelectedChapter is null)
                    {
                        return;
                    }

                    var result = MessageBox.Show("Вы уверены что хотите удалить главу?", "Оповещение", MessageBoxButton.YesNo);
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

        //Partitions
        private RelayCommand addPartitionCommand;
        public RelayCommand AddPartitionCommand
        {
            get
            {
                return addPartitionCommand ??= new RelayCommand(async (pt) =>
                {
                    try
                    {
                        if (SelectedChapter is null)
                        {
                            return;
                        }

                        if (pt is Partition partition)
                        {
                            await dbContext.Partitions.AddAsync(partition);
                            await dbContext.SaveChangesAsync();
                            SelectedChapter.Partitions.Add(partition);
                        }
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка создания.");
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
                        if (SelectedPartition is null)
                        {
                            return;
                        }

                        dbContext.Partitions.Update(SelectedPartition);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
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
                    var result = MessageBox.Show("Вы уверены что хотите удалить главу?", "Notif", MessageBoxButton.YesNo);
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
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }

        //Tests
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
                            SelectedChapter.Tests.Add(test);
                        }
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка создания.");
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
                        if (SelectedTestAdmin is null)
                        {
                            return;
                        }

                        dbContext.Tests.Update(SelectedTestAdmin);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка обновления.");
                    }
                });
            }
        }

        private RelayCommand deleteAnswersCommand;
        public RelayCommand DeleteAnswersCommand
        {
            get
            {
                return deleteAnswersCommand ??= new RelayCommand(async (answers) =>
                {
                    try
                    {
                        if (answers is null)
                        {
                            return;
                        }

                        var ans = (IEnumerable<Answer>)answers;
                        dbContext.Answers.RemoveRange(ans);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка удаления.");
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
                        if (SelectedTestAdmin is null)
                        {
                            return;
                        }

                        dbContext.Tests.Remove(SelectedTestAdmin);
                        await dbContext.SaveChangesAsync();
                        SelectedChapter.Tests.Remove(SelectedTestAdmin);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка удаления.");
                    }
                });
            }
        }


        //Questions
        private RelayCommand addQuestionCommand;
        public RelayCommand AddQuestionCommand
        {
            get
            {
                return addQuestionCommand ??= new RelayCommand(async (pt) =>
                {
                    try
                    {
                        if (SelectedTestAdmin is null)
                        {
                            return;
                        }

                        if (NewQuestion.Answers.Count == 0 || string.IsNullOrEmpty(NewQuestion.Text))
                        {
                            return;
                        }

                        NewQuestion.TestId = SelectedTestAdmin.Id;

                        var question = NewQuestion;
                        await dbContext.Questions.AddAsync(question);
                        await dbContext.SaveChangesAsync();
                        SelectedTestAdmin.Questions.Add(question);
                        NewQuestion = new();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка создания.");
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
                        if (SelectedQuestion is null)
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


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
