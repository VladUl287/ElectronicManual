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

                entity.HasData(new Chapter[]
                {
                    new Chapter { Id = 1, Name = "Введение" },
                    new Chapter { Id = 2, Name = "Основные положения" },
                    new Chapter { Id = 3, Name = "Заключение" },
                });
            });

            modelBuilder.Entity<Partition>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasData(new Partition[]
                {
                    new Partition { Id = 1, Name = "Partition 1", Text = "ergregre", ChapterId = 1 },
                });
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasData(new Test
                {
                    Id = 1,
                    Name = "Test 1",
                    ChapterId = 1
                });
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasData(new Question[]
                {
                    new Question
                    {
                        Id = 1,
                        Text = "Question 1",
                        TestId = 1
                    },
                    new Question
                    {
                        Id = 2,
                        IsMultiple = true,
                        Text = "Question 2",
                        TestId = 1
                    },
                    new Question
                    {
                        Id = 3,
                        Text = "Question 3",
                        IsUserAnswer = true,
                        TestId = 1
                    },
                    new Question
                    {
                        Id = 4,
                        Text = "Question 4",
                        TestId = 1
                    },
                    new Question
                    {
                        Id = 5,
                        Text = "Question 5",
                        TestId = 1,
                        IsMultiple = true
                    },
                });
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasData(new Answer[]
                {
                    new Answer
                    {
                        Id = 1,
                        Text = "Answer 1",
                        QuestionId = 1,
                    },
                    new Answer
                    {
                        Id = 2,
                        Text = "Answer 2",
                        IsRight = true,
                        QuestionId = 1,
                    },
                    new Answer
                    {
                        Id = 3,
                        Text = "Answer 3",
                        QuestionId = 1,
                    },
                    new Answer
                    {
                        Id = 4,
                        Text = "Answer 4",
                        QuestionId = 1,
                    },
                    //
                    new Answer
                    {
                        Id = 5,
                        Text = "Answer 1",
                        IsRight = true,
                        QuestionId = 2,
                    },
                    new Answer
                    {
                        Id = 6,
                        Text = "Answer 2",
                        QuestionId = 2,
                    },
                    new Answer
                    {
                        Id = 7,
                        Text = "Answer 3",
                        QuestionId = 2,
                    },
                    new Answer
                    {
                        Id = 8,
                        Text = "Answer 4",
                        IsRight = true,
                        QuestionId = 2,
                    },
                    //
                    new Answer
                    {
                        Id = 9,
                        Text = "Answer 1",
                        QuestionId = 3,
                        IsRight = true,
                    },
                    //
                    new Answer
                    {
                        Id = 13,
                        Text = "Answer 1",
                        QuestionId = 4,
                    },
                    new Answer
                    {
                        Id = 14,
                        Text = "Answer 2",
                        QuestionId = 4,
                    },
                    new Answer
                    {
                        Id = 15,
                        Text = "Answer 3",
                        QuestionId = 4,
                        IsRight = true
                    },
                    new Answer
                    {
                        Id = 16,
                        Text = "Answer 4",
                        QuestionId = 4,
                    },
                    //
                    new Answer
                    {
                        Id = 17,
                        Text = "Answer 1",
                        QuestionId = 5,
                    },
                    new Answer
                    {
                        Id = 18,
                        Text = "Answer 2",
                        QuestionId = 5,
                    },
                    new Answer
                    {
                        Id = 19,
                        Text = "Answer 3",
                        QuestionId = 5,
                        IsRight = true
                    },
                    new Answer
                    {
                        Id = 20,
                        Text = "Answer 4",
                        QuestionId = 5,
                        IsRight = true
                    },
                });
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
