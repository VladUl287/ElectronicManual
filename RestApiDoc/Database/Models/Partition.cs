namespace RestApiDoc.Database.Models
{
    public class Partition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
    }
}