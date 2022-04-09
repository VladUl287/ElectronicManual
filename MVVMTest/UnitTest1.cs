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
        public UnitTest1()
        {
            dbContext = new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Test").Options);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var sViewModel = new MainViewModel(dbContext);
            var chapters = sViewModel.Chapters.ToArray();
            CollectionAssert.AreEqual(chapters, new Chapter[]
                {
                    new Chapter { Id = 1, Name = "Chapter 1" },
                    new Chapter { Id = 2, Name = "Chapter 2" },
                    new Chapter { Id = 3, Name = "Chapter 3" }
                });
        }
    }
}