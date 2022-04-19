using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestApiDoc.Database.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ObservableCollection<Test> Tests { get; set; } = new();
        public ObservableCollection<Partition> Partitions { get; set; } = new();
    }
}