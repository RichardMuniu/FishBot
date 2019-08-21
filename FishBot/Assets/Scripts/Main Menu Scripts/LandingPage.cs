using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles the landing page
public class LandingPage : MonoBehaviour
{
    public Image unityIntro;                   // Unity image
    public Image blackOverlay;                 // Black image
    public GameObject clickAnywhere;           // Click anywhere label
    public GameObject loginScreen;             // Login screen
    public GameObject poweredByUnityScreen;    // Powered by unity screen
    public StoryChooser storyChooser;          // Story chooser script
    public GameObject fadingObject;            // Fading controller
    
    public float flashInterval;                // Flashing speed            
    public float fadeSpeed = 1.5f;             // Speed of fading for powered by unity scene
    private bool clickAllowed = false;         // Clicking in landing page would only be allowed after powered by unity scene has fully proceeded

    private FadingScript fadeScript;           // Fading script component of the controller  

    void Start()
    {
        // Get fading script
        fadeScript = fadingObject.GetComponent<FadingScript>();
        // Repeat the routine of flashing
        InvokeRepeating("FlashLabel", 0, flashInterval);
        // For fading purposes
        unityIntro.canvasRenderer.SetAlpha(0.0f);
        // Start nested coroutines for animating the powered by unity screen
        StartCoroutine(StartFades());
    }

    void Update()
    {
        // Check input and if clicking is allowed
        if (Input.GetMouseButtonDown(0) && clickAllowed == true)
        {
            // Clicking no more allowed
            clickAllowed = false;
            // Deactivate Powered ByUnity Screen
            poweredByUnityScreen.SetActive(false);
            // Fade from landing to login
            fadeScript.LandingToLogin();
        }
    }

    // Switch status of the clickanywhere text
    void FlashLabel()
    {
        if(clickAnywhere.activeSelf)
            clickAnywhere.SetActive(false);
        else
            clickAnywhere.SetActive(true);
    }

    // Routine for powered by unity screen
    public IEnumerator StartFades()
    {
        // Fade In unity picture
        FadeIn(unityIntro);
        // Wait for two seconds
        yield return new WaitForSeconds(2);
        // Fade out unity picture
        FadeOut(unityIntro);
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);
        // Fade Out black background
        FadeOut(blackOverlay);
        // Allow Clicking
        clickAllowed = true;
    }

    // Fade In image method
    void FadeIn(Image image)
    {
        image.CrossFadeAlpha(1, fadeSpeed, false);
    }

    // Fade out image method
    void FadeOut(Image image)
    {
        image.CrossFadeAlpha(0, fadeSpeed, false);
    }
}
