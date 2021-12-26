using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=prisma;Username=johndoe;Password=randompassword");
        }
    }
}
