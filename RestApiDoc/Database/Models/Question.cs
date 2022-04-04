using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiDoc.Database.Models
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsUserAnswer { get; set; }
        public List<Answer> Answers { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        [NotMapped]
        public bool IsRight { get; set; } = false;
        [NotMapped]
        public List<Answer> SelectAnswers { get; set; } = new();
    }
}
