using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using System.Collections.Generic;
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
