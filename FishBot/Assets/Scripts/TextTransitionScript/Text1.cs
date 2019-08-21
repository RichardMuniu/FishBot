using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Handles text transitioning with fading
public class Text1 : MonoBehaviour
{
    // List of texts to display
    List<string> _textList = new List<string>();
    // List of times which texts above stay on screen for
    List<float> _timeOnScreen = new List<float>();
    // Text component of the object to which this script is attached
    Text textComp;
    
    void Start()
    {
        // Initializes and Resets TextComp
        textComp = this.GetComponent<Text>();
        textComp.text = "";
        textComp.color = new Color(textComp.color.r, textComp.color.g, textComp.color.b, 0);

        // Initialize Lists
        _textList.Add("Level 1");
        _timeOnScreen.Add(1f);
        _textList.Add("Sea");
        _timeOnScreen.Add(1f);

        // Starts the routine
        StartCoroutine(DisplayLevelText());
    }

    // Routine that displays text with fading
    public IEnumerator DisplayLevelText()
    {
        // Waits Initially
        yield return new WaitForSeconds(2);

        // Cycles through text
        for(int j = 0; j < _textList.Count; j++)
        {
            // Put text
            textComp.text = _textList[j];
            // Make a simpler reference i
            Text i = textComp;
            // How fast should fading be
            float t = 1.0f;

            // Fade In
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
            while (i.color.a < 1.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
                yield return null;
            }
            // Wait text
            yield return new WaitForSeconds(_timeOnScreen[j]);

            // Fade Out
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            while (i.color.a > 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }

            // Wait black
            yield return new WaitForSeconds(1);
        }
        
        // Now load next level after finishing displaying
        LoadNextLevel();

    }

    // Load next level
    void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
