using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=database;Username=johndoe;Password=randompassword;Port=5432");
        }
    }
}
