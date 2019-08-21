using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FishBot;
using System;

// Hadles the pause menu
public class PauseMenu : MonoBehaviour
{
    // A public global variable to keep track of whether the game is paused
    public static bool GameIsPaused = false;
    // Pause menu object
    public GameObject pauseMenuUI;
    // Global usermanager variable
    private UserManager userManager = UserManagerScript.userManager;
    // Sliders
    public Slider volumeSlider;
    public Slider brightnessSlider;
         
    void Start()
    {
        // Get current user
        User currentUser = userManager.SelectedUserCopy;
        // Update Sliders
        float userVolume = Convert.ToSingle(currentUser.GetOptionValue("Volume"));
        volumeSlider.value = userVolume/100;
        float userBrightness = Convert.ToSingle(currentUser.GetOptionValue("Brightness"));
        brightnessSlider.value = userBrightness/100;    
    }

    // Update is called once per frame
    void Update()
    {
        // Input Key down Escape
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else if(StoryModeController.allowedToPause)
            {
                Pause();
            }
        }
    }

    // Resume button pressed callback
    public void Resume()
    {
        // Unpause
        pauseMenuUI.SetActive(false);
        // Unfreeze time
        Time.timeScale = 1f;
        // Set game is paused to false
        GameIsPaused = false;
    }

    // Changing volume callback
    void ChangeVolume()
    {
        userManager.UpdateOptionSelectedUser("Volume", Convert.ToInt32(Math.Round(volumeSlider.value*100)));
        ////////////////////// NEED CALLBACK HERE //////////////////////
    }
    void ChangeBrightness()
    {
        userManager.UpdateOptionSelectedUser("Brightness", Convert.ToInt32(Math.Round(brightnessSlider.value*100)));
        ////////////////////// NEED CALLBACK HERE //////////////////////
    }

    // Pause method
    void Pause()
    {
        // Activate pause-menu
        pauseMenuUI.SetActive(true);
        // Freeze time
        Time.timeScale = 0f;
        // Set game is paused to true
        GameIsPaused = true;
    }

    // Quit button callback
    public void QuitGame()
    {
        // Unfreeze time
        Time.timeScale = 1f;
        // Save users
        UserManagerScript.SaveUsers();
        // Load MainMenu
        SceneManager.LoadScene(0);
    }
}
