using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using FishBot;

// Handles the story chooser menu
public class StoryChooser : MonoBehaviour
{
    // Speed of letters appearing
    public float letterPause;
    // Text to be animated
    public Text chooserText;
    // Screens
    public GameObject loadingScreen;
    public GameObject storyChooserScreen;
    // Loading text and slider
    public Text levelLoaderText;
    public Slider progressBar;
    // Fading image and speed
    public Image blackOverlay;
    public float fadeSpeed = 1.5f;

    // Temporary holder for chooserText's content
    private string _message;
    // Variable for current user and global user manager
    private User _currentUser;
    private UserManager _userManager = UserManagerScript.userManager;

    // Sound while typing
    private AudioSource[] _sounds;
    private List<AudioSource> _typingSounds = new List<AudioSource>();
    private AudioSource _spaceBar;
    static System.Random rand = new System.Random();

    // When chooser is enabled, load the sounds
    private void OnEnable()
    {
        LoadSounds();
    }

    // Set current user on start
    void Start()
    {
        _currentUser = _userManager.SelectedUserCopy;
    }

    // Load sound lists
    public void LoadSounds()
    {
         _sounds = GetComponents<AudioSource>();
        // make a list of typing sounds
        _typingSounds.Add(_sounds[0]);
        _typingSounds.Add(_sounds[1]);
        // some extra fun sounds
        _spaceBar = _sounds[2];
    }

    // Start the coroutine of typing
    public void DisplayChooserInstructions()
    {
        if(transform.gameObject.activeSelf)
        {
            _message = chooserText.text;
            chooserText.text = "";
            StartCoroutine(TypeText());
        }
    }

    // Courotine of typing
    IEnumerator TypeText()
    {
        foreach (char letter in _message.ToCharArray())
        {
            
            if (letter == ' ')
            {
                _spaceBar.Play();
            }
            
            else
            {
                //play one of two typing sounds at random
                int r = rand.Next(_typingSounds.Count);
                _typingSounds[r].Play();
            } 
            chooserText.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
    }

    // Handles pressing story mode button
    public void PlayStoryMode()
    {
        StartCoroutine(FadeIntoLevel());
    }

    // Handles pressing endless mode button
    public void PlayEndlessMode()
    {
        LoadLevel(2);
    }

    // Loaded the level with a certain index
    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(AsyncLevelLoader(levelIndex));
    }

    // Routine to load level
    IEnumerator AsyncLevelLoader(int levelIndex)
    {
        // Setup AsyncOperation
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(levelIndex);
        // Activates the loading screen and deactivates chooser menu
        loadingScreen.SetActive(true);
        storyChooserScreen.SetActive(false);

        // Loading Text
        if(levelIndex == 2)
        {
            levelLoaderText.text = "ENDLESS MODE";
        }
        // Loading Text
        else
        {
            // Recall the offset by 2 in build settings
            levelLoaderText.text = String.Format("Level {0}", levelIndex - 2);
        }

        // Loading progression
        while (!loadOp.isDone)
        {
            float progress = Mathf.Clamp01(loadOp.progress / 0.9f);
            progressBar.value = progress;
            yield return null;
        }
    }

    // Routine to fade into level
    IEnumerator FadeIntoLevel()
    {
        // Fade
        blackOverlay.gameObject.SetActive(true);
        blackOverlay.canvasRenderer.SetAlpha(0.0f);
        blackOverlay.CrossFadeAlpha(1.0f, fadeSpeed, false);
        // Wait
        yield return new WaitForSeconds(2.0f);
        // Load
        int currentLevel = Convert.ToInt32(_currentUser.GetOptionValue("Level"));
        LoadLevel(currentLevel + 2); // add two since level 1's index in build settings is 3
    }
}
