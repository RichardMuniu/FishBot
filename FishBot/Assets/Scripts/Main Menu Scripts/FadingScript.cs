using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script for handling fading
public class FadingScript : MonoBehaviour
{
    // Screen Objects
    public GameObject landing;
    public GameObject login;
    public GameObject signUp;
    public GameObject rename;
    public GameObject menu;
    public GameObject options;
    public GameObject leader;
    public GameObject chooser;
    public GameObject help;

    // Black Overlay
    public Image blackOverlay;
    // An overlay object that prevents clicking
    public GameObject preventClick;
    // Fading speeds
    public float fadeSpeed = 1.0f;

    // Private variables for ease of typing
    private Image i; // Black overlay
    private float t; // Fading speed

    public void Start()
    {
        // Setup
        preventClick.SetActive(false);
        i = blackOverlay;
        t = fadeSpeed;
    }
    
    ////////////////////////////////////////////////////////////////////
    // Fading Methods
    ////////////////////////////////////////////////////////////////////
    public void LandingToLogin(){StartCoroutine(ILandingToLogin());}
    public void LoginToSignUp(){StartCoroutine(ILoginToSignUp());}
    public void SignUpToLogin(){StartCoroutine(ISignUpToLogin());}
    public void LoginToRename(){StartCoroutine(ILoginToRename());}
    public void RenameToLogin(){StartCoroutine(IRenameToLogin());}
    public void LoginToMenu(){StartCoroutine(ILoginToMenu());}
    public void MenuToLogin(){StartCoroutine(IMenuToLogin());}
    public void MenuToOptions(){StartCoroutine(IMenuToOptions());}
    public void OptionsToMenu(){StartCoroutine(IOptionsToMenu());}
    public void MenuToLeader(){StartCoroutine(IMenuToLeader());}
    public void LeaderToMenu(){StartCoroutine(ILeaderToMenu());}
    public void MenuToChooser(){StartCoroutine(IMenuToChooser());}
    public void ChooserToMenu(){StartCoroutine(IChooserToMenu());}
    public void MenuToHelp(){StartCoroutine(IMenuToHelp());}
    public void HelpToMenu(){StartCoroutine(IHelpToMenu());}

    ////////////////////////////////////////////////////////////////////
    // Fading Routines
    ////////////////////////////////////////////////////////////////////
    public IEnumerator ILandingToLogin()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        landing.SetActive(false);
        login.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator ILoginToSignUp()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        login.SetActive(false);
        signUp.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator ISignUpToLogin()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        signUp.SetActive(false);
        login.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator ILoginToRename()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        login.SetActive(false);
        rename.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IRenameToLogin()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        rename.SetActive(false);
        login.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator ILoginToMenu()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        login.SetActive(false);
        menu.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IMenuToLogin()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        menu.SetActive(false);
        login.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IMenuToOptions()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        menu.SetActive(false);
        options.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IOptionsToMenu()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        options.SetActive(false);
        menu.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IMenuToLeader()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        menu.SetActive(false);
        leader.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator ILeaderToMenu()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        leader.SetActive(false);
        menu.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IMenuToChooser()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        menu.SetActive(false);
        chooser.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IChooserToMenu()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        chooser.SetActive(false);
        menu.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IMenuToHelp()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        menu.SetActive(false);
        help.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }
    public IEnumerator IHelpToMenu()
    {
        // Disable Input
        preventClick.SetActive(true);
        // Fade In
        yield return StartCoroutine(FadeInBlack());
        // Switch
        help.SetActive(false);
        menu.SetActive(true);
        // Fade Out
        yield return StartCoroutine(FadeOutBlack());
        // Enable Input
        preventClick.SetActive(false);
    }

    public IEnumerator FadeInBlack()
    {
        // Fade In
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }    
    }

    public IEnumerator FadeOutBlack()
    {
        // Fade Out
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

        // Enable Input    
    }

}
