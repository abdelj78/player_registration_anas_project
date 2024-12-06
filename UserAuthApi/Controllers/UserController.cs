// This is the controller for handling user registration and login and fetching all users from the database.
// Abdeljabbar Rebani 12/2024
using Microsoft.AspNetCore.Mvc;
using UserAuthApi.Models;  // Assuming you have a User model
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserAuthApi.Data;
using System.Threading.Tasks;

namespace UserAuthApi.Controllers
{
    [Route("api/[controller]")]  // This defines the route to access this controller
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;

        public UserController(AppDbContext context) // Injecting the database context
        {
            _context = context;
        }

        // POST api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) 
            || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid data.");
            }

            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return Conflict(new { message = "Email already in use." });
            }

            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                return Conflict(new { message = "Username already in use." });
            }

            // Save the user data to a database (using your DbContext).
            // doing it asynchronously to avoid blocking the main thread
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            // For now, we'll just return a success response.
            return Ok(new { message = "User created successfully", user });
        }

        // POST api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            // following lines show previous way of querying the database without async just for reference
            // var user = _context.Users.SingleOrDefault(u => u.Email == loginDto.Email &&
            //  u.Password == loginDto.Password);

            // Check if the user exists in the database
            var user = await _context.Users.FirstOrDefaultAsync(u =>
            (u.Email == loginDto.EmailOrUsername || u.Username == loginDto.EmailOrUsername) &&
            u.Password == loginDto.Password);
            
            // If user not found, user will be null
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" }); // HTTP 401 Unauthorized
            }

            return Ok(new { message = "Login successful", userId = user.Id, username = user.Username}); // HTTP 200 OK
        }


        // GET api/user
        // This method will return all users in the database
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync(); // Fetch all users from database
            return Ok(users);
        }
    }
}

