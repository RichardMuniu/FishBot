using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Handles displaying the monologue of the fish
public class TextAnimator : MonoBehaviour
{
    // Black image
    public Image overlayImage;
    // Fading speed
    public float fadeSpeed = 1.5f;
    // Text difplayed object
    public TextMeshProUGUI displayText;
    // Sentences to be displayed
    public string[] sentences;
    // Typing speed of display
    public float typingSpeed = 0.5f;
    // Continue button object
    public GameObject continueButton;
    // Animator for the text
    public Animator textAnim;

    // Tracks index of current sentence
    private int sentenceIndex;
    // Typing sounds array
    private AudioSource[] sounds;
    // Typing sounds list
    private List<AudioSource> typingSounds = new List<AudioSource>();
    // Space bar sound
    private AudioSource spaceBar;
    // Continue press sound
    private AudioSource continueSwoosh;
    
    static System.Random rnd = new System.Random();

    void Start()
    {
        // Get list of sounds
        sounds = GetComponents<AudioSource>();
        // Make a list of typing sounds
        typingSounds.Add(sounds[0]);
        typingSounds.Add(sounds[1]);
        // Some extra fun sounds
        spaceBar = sounds[2];
        continueSwoosh = sounds[3];
        // Initialize/rese4t displayText
        displayText.text = "";
        // Fade overlay out, begin scene at the end of the routine
        overlayImage.gameObject.SetActive(true);
        StartCoroutine(FadeOut(overlayImage)); // The coroutines call each other
    }

    // Fade out then start typing current sentence
    IEnumerator FadeOut(Image image)
    {
        image.CrossFadeAlpha(0, fadeSpeed, false);
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(TypeCurrentSentence());
    }

    // Fade In
    IEnumerator FadeIn(Image image)
    {
        image.CrossFadeAlpha(1, fadeSpeed, false);
        yield return new WaitForSeconds(2f);
    }

    // Type the current sentence
    IEnumerator TypeCurrentSentence()
    {
        foreach (char letter in sentences[sentenceIndex].ToCharArray())
        {
            if (letter == ' ')
            {
                // Play spacebar sound
                spaceBar.Play();
            }
            else
            {
                // Play one of two typing sounds at random
                int r = rnd.Next(typingSounds.Count);
                typingSounds[r].Play();
            }

            // Display
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Callback when continue button is clicked
    public void TypeNextSentence()
    {
        // Play continue sound
        continueSwoosh.Play();
        textAnim.SetTrigger("Change");
        // Deactivate continue button
        continueButton.SetActive(false);
        
        // If there are more sentences
        if (sentenceIndex < sentences.Length - 1)
        {
            // Increment sentence counter
            sentenceIndex++;
            // Reset display text
            displayText.text = "";
            // Type current sentence
            StartCoroutine(TypeCurrentSentence());
        }
        else
        {
            // Nothing to type
            displayText.text = "";
            // Darkness fades in 
            StartCoroutine(FadeIn(overlayImage));
            // Go to next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void Update()
    {
        // Check to prevent scrambled letters and restrict spam clicking
        // Only displays continue button when typing is done 
        if(displayText.text == sentences[sentenceIndex])
        {
            continueButton.SetActive(true);
        }
    }
}
