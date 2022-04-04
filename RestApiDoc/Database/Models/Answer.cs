namespace RestApiDoc.Database.Models
{
    public class Answer : BaseEntity
    {
        public string Text { get; set; }
        public bool IsRight { get; set; }
        public int? QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
