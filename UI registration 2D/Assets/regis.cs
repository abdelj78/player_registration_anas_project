// Code for the registration part of the UI 
// Abdeljabbar Rebani 12/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class regis : MonoBehaviour
{
    public TMPro.TMP_InputField username;
    public TMPro.TMP_InputField email;
    public TMPro.TMP_InputField password;
    public TMPro.TMP_InputField passwordConfirm;
    public TMP_Text regFeedbackText; //for general feedback after registration attempt
    public TMP_Text regUserFeedback; //for username feedback
    public TMP_Text regEmailFeedback; //for email feedback
    public TMP_Text regPassFeedback; //for password feedback
    public TMP_Text regPassConfFeedback; //for password confirmation feedback

    //having a reference to the ApiManager script allows to manually attach the api manager
    //script to the game object of this script on unity editor. Good practice.
    public ApiManager ApiManager;

    public void OnSubmit()
    {
        //reset feedback text
        regUserFeedback.text = "";
        regEmailFeedback.text = "";
        regPassFeedback.text = "";
        regPassConfFeedback.text = "";
        regFeedbackText.text = "";

        string usernameText = username.text;
        string emailText = email.text;
        string passText = password.text;
        string passConfText = passwordConfirm.text;

        bool filled = true;

        //////// Check if the input fields are empty
        if (string.IsNullOrEmpty(usernameText))
        {
            regUserFeedback.text = "Username is required!";
            filled = false;
        }
        if (string.IsNullOrEmpty(emailText))
        {
            regEmailFeedback.text = "Email is required!";
            filled = false;
        }
        if (string.IsNullOrEmpty(passText))
        {
            regPassFeedback.text = "Password is required!";
            filled = false;
        }
        if (string.IsNullOrEmpty(passConfText))
        {
            regPassConfFeedback.text = "Password confirmation is required!";
            filled = false;
        }

        if (!filled) return; // If any field is empty, stop the function
        ///////////////////
        
        //////Further validation checks//////
        // Check if the email is valid (format of email)
        if (!IsValidEmail(emailText))
        {
            regEmailFeedback.text = "Invalid email address!";
            return;
        }

        // Check if the password is strong
        if (!IsPasswordStrong(passText))
        {
            regPassFeedback.text = "Password is too weak! Must be at least 8 characters long!";
            return;
        }
        
        //check if the password and password confirmation match
        if (!(passText == passConfText))
        {
            regPassConfFeedback.text = "Passwords do not match! Check your password!";
            return;
        }
        /////////////////////////////////////
        
        // If all checks passed, proceed with registration
        Debug.Log("Sending registration of user: " + usernameText + ", Email: " + emailText +
                  ", Password: " + passText + ", Password Confirm: " + passConfText); // delete this line after testing for password security

        //HandleRegisterResponse is a callback function that will be called when the registration is done
        ApiManager.RegisterUser(usernameText, emailText, passText, HandleRegisterResponse); 

    }

    void HandleRegisterResponse(string message)
    {
        switch (message)
        {
            case "Success":
                regFeedbackText.text = "Registration successful!";
                break;
            case "UsernameConflict":
                regFeedbackText.text = "Registration failed - Username already being used.";
                break;
            case "EmailConflict":
                regFeedbackText.text = "Registration failed - Email already being used.";
                break;
            case "Conflict":
                regFeedbackText.text = "Registration failed - Unknown conflict.";
                break;
            case "Error":
                regFeedbackText.text = "Error registering user. Please try again later.";
                break;
            default:
                regFeedbackText.text = "Unknown error. Please try again later.";
                break;
        }
    }
    bool IsValidEmail(string email)
    {
        // Basic email validation
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
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
        return score >= 1; // Password is strong if it meets at least 4 criteria
    }

}
