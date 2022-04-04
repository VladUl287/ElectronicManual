using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestApiDoc.Database.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
        public ObservableCollection<Question> Questions { get; set; }
    }
}