// Purpose: Contains the AppDbContext class which is used to interact with the database.
using Microsoft.EntityFrameworkCore;
using UserAuthApi.Models;

namespace UserAuthApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        // This will act as the Users table
        public DbSet<User> Users { get; set; } //Users is the name of the table
    }
}
