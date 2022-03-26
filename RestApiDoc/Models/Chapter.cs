using System.Collections.ObjectModel;

namespace RestApiDoc.Models
{
    public class Chapter : BaseEntity
    {
        public string Name { get; set; }
        public ObservableCollection<Partition> Partitions { get; set; }
    }
}