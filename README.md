# player_registration_anas_project
Project aim: To develop a simple UI on Unity to register and login user credentials.

## Directories
- /UserAuthApi: API project done with .NET
- /UI registration 2D: Unity project


To start the API:
- Be in the folder of the project in the terminal
- dotnet restrore //optional but to update everyting if packages versions changed etc 
- dotnet build //to build the project 
- dotnet run // to run the project


To test api:
- Can directly POST using unity UI 
- Can visualise database (.db) file directly
- Use postman and post at corresponding http link a json such as: 

http://localhost:5000/api/user/register
JSON to register example:
{
  "username": "abdelj78",
  "email": "abdel.rebani@gmail.com",
  "password": "testpass"
}

http://localhost:5000/api/user/login
JSON to login
{
  "email": "abdel.rebani@gmail.com",
  "password": "testpass"
}

To see whole database:
GET at http://localhost:5000/api/user

To reset the database, simply delete (rm) the .db file and a new one will be created when re-runing the .net project



## The methods to POST and GET from the database are asynchronous, benefits of These Changes:
- Non-blocking database operations: All database calls (AnyAsync, AddAsync, SaveChangesAsync ToListAsync) are now non-blocking.
- Improved user experience: The server can handle multiple requests more efficiently, ensuring that one slow query doesn’t block others.
 

Current limitations and ideas for futur work:
- currentyl using SQLite with local database so not accessible through network
- low security as not using tokens for response for user after login


## .NET
- Using Microsoft .NET SDK 8.0.404 (Software Development Kit) 
- .NET Runtime 8.0.11
- ASP.NET Core Runtime 8.0.11
- .NET Windows Desktop Runtime 8.0.11

<img src="/media/1_UI.png" alt="real plant sound" width="750"/>