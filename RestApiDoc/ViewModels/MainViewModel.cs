using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RestApiDoc.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Test? selectedTest;
        private Chapter? selectedChapter;
        private Partition? selectedPartition;
        private ObservableCollection<Chapter> chapters;

        protected readonly DatabaseContext dbContext;

        public MainViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;

            Initilize();
        }

        protected virtual async Task Initilize()
        {
            var chapters = await dbContext.Chapters
               .Include(e => e.Partitions)
               .Include(e => e.Tests)
               .ThenInclude(e => e.Questions)
               .ThenInclude(e => e.Answers)
               .AsNoTracking()
               .ToListAsync();

            Chapters = new ObservableCollection<Chapter>(chapters);
        }

        public Chapter? SelectedChapter
        {
            get { return selectedChapter; }
            set
            {
                selectedChapter = value;
                OnPropertyChanged("SelectedChapter");
            }
        }

        public Partition? SelectedPartition
        {
            get { return selectedPartition; }
            set
            {
                selectedPartition = value;
                OnPropertyChanged("SelectedPartition");
            }
        }

        public Test? SelectedTest
        {
            get { return selectedTest; }
            set
            {
                selectedTest = value;
                OnPropertyChanged("SelectedTest");
            }
        }

        public ObservableCollection<Chapter> Chapters
        {
            get { return chapters; }
            set
            {
                chapters = value;
                OnPropertyChanged("Chapters");
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
