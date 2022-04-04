namespace RestApiDoc.Database.Models
{
    public class Partition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
    }
}