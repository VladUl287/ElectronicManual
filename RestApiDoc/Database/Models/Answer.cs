﻿namespace RestApiDoc.Database.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsRight { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
