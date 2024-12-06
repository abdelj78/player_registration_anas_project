//this is simply to store the email and password of the user to send to the API
//this is a simple class with two string fields
//needed because it seems like:
//JsonUtility Limitation: Unity's JsonUtility.ToJson doesn't work well with anonymous types (like var user = new { ... }) 
//or with objects that are not classes explicitly defined with public fields.
//why done in a separate file?
//Why Separate? Keeping this class in its own script makes your project more modular and easier to maintain.

[System.Serializable]
public class User
{
    //works fine without capitalizing the fields but should match the API's fields in 
    //the userContext but weirdly it works without matching the fields
    public string Username;  // The username field to send to the API
    public string Email;  // The email field to send to the API
    public string Password;  // The password field to send to the API
}


public class UserLog
{
    //works fine without capitalizing the fields but should match the API's fields in 
    //the userContext but weirdly it works without matching the fields
    public string EmailOrUsername;  // The email field to send to the API
    public string Password;  // The password field to send to the API
}
