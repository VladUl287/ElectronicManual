using Microsoft.EntityFrameworkCore;
using RestApiDoc.Models;

namespace RestApiDoc.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOpt) : base(dbContextOpt)
        {
            Database.EnsureCreated();
        }

        public DbSet<Chapter> Chapters { get; init; }
        public DbSet<Partition> Partitions { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasData(new Chapter[]
                {
                    new Chapter { Id = 1, Name = "Chapter 1" },
                    new Chapter { Id = 2, Name = "Chapter 2" },
                    new Chapter { Id = 3, Name = "Chapter 3" }
                });
            });

            modelBuilder.Entity<Partition>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasData(new Partition[]
                {
                    new Partition { Id = 1, Name = "Partition 1", Text = "ergregre", ChapterId = 1 },
                    new Partition { Id = 2, Name = "Partition 2", Text = "wefwefwe", ChapterId = 1 },
                    new Partition { Id = 3, Name = "Partition 3", Text = "24324235", ChapterId = 2 },
                    new Partition { Id = 4, Name = "Partition 4", Text = "erfeger", ChapterId = 2 },
                    new Partition { Id = 5, Name = "Partition 5", Text = "ergregger", ChapterId = 2 },
                });
            });
        }
    }
}
