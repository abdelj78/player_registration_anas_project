using Microsoft.AspNetCore.Mvc;
using UserAuthApi.Models;  // Assuming you have a User model
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserAuthApi.Data;

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

        // POST api/user
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) 
            || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid data.");
            }

            // Here, you'd save the user data to a database (using your DbContext).
            _context.Users.Add(user);
            _context.SaveChanges();
            
            // For now, we'll just return a success response.
            return Ok(new { message = "User created successfully", user });
        }

        // GET api/user
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList(); // Fetch all users from the in-memory database
            return Ok(users);
        }



    }
}
