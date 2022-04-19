using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database.Models;

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
        public DbSet<User> Users { get; init; }
        public DbSet<Test> Tests { get; init; }
        public DbSet<Question> Questions { get; init; }
        public DbSet<Answer> Answers { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Partition>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasDefaultValue(false);

                entity.HasData(new User[]
                {
                    new User
                    {
                        Id = 1,
                        Email = "email@mail.ru",
                        Password = "password",
                        IsAdmin = true,
                    },
                });
            });
        }
    }
}
