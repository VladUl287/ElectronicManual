using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestApiDoc.Database.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Partition> Partitions { get; set; }
        public ObservableCollection<Test> Tests { get; set; }
    }
}