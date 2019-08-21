using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FishBot;
using System.IO;

// Handles everything that happens in the login screen
public class LoginScreen : MonoBehaviour
{
    // Objects related to login screen
    public GameObject signUpScreen;       // Sign up screen
    public GameObject renameScreen;       // Renaming screen
    public GameObject loginScreen;        // Main login screen
    public GameObject mainMenuScreen;     // Main menu screen
    public InputField signUpInput;        // Sign up input field
    public InputField renameInput;        // Renaming input field
    public GameObject signUpErrorLabel;   // Sign up error label
    public GameObject renameErrorLabel;   // Rename error label
    public GameObject scrollContent;      // The scrolling list object
    public GameObject userButtonTemplate; // Template for a user button
    public GameObject fadeObject;         // Fading controller object

    private Text renameErrorTextComp;     // Private text component of rename error label
    private Text signUpErrorTextComp;     // Private text component of sign up error label

    private UserManager _userManager = UserManagerScript.userManager; // Private user manager that refers to the global manager
    private List<GameObject> _buttonList = new List<GameObject>();    // Private list of buttons

    private FadingScript fadeScript; // Private variable for the fading script
    private bool goingBack = false;  // Private variable to prevent multiple user submissions while fading

    public void Start()
    {
        // Setup
        fadeScript = fadeObject.GetComponent<FadingScript>();
        renameErrorTextComp = renameErrorLabel.GetComponent<Text>();
        signUpErrorTextComp = signUpErrorLabel.GetComponent<Text>();
        // Load scrolling content
        ScrollLoad();
    }

    // Login Button Press Handler
    public void Login()
    {
        // Return if no user is selected
        if(_userManager.SelectedUserIndex == -1)
        {
            return;
        }
        // Otherwise, fade to main menu
        fadeScript.LoginToMenu();
    }

    // Clicking on scroll handler
    public void ScrollClick()
    {
        // Name of clicked user
        string clickedName = EventSystem.current.currentSelectedGameObject.name;
        // If no user was selected or if the user selected is not the one pressed
        if(_userManager.SelectedUserIndex == -1 || clickedName != _userManager.SelectedUserCopy.UserName)
        {
            // Select the pressed user
            _userManager.SelectUser(clickedName);
            // Get the button clicked
            Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            // Color all buttons white
            foreach(GameObject button in _buttonList)
            {
                button.GetComponent<Image>().color = Color.white;
            }
            // Color the selected button gray
            clickedButton.GetComponent<Image>().color = new Color(50f/255f, 50f/255f, 50f/255f);
        }
        // Otherwise, the user clicked was a selected user
        else
        {
            // Deselect the user
            _userManager.Deselect();
            // Color all buttons white
            foreach(GameObject button in _buttonList)
            {
                button.GetComponent<Image>().color = Color.white;
            }
        }
    }

    // SignUp Button Press Handler
    public void SignUp()
    {
        // Resets error label and input field
        signUpErrorTextComp.text = "";
        signUpInput.text = "";
        // Fade to sign up screen
        fadeScript.LoginToSignUp();
        // Select the input field
        signUpInput.Select();

    } 

    // Rename Button Press Handler
    public void Rename()
    {
        // Only proceed if some user is selected
        if(_userManager.SelectedUserIndex != -1)
        {
            // Resets error label and input field
            renameErrorTextComp.text = "";
            renameInput.text = "";
            // Fade to rename screen
            fadeScript.LoginToRename();
            // Select the input field
            renameInput.Select();
        }
    } 

    // Handles submission of user in sign up screen
    public void UserSubmit()
    {
        // Fading is happening so reject additional presses
        if(goingBack == true)
        {return;}
        // Get the username
        string username = signUpInput.text;
        // Error if manager already has the name
        if(_userManager.HasUser(username))
        {
            signUpErrorTextComp.text = "Already exists!";
            return;
        }
        // Variable to check if adding user passes
        bool pass = false;
        try
        {
            // Try adding the user
            _userManager.AddUser(new User(username));
            // User has been added successfully if the program gets to this line so set pass true
            pass = true;
        } catch{}
        
        // If adding was not successful then the username is invalid
        if(pass == false)
        {
            signUpErrorTextComp.text = "Username Invalid!";
            return;
        }
        // Otherwise, adding the user proceeded successfully
        else
        {
            // Deselect in usermanager
            _userManager.Deselect();
            // Fade back to login
            fadeScript.SignUpToLogin();
            // Load scrolling content
            ScrollLoad();
        }
    }

    // Handles submission of user in rename screen
    public void RenameUserSubmit()
    {
        // If fading back to login screen, ignore.
        if(goingBack == true)
        {return;}
        // Get the username
        string username = renameInput.text;
        // Error if manager already has the name
        if(_userManager.HasUser(username))
        {
            renameErrorTextComp.text = "Already exists!";
            return;
        }
        // Variable to check if renaming user passes
        bool pass = false;
        try
        {
            // Get a copy of the selected user
            User user = _userManager.SelectedUserCopy;
            // Change username of the copy
            user.UserName = username;
            // Reconfigure the selected user
            _userManager.ReconfigureSelectedUser(user);
            // User has been renamed successfully if the program gets to this line so set pass true
            pass = true;
        } catch{}
        
        // If renaming was not successful
        if(pass == false)
        {
            renameErrorTextComp.text = "Username Invalid!";
        }
        // Otherwise,
        else
        {
            // Deselect usermanager
            _userManager.Deselect();
            // Fade back to login
            fadeScript.RenameToLogin();
            // Load scroll content
            ScrollLoad();
        }
    }

    // Handles the pressing of back button in rename and sign-up screens
    public void BackButton()
    {
        // Fading is happening
        goingBack = true;
        // Reset labels and fields
        signUpInput.text = "";
        renameInput.text = "";
        signUpErrorTextComp.text = "";
        renameErrorTextComp.text = "";
        // If signUp screen is up, fade from sign up to login
        if(signUpScreen.activeSelf)
        {
            fadeScript.SignUpToLogin();
        }
        // If rename screen is up, fade from rename to login
        else if(renameScreen.activeSelf)
        {
            fadeScript.RenameToLogin();
        }
        // Fading ended
        goingBack = false;
    }

    // Handles pressing of remove in login screen
    public void RemoveUser()
    {
        // Ignore if no user is selected
        if(_userManager.SelectedUserIndex == -1)
        {
            return;
        }
        // Remove the user selected otherwise and update scroll content
        else
        {
            _userManager.RemoveUser(_userManager.SelectedUserCopy.UserName);
            ScrollLoad();
        }
    }

    // Saves Users
    public void SaveUsers()
    {
        UserManagerScript.SaveUsers();
    }

    // Loads the scroll content
    public void ScrollLoad()
    {
        // Saves the users
        SaveUsers();
        // Destroy all currently active children (avoid the inactive template)
        foreach (Transform child in scrollContent.transform)
        {
            if(child.gameObject.activeSelf)
                Destroy(child.gameObject);
        }
        // Get list of usernames
        List<string> userNameList = _userManager.UserNameListCopy;
        // Creates new list of buttons
        _buttonList = new List<GameObject>();
        // Loops through the names and creates buttons
        foreach(string name in userNameList)
        {
            // Instantiate the template
            GameObject userButton = Instantiate(userButtonTemplate) as GameObject;
            // Add the instance to the button list
            _buttonList.Add(userButton);
            // Change the name of the button
            userButton.name = name;
            // Activate the button
            userButton.SetActive(true);
            // Get the text component of the button
            Text textComp = userButton.GetComponentInChildren<Text>();
            // Change it to the name of the user
            textComp.text = name;
            // Set the parent of the button created to be the parent of the template
            userButton.transform.SetParent(userButtonTemplate.transform.parent);
        }
    }
}
