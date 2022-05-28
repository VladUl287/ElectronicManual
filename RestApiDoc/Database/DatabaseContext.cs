using Microsoft.EntityFrameworkCore;
using RestApiDoc.Database.Models;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

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

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            var path = $@"{Directory.GetCurrentDirectory()}\Data\theory";
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                string text = File.ReadAllText($@"{path}\chapters.json");
                var items = System.Text.Json.JsonSerializer.Deserialize<Chapter[]>(text, options);
                entity.HasData(items);
            });

            modelBuilder.Entity<Partition>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Text)
                    .IsRequired();

                string text = File.ReadAllText($@"{path}\partitions.json");
                var items = System.Text.Json.JsonSerializer.Deserialize<Partition[]>(text, options);
                entity.HasData(items);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                string text = File.ReadAllText($@"{path}\tests.json");
                var items = System.Text.Json.JsonSerializer.Deserialize<Test[]>(text, options);
                entity.HasData(items);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                string text = File.ReadAllText($@"{path}\questions.json");
                var items = System.Text.Json.JsonSerializer.Deserialize<Question[]>(text, options);
                entity.HasData(items);
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                string text = File.ReadAllText($@"{path}\answers.json");
                var items = System.Text.Json.JsonSerializer.Deserialize<Answer[]>(text, options);
                entity.HasData(items);
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