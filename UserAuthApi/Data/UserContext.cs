/* code that was here alrady */
// using Microsoft.EntityFrameworkCore;
// using UserAuthApi.Models;

// namespace UserAuthApi.Data
// {
//     public class UserContext : DbContext
//     {
//         public UserContext(DbContextOptions<UserContext> options) : base(options) { }

//         public DbSet<User> Users { get; set; }
//     }
// }

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

        // This will act as your Users table
        public DbSet<User> Users { get; set; } //Users is the name of the table
    }
}
