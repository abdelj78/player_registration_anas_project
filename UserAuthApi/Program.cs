// Purpose: Entry point for the application. Configures the application and sets up the database.
// Abdeljabbar Rebani 12/2024
using Microsoft.EntityFrameworkCore;
using UserAuthApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// // Add the in-memory database service
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseInMemoryDatabase("UserDb")); // Name your database "UserDb"

// SQLite database
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlite("Data Source=UserDb.db"));

// Add the SQLite database service
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Creates the database if it does not exist
    // dbContext.Database.Migrate(); // Use this if you're applying migrations
}


app.UseAuthorization();
app.MapControllers();
app.Run();