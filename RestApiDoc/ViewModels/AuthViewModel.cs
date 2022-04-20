using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;

namespace RestApiDoc.ViewModels
{
    public class AuthViewModel
    {
        private readonly DatabaseContext dbContext;

        public AuthViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public static bool IsAuth { get; private set; }
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
                        IsAuth = await dbContext.Users.AnyAsync(e => e.Email == Email && e.Password == Password);
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