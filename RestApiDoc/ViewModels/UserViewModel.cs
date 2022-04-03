using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace RestApiDoc.ViewModels
{
    public class UserViewModel
    {
        private readonly DatabaseContext dbContext;

        public ObservableCollection<User> Users { get; set; }

        public UserViewModel(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
            Users = new(dbContext.Users.ToList());
        }

        private RelayCommand createCommand;
        public RelayCommand CreateCommand
        {
            get
            {
                return createCommand ??= new RelayCommand(async (_) =>
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

                    }
                });
            }
        }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??= new RelayCommand(async (user) =>
                {
                    try
                    {
                        var us = (User)user;
                        dbContext.Users.Update(us);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {

                    }
                });
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??= new RelayCommand(async (user) =>
                {
                    try
                    {
                        var us = (User)user;
                        dbContext.Users.Remove(us);
                        await dbContext.SaveChangesAsync();
                        Users.Remove(us);
                    }
                    catch (DbUpdateException)
                    {

                    }
                });
            }
        }
    }
}
