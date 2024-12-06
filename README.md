# player_registration_anas_project
Project aim: To develop a simple UI on Unity to register and login user credentials.

## Directories
- /UserAuthApi: API project done with .NET
- /UI registration 2D: Unity project


## To start the API:
- Be in the folder of the project in the terminal
- dotnet restrore (optional but to update everyting if packages versions changed etc) 
- dotnet build (to build the project) 
- dotnet run (to run the project)


## To test api:
- Can directly POST using unity UI 
- Can visualise database (.db) file directly
- Use postman and post at corresponding http link a json such as: 

To register:<br>
Use POST at: http://localhost:5000/api/user/register<br>
Send JSON:<br>
{<br>
  "username": "abdelj78",<br>
  "email": "abdel.rebani@gmail.com",<br>
  "password": "testpass"<br>
}<br>

To login:<br>
Use POST at: http://localhost:5000/api/user/login <br>
Send JSON:<br>
{<br>
  "email": "abdel.rebani@gmail.com",<br>
  "password": "testpass"<br>
}<br>

To see whole database:<br>
Use GET at: http://localhost:5000/api/user

To reset the database, simply delete (rm) the .db file and a new one will be created when re-runing the .net project


## The methods to POST and GET from the database are asynchronous, benefits of These Changes:
- Non-blocking database operations: All database calls (AnyAsync, AddAsync, SaveChangesAsync ToListAsync) are now non-blocking.
- Improved user experience: The server can handle multiple requests more efficiently, ensuring that one slow query doesn’t block others.
 

## Current limitations and ideas for futur work:
- Currentyl using SQLite with local database so not accessible through network.
- Low security as not using tokens or encryption in any process.
- No constraints in input field lengths, could cause issue with limits of Database used.


## .NET version
- Using Microsoft .NET SDK 8.0.404 (Software Development Kit) 
- .NET Runtime 8.0.11
- ASP.NET Core Runtime 8.0.11
- .NET Windows Desktop Runtime 8.0.11

## RESULTS
User Interface overview. Very Basic to let game designers work on it.<br>
<img src="/media/1_UI.png" alt="ui overview" width="700"/>
<br><br>

Errors dispalyed when input fields are empty and attempt to register or login.<br>
<img src="/media/2_UI_empty.png" alt="ui empty" width="700"/>
<br><br>

Error displayed for weak password. Here, only the length is assessed but some code (in regis.cs) can be uncommented to increase the strength requirements of the password.<br>
<img src="/media/3_weak_pass.png" alt="weak pass" width="350"/>
<br><br>

Error displayed for non matching password and confirmed password. <br>
<img src="/media/4_pass_not_match.png" alt="pass not matched" width="350"/>
<br><br>

Successful registration.<br>
<img src="/media/5_reg_success.png" alt="successful registration" width="350"/>
<br><br>

Error failed to login as non existing username.<br>
<img src="/media/6_log_fail.png" alt="login fail" width="350"/>
<br><br>

Successful login with username.<br>
<img src="/media/7_log_success1.png" alt="success login username" width="350"/>
<br><br>

Successful login with email.<br>
<img src="/media/8_log_success1email.png" alt="succ login with email" width="350"/>
<br><br>

Successful login with email for a 2nd user. <br>
<img src="/media/9_log_success2email.png" alt="successful login email2" width="350"/>
<br><br>

Database overview.<br>
<img src="/media/database.png" alt="database" width="750"/>

Error registering already used username.<br>
<img src="/media/10_regis_exist_username.png" alt="regis exist user" width="350"/>
<br><br>

Error registering already used email.<br>
<img src="/media/11_regis_exist_email.png" alt="regis exist email" width="350"/>
<br><br>

Recognition of wrong format for email.<br>
<img src="/media/12_invalid_email_format.png" alt="format email wrong" width="350"/>
<br><br>
