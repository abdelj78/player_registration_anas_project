// Purpose: Contains the User class and UserLoginDto class.
// The User class is used to represent a user in the database. 
// The UserLoginDto class is used to represent the data that is sent to the server when a user logs in.
// {get; set;} accessors, allow the variable to be read and modified from outside the class. 

namespace UserAuthApi.Models
{
    public class User
    {
        public int Id { get; set; } 
        // primary key, needed to uniquely identify a user
        // This is automatically generated by the database, no need to set it manually
        // but could be set manually if needed
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }

}
