using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// User Management Initialization for main menu
// Main purpose was for handling what to do when Main Menu scene is opened
public class ApplicationStart : MonoBehaviour
{
    // Main menu screen object
    public GameObject mainMenuScreen;
    // Landing page object
    public GameObject landingPage;
    // Powered by unity screen object
    public GameObject poweredByUnityScreen;

    void Start()
    {
        // If no user is selected, go to landing page
        if(UserManagerScript.userManager.SelectedUserIndex == -1)
        {
            UserManagerScript.folderPath = Application.dataPath + "/Saves";
            UserManagerScript.ReadUsers();
            poweredByUnityScreen.SetActive(true);
            landingPage.SetActive(true);
        }
        // If a user is selected, go straight to main menu
        else
        {
            mainMenuScreen.SetActive(true);
        }
    }
}
