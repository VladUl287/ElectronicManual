using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;

namespace RestApiDoc.ViewModels
{
    public class AuthViewModel
    {
        private readonly DatabaseContext dbContext;

        public AuthViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public static User? AuthUser { get; private set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        private RelayCommand? loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??= new RelayCommand(async (_) =>
                {
                    if (IsValid)
                    {
                        AuthUser = await dbContext.Users.FirstOrDefaultAsync(e => e.Email == Email && e.Password == Password);
                    }
                });
            }
        }

        private static readonly string[] ValidatedProperties = { "Email", "Password" };

        public bool IsValid
        {
            get
            {
                var result = true;
                for (int i = 0; i < ValidatedProperties.Length; i++)
                {
                    switch (ValidatedProperties[i])
                    {
                        case "Email":
                            if (string.IsNullOrEmpty(Email))
                            {
                                result = false;
                            }
                            break;

                        case "Password":
                            if (string.IsNullOrEmpty(Password))
                            {
                                result = false;
                            }
                            break;
                    }
                }
                return result;
            }
        }
    }
}