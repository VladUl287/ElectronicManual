using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace RestApiDoc.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly DatabaseContext dbContext;
        public ObservableCollection<User> users { get; private set; }

        public UserViewModel(DatabaseContext databaseContext)
        {
            dbContext = databaseContext;

            Initilize();
        }

        public async Task Initilize()
        {
            Users = new(await dbContext.Users.ToListAsync());
        }

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }

        private RelayCommand? createUserCommand;
        private RelayCommand? deleteUserCommand;
        private RelayCommand? updateUserCommand;

        public RelayCommand CreateUserCommand
        {
            get
            {
                return createUserCommand ??= new RelayCommand(async (_) =>
                {
                    try
                    {
                        var user = new User();
                        await dbContext.Users.AddAsync(user);
                        await dbContext.SaveChangesAsync();
                        Users.Add(user);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка создания пользователя.");
                    }
                });
            }
        }

        public RelayCommand UpdateUserCommand
        {
            get
            {
                return updateUserCommand ??= new RelayCommand(async (us) =>
                {
                    try
                    {
                        if (us is not User user)
                        {
                            return;
                        }

                        dbContext.Users.Update(user);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка обновления пользователя.");
                    }
                });
            }
        }

        public RelayCommand DeleteUserCommand
        {
            get
            {
                return deleteUserCommand ??= new RelayCommand(async (us) =>
                {
                    try
                    {
                        if (us is not User user)
                        {
                            return;
                        }

                        dbContext.Users.Remove(user);
                        await dbContext.SaveChangesAsync();
                        Users.Remove(user);
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Ошибка удаления пользователя.");
                    }
                });
            }
        }
    }
}
