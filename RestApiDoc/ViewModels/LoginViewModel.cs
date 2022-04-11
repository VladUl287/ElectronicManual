using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;

namespace RestApiDoc.ViewModels
{
    public class LoginViewModel
    {
        private readonly DatabaseContext dbContext;

        public LoginViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public static User? AuthUser { get; private set; }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??= new RelayCommand(async (_) =>
                {
                    if (IsValid)
                    {
                        var user = await dbContext.Users.FirstOrDefaultAsync(e => e.Email == Email && e.Password == Password);
                        if (user is not null)
                        {
                            AuthUser = user;
                        }
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
                    if (!GetValidationError(ValidatedProperties[i]))
                    {
                        result = false;
                    }
                }
                return result;
            }
        }

        private bool GetValidationError(string name)
        {
            bool result = true;

            switch (name)
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

            return result;

        }
    }
}
