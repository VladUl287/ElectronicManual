namespace RestApiDoc.Database.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}