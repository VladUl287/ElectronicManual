using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestApiDoc.Database.Models
{
    public record Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ObservableCollection<Test> Tests { get; set; }
        public ObservableCollection<Partition> Partitions { get; set; }
    }
}