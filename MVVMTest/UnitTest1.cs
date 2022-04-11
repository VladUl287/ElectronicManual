using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApiDoc.Database;
using RestApiDoc.Database.Models;
using RestApiDoc.ViewModels;
using System.Linq;

namespace MVVMTest
{
    [TestClass]
    public class UnitTest1
    {
        public DatabaseContext dbContext { get; set; }
        public MainViewModel mainViewModel { get; set; }
        public LoginViewModel loginViewModel { get; set; }
        public AdminViewModel adminViewModel { get; set; }

        public UnitTest1()
        {
            dbContext = new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Test").Options);

            mainViewModel = new MainViewModel(dbContext);
            loginViewModel = new LoginViewModel(dbContext);
            adminViewModel = new AdminViewModel(dbContext);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var chapters = mainViewModel.Chapters.ToArray();
            CollectionAssert.AreEqual(chapters, new Chapter[]
                {
                    new Chapter { Id = 1, Name = "Введение" },
                    new Chapter { Id = 2, Name = "Основные положения" },
                    new Chapter { Id = 3, Name = "Заключение" },
                });
        }

        [TestMethod]
        public void TestMethod2()
        {
            loginViewModel.Email = "email@mail.ru";
            loginViewModel.Password = "password";
            loginViewModel.LoginCommand.Execute(null);
            Assert.AreEqual(LoginViewModel.AuthUser, new User
            {
                Id = 1,
                Email = "email@mail.ru",
                Password = "password",
                IsAdmin = true
            });
        }

        [TestMethod]
        public void TestMethod3()
        {
            var users = adminViewModel.Users;
            CollectionAssert.AreEqual(users, new User[]
                {
                    new User
                    {
                        Id = 1,
                        Email = "email@mail.ru",
                        Password = "password",
                        IsAdmin = true,
                    },
                });
        }
    }
}