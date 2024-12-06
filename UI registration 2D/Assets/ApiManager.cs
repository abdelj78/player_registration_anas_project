// Code for API manager to handle the registration and login requests
// Abdeljabbar Rebani 12/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : MonoBehaviour
{
    private const string ApiBaseUrl = "http://localhost:5000/api/user"; // Your API's base URL
                                        
    // REGISTRATION METHODS
    public void RegisterUser(string username, string email, string password, System.Action<string> callback)
    {
        StartCoroutine(SendRegisterRequest(username, email, password, callback));
    }

    private IEnumerator SendRegisterRequest(string username, string email, string password, System.Action<string> callback)
    {
        // Create a JSON payload
        // var allows you to create an object without defining a class
        //new {} creates an anonymous type where you can define properties
        //so here, we're creating an object with email and password properties which are strings
        //this is useful for creating simple objects without defining a class such as json payloads
        var user = new User
        {
            Username = username,
            Email = email,
            Password = password
        };
        string jsonPayload = JsonUtility.ToJson(user);

        // Log the JSON payload for debugging
        Debug.Log("JSON Payload: " + jsonPayload);

        // Create the request
        UnityWebRequest request = new UnityWebRequest(ApiBaseUrl + "/register", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callback?.Invoke("Success");// send message code to the callback
            Debug.Log("API manager: User registered successfully: " + request.downloadHandler.text);
        }
        else if (request.responseCode == 409) // 409 is the HTTP status code for Conflict
        {
            var errorResponse = request.downloadHandler.text; // Get the error response body
            if (errorResponse.Contains("Email"))//check if the error response contains the word "Email"
            {
                callback?.Invoke("EmailConflict");// send message code to the callback
                Debug.LogWarning("API manager: Registration failed - Email already exists.");
            }
            else if (errorResponse.Contains("Username"))
            {
                callback?.Invoke("UsernameConflict");// send message code to the callback
                Debug.LogWarning("API manager: Registration failed - Username already exists.");
            }
            else
            {
                callback?.Invoke("Conflict");// send message code to the callback
                Debug.LogWarning("API manager: Registration failed - Conflict.");
            }
        }
        else
        {
            callback?.Invoke("Error");// send message code to the callback
            Debug.LogError("API manager: Error registering user: " + request.error);
        }
    }

    // LOGIN METHODS
    public void LoginUser(string emailOrUsername, string password, System.Action<string, int, string> callback)
    {
        StartCoroutine(SendLoginRequest(emailOrUsername, password, callback));
    }

    private IEnumerator SendLoginRequest(string emailOrUsername, string password, System.Action<string, int, string> callback)
    {
        var userLogin = new UserLog
        {
            EmailOrUsername = emailOrUsername,
            Password = password
        };
        string jsonPayload = JsonUtility.ToJson(userLogin);

        UnityWebRequest request = new UnityWebRequest(ApiBaseUrl + "/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("API manager: User logged in successfully: " + request.downloadHandler.text);
            
            // Deserialize the response
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
            if (response != null)
            {
                callback?.Invoke("Success", response.userId, response.username);// send message code to the callback
            }
            else
            {
                Debug.LogWarning("API manager: Login failed - Invalid response.");
                callback?.Invoke("Error", 0, null);// send message code to the callback
            }

        }
        else
        {
            // Check the HTTP response code
            if (request.responseCode == 401)// 401 is the HTTP status code for Unauthorized
            {
                Debug.LogWarning("API manager: Login failed - Invalid email or password.");
                // Handle invalid credentials
                callback?.Invoke("Invalid", 0,null);// send message code to the callback
            }
            else
            {
                Debug.LogError($"API manager: Login failed - {request.responseCode}: {request.error}");
                // Handle other errors (e.g., server issues, network errors)
                callback?.Invoke("Error", 0,null);// send message code to the callback
            }
        }

    }

}

[System.Serializable]
public class LoginResponse
{
    public string message;  // "Login successful"
    public int userId;      // User's unique ID
    public string username; // User's username
}


