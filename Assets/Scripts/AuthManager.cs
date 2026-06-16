using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Firebase.Database;
public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;


    public void LogoutButton()
    {

        if (User != null)
        {
            StartCoroutine(Logout());
        }
        else
        {
            confirmLoginText.text = "Please Login first";
            warningLoginText.text = "";
        }
    }

    private IEnumerator Logout()
    {
        // Sign out from Firebase authentication
        auth.SignOut();
        confirmLoginText.text = "Logged Out";
        warningLoginText.text = "";
        yield return null; // Yielding null to end the coroutine
    }
    public void ForgotPasswordButton()
    {
        // Call the forgot password coroutine passing the email
        StartCoroutine(ForgotPassword(emailLoginField.text));
    }

    private IEnumerator ForgotPassword(string _email)
    {
        // Call the Firebase auth send password reset email function
        Task PasswordResetTask = auth.SendPasswordResetEmailAsync(_email);

        // Wait until the task completes
        yield return new WaitUntil(() => PasswordResetTask.IsCompleted);

        if (PasswordResetTask.Exception == null)
        {
            // Password reset email sent successfully
            Debug.Log("Password reset email sent successfully");
            warningLoginText.text = "";
            confirmLoginText.text = "Password Reset Email Sent";
        }
        else
        {
            // If there are errors, handle them
            Debug.LogWarning($"Failed to send password reset email: {PasswordResetTask.Exception}");
            FirebaseException firebaseEx = PasswordResetTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Password Reset Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "User Not Found";
                    break;
            }
            warningLoginText.text = message;
            confirmLoginText.text = "";
        }
    }
    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();

                // Check if a user is already logged in
                if (auth.CurrentUser != null)
                {
                    // Redirect to the main scene if a user is already logged in
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                }
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }


    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        // Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        // Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            // If there are errors, handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
            confirmLoginText.text = "";
        }
        else
        {
            // User is now logged in
            // Now get the result
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";

            // Check if the user is logged in and load the "GameScene"
            if (User != null)
            {
                // Load the "GameScene"
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }
    private IEnumerator Register(string _email, string _password, string _username)
    {
        // List of allowed email domains
        List<string> allowedDomains = new List<string>
    {
        "gmail.com",
        "hotmail.com",
        "yahoo.com",
        // Add more allowed domains as needed
    };

        // Split the email into parts using "@" as delimiter
        string[] emailParts = _email.Split('@');

        if (emailParts.Length != 2 || !allowedDomains.Contains(emailParts[1]))
        {
            warningRegisterText.text = "Invalid Email Domain";
            confirmLoginText.text = "";
            yield break; // Exit the registration process
        }

        // Check if the email already exists
        bool emailExists = false;

        // Call the Firebase auth fetch providers for email function passing the email
        Task<IEnumerable<string>> FetchProvidersTask = auth.FetchProvidersForEmailAsync(_email);

        // Wait until the task completes
        yield return new WaitUntil(() => FetchProvidersTask.IsCompleted);

        if (FetchProvidersTask.Exception == null)
        {
            emailExists = FetchProvidersTask.Result != null && FetchProvidersTask.Result.Any();
        }
        else
        {
            Debug.LogWarning($"Failed to check email existence: {FetchProvidersTask.Exception}");
            warningRegisterText.text = "Failed to Check Email Existence";
            yield break; // Exit the registration process
        }

        if (emailExists)
        {
            warningRegisterText.text = "Email Already Exists";
            yield break; // Exit the registration process
        }

        if (_username == "")
        {
            // If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            // If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            // Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            // Wait until the task completes
            yield return new WaitUntil(() => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                // If there are errors, handle them
                Debug.LogWarning($"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                // User has now been created
                // Now get the result
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    // Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    // Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);

                    // Wait until the task completes
                    yield return new WaitUntil(() => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        // If there are errors, handle them
                        Debug.LogWarning($"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        // Username is now set
                        // Store the username in the Realtime Database
                        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                        reference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username)
                            .ContinueWith(task =>
                            {
                                if (task.IsFaulted)
                                {
                                    Debug.LogError("Failed to store username in the database: " + task.Exception);
                                    warningRegisterText.text = "Failed to store username in the database!";
                                }
                                else if (task.IsCompleted)
                                {
                                    // Successfully stored the username in the database
                                    Debug.Log("Username stored in the database successfully.");
                                }
                            });

                        // Now return to the login screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }


}