using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using FishBot;
using System;

// Hadles MainMenu screen
public class MainMenu : MonoBehaviour
{
    // Texts in mainmenu screen
    public Text welcomeText;
    public Text scoreText;
    public Text progressText;

    // Volume and brightness objects
    public AudioMixer audioMixer;
    public GameObject colorFilter;
    // Options menu script variable
    public OptionsMenu _optionMenu;


    // Global User Manager
    private UserManager userManager = UserManagerScript.userManager;
    // Private variable for current selected user
    private User currentUser;

    // Runs whenever main menu is activated
    void OnEnable()
    {
        // Save users
        UserManagerScript.SaveUsers();
        // Set current user
        currentUser = userManager.SelectedUserCopy;
        // Display the user's information
        DisplayUserInfo(currentUser);
        
        // Set-up volume
        int minVol = -60;
        float currVol = Convert.ToInt32(currentUser.GetOptionValue("Volume"))/100f;
        audioMixer.SetFloat("volume", (-minVol)*(currVol-1));

        // Set-up brightness
        float minBright = 0.6f;
        Image image = colorFilter.GetComponent<Image>();
        var tempColor = image.color;
            // note the inverse alpha behavior relative to the slider
        tempColor.a = 1f - ((1-minBright)*(Convert.ToInt32((currentUser.GetOptionValue("Brightness")))-1) + 1); 
        image.color = tempColor;
    }

    // Handles quitting
    void Exit()
    {
        Application.Quit();
    }

    // Updates information displayed in main menu
    void DisplayUserInfo(User currentUser)
    {
        // Displays main menu welcome text and level/score updates
        welcomeText.text = string.Format("Welcome, {0}!", currentUser.UserName);
        progressText.text = string.Format("Current Level: {0}", currentUser.GetOptionValue("Level").ToString());
        scoreText.text = string.Format("Score: {0}", currentUser.GetOptionValue("Score").ToString());
        
        // Set slider values to user custom presets
        float userVolume = Convert.ToSingle(currentUser.GetOptionValue("Volume"));
        _optionMenu.volumeSlider.value = userVolume / 100;
        float userBrightness = Convert.ToSingle(currentUser.GetOptionValue("Brightness"));
        _optionMenu.brightnessSlider.value = userBrightness / 100;
    }
}
