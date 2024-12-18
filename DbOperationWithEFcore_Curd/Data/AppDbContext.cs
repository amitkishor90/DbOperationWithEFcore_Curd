using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DbOperationWithEFcore_Curd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding data for CurrencyType
            modelBuilder.Entity<CurrencyType>().HasData(
                new CurrencyType { CurrencyId = 1, Title = "INR", Descripation = "INDIA INR" },
                new CurrencyType { CurrencyId = 2, Title = "Dollar", Descripation = "Dollar" },
                new CurrencyType { CurrencyId = 3, Title = "Euro", Descripation = "Euro" }
            );

            // Seeding data for Language
            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Title = "Hindi", Description = "Hindi INDIA" },
                new Language { Id = 2, Title = "English", Description = "English" },
                new Language { Id = 3, Title = "khota", Description = "khota" }
            );

            // Specify table names if needed
            modelBuilder.Entity<Language>().ToTable("Languages");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        // DbSets for entities
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }
    }
}
