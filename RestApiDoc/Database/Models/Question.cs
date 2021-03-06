using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiDoc.Database.Models
{
    public class Question
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Text { get; set; } = string.Empty;
        public int TestId { get; set; }
        public Test Test { get; set; }
        public List<Answer> Answers { get; set; } = new();

        [NotMapped]
        public bool IsRight { get; set; } = false;

        [NotMapped]
        public List<Answer> SelectAnswers { get; set; } = new();
    }

    public enum QuestionType
    {
        Single,
        Multiple,
        UserAnswer
    }
}