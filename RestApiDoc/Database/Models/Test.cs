using System.Collections.Generic;

namespace RestApiDoc.Database.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}