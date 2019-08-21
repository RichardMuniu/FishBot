using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using FishBot;

// Handles options screen
public class OptionsMenu : MonoBehaviour
{
    // Sliders
    public Slider volumeSlider;
    public Slider brightnessSlider;

    // Global usermanager
    private UserManager userManager = UserManagerScript.userManager;
    // Private currentUser
    private User currentUser;
    
    // Brightness and volume handlers
    public GameObject colorFilter;
    public AudioMixer audioMixer;

    // Re-Update Volume based on slider
    public void ChangeVolume()
    {
        userManager.UpdateOptionSelectedUser("Volume", Convert.ToInt32(Math.Round(volumeSlider.value * 100)));
        SetVolume(volumeSlider.value);
    }

    // Re-Update Brightness based on slider
    public void ChangeBrightness()
    {
        userManager.UpdateOptionSelectedUser("Brightness", Convert.ToInt32(Math.Round(brightnessSlider.value * 100)));
        SetBrightness(brightnessSlider.value);
    }

    // Set internal volume
    void SetVolume(float volume)
    {
        int minVol = -60;
        audioMixer.SetFloat("volume", (-minVol)*(volume-1));
    }

    // Set internal brightness
    void SetBrightness(float brightness)
    {
        float minBright = 0.6f;
        Image image = colorFilter.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f - ((1-minBright)*(brightness-1) + 1); // note the inverse alpha behavior relative to the slider
        image.color = tempColor;
    }

}
