using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RestApiDoc.ViewModels
{
    public class ChapterViewModel : INotifyPropertyChanged
    {
        private Chapter selectedChapter;
        private Partition selectedPartition;
        private readonly DatabaseContext dbContext;
        public ObservableCollection<Chapter> Chapters { get; private set; }

        public ChapterViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;

            var chapters = this.dbContext.Chapters
                .Include(e => e.Partitions)
                .AsNoTracking();
            Chapters = new ObservableCollection<Chapter>(chapters);
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

        public Chapter SelectedChapter
        {
            get { return selectedChapter; }
            set
            {
                selectedChapter = value;
                OnPropertyChanged("SelectedChapter");
            }
        }

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
                        var chapter = new Chapter { Name = chapterName };
                        await dbContext.Chapters.AddAsync(chapter);
                        await dbContext.SaveChangesAsync();
                        Chapters.Add(chapter);
                    }
                    catch (DbUpdateException)
                    {
                        
                    }
                });
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??= new RelayCommand(async (chapter) =>
                {
                    try
                    {
                        dbContext.Chapters.Update(SelectedChapter);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {

                    }
                });
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??= new RelayCommand(async (name) =>
                {
                    if (SelectedChapter is null)
                    {
                        return;
                    }

                    var result = MessageBox.Show("Вы уверены что хотите удалить главу?", "Notif", MessageBoxButton.YesNo);
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
                return editPartitionCommand ??= new RelayCommand(async (partition) =>
                {
                    try
                    {
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


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
