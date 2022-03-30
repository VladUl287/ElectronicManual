using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using System.Collections.ObjectModel;

namespace RestApiDoc.ViewModels
{
    public class LoginViewModel
    {
        private readonly DatabaseContext dbContext;

        public LoginViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ObservableCollection<string> LoginErrors { get; set; } = new();


        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??= new RelayCommand(async (_) =>
                {
                    if (IsValid)
                    {
                        LoginErrors.Clear();
                        var user = await dbContext.Users.FirstOrDefaultAsync(e => e.Email == Email && e.Password == Password);
                        if (user is null)
                        {
                            LoginErrors.Add("faild");
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
                LoginErrors.Clear();
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
                        LoginErrors.Add("Некорректный Email.");
                    }
                    break;
                case "Password":
                    if (string.IsNullOrEmpty(Password))
                    {
                        result = false;
                        LoginErrors.Add("Некорректный пароль.");
                    }
                    break;
            }

            return result;

        }
    }
}
