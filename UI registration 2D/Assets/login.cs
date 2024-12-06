// Code for the login part of the UI 
// Abdeljabbar Rebani 12/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class login : MonoBehaviour
{
    public TMPro.TMP_InputField emailOrUsernameLog;
    public TMPro.TMP_InputField passwordLog;
    public TMP_Text logFeedbackText; //for general feedback after login attempt
    public TMP_Text logEmailFeedback; //for email feedback
    public TMP_Text logPassFeedback; //for password feedback

    //having a reference to the ApiManager script allows to manually attach the api manager
    //script to the game object of this script on unity editor. Good practice.
    public ApiManager ApiManager;
    // Start is called before the first frame update
    public void OnSubmit()
    {  
        //reset feedback text
        logEmailFeedback.text = "";
        logPassFeedback.text = "";
        logFeedbackText.text = "";

        string emailText = emailOrUsernameLog.text;
        string passwordText = passwordLog.text;
        
        bool filled = true; // Check if the input fields are empty
 
        //////// Check if the input fields are empty
        if (string.IsNullOrEmpty(emailText))
        {
            logEmailFeedback.text = "Email or username is required!";
            filled = false;
        }
        if (string.IsNullOrEmpty(passwordText))
        {
            logPassFeedback.text = "Password is required!";
            filled = false;
        }

        if (!filled) return; // If any field is empty, stop the function
        ///////////////////////

        if (!IsPasswordStrong(passwordText))
        {
            logPassFeedback.text = "Password must be at least 8 characters long!";
            return;
        }

        // sending login request to the API
        Debug.Log("Sending log in request with: Email: " + emailText +
                  ", Password: " + passwordText); // delete this line after testing for password security
        ApiManager.LoginUser(emailText, passwordText, HandleLoginResponse);

    }


    void HandleLoginResponse(string message, int userId, string username)
    {
        switch (message)
        {
            case "Success":
                logFeedbackText.text = "Log in successful! Welcome, " + username + " (user ID: " + userId+")";
                // Store session data, uncomment following line if you have a SessionManager
                // This is the next step after successful login
                // To be implemented in the next part of game development
                // The Register/Login UI + API developper stopped here
                // Good luck for the future, don't hesitate to ask for help
                //SessionManager.Instance.SetSession(userId, username);
                break;
            case "Invalid":
                logFeedbackText.text = "Invalid email or password. Please try again.";
                break;
            case "Error":
                logFeedbackText.text = "An error occurred. Please try again later.";
                break;
            default:
                logFeedbackText.text = "Unknown error. Please try again later.";
                break;
        }
    }

    bool IsPasswordStrong(string pass)
    {
        // Basic example of checking password strength
        int score = 0;

        if (pass.Length >= 8) score++;
        // if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]")) score++;
        // if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]")) score++;
        // if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]")) score++;
        // if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[\W_]")) score++;

        // Update the strength bar based on the score
        //passwordStrengthBar.value = score / 5.0f;
        return score >= 1; // Password is strong if it is at least 8 characters long
    }
}


