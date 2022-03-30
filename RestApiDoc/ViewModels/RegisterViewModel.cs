using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using System.Collections.ObjectModel;

namespace RestApiDoc.ViewModels
{
    public class RegisterViewModel
    {
        private readonly DatabaseContext dbContext;

        public RegisterViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public ObservableCollection<string> RegErrors { get; set; } = new();


        private RelayCommand regCommand;
        public RelayCommand RegCommand
        {
            get
            {
                return regCommand ??= new RelayCommand(async (_) =>
                {
                    if (IsValid)
                    {
                        RegErrors.Clear();
                        await dbContext.Users.AddAsync(new User { Email = Email, Password = Password });
                        await dbContext.SaveChangesAsync();
                    }
                });
            }
        }

        private static readonly string[] ValidatedProperties = { "Email", "Password", "ConfirmPassword" };

        public bool IsValid
        {
            get
            {
                RegErrors.Clear();
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
                        RegErrors.Add("Некорректный Email.");
                    }
                    break;
                case "Password":
                    if (string.IsNullOrEmpty(Password))
                    {
                        result = false;
                        RegErrors.Add("Некорректный пароль.");
                    }
                    break;
                case "ConfirmPassword":
                    if (ConfirmPassword != Password)
                    {
                        result = false;
                        RegErrors.Add("Некорректный пароль.");
                    }
                    break;
            }

            return result;

        }
    }
}
