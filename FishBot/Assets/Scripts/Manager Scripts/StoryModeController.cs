using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controls the main features of the level
// Handles checkpoints, ending level calls, fish death calls, pause menu calls, fish and cam constant movement
public class StoryModeController : MonoBehaviour
{
	// Movement offset for fish and cam
    public float xOffset;
	// Objects
	public GameObject fish; // The fish
    public GameObject cam; // The camera
	public GameObject blackImage; // The fading image
	public GameObject endOfLevelPanel; // End of level panel
	public Image endBg; // Black backgournd for end of level panel
	public float fadeSpeed = 1.5f; // Fading speed
	
	public static bool allowedToPause = false; // Static variable to manages allowing to pause or not

    private SpriteRenderer blackImageSpriteComp; // Private SpriteRenderer component of the black image
	private Transform fishTransform; // Transform of the fish
    private Transform cameraTransform; // Transform of the camera

    private Vector3 checkpointFishPos; // Stores checkpoint position for fish
    private Vector3 checkpointCamPos; // Stores checkpoint position for camera

    // Start is called before the first frame update
    void Start()
    {
		// Setup private variables
        allowedToPause = true;
		fishTransform = fish.GetComponent<Transform>();
        cameraTransform = cam.GetComponent<Transform>();
	    checkpointFishPos = new Vector3(fishTransform.position.x,0,0);
        checkpointCamPos = new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);
		blackImageSpriteComp = blackImage.GetComponent<SpriteRenderer>();
		////////////////////////////////////////////////////////////////////////// SAVE CURRENT LEVEL
    }

    // Update is called once per frame
    void Update()
    {
		// Keep moving the camera and the player if the game is not paused
        if(!PauseMenu.GameIsPaused)
            MoveCamPlayer();
			MovePlayer();
		////////////////////////////////////////////////////////////////////////// Handle movement when level ends here
    }

	// Moves the cam to the right by an offset
    void MoveCamPlayer()
    {
        cam.transform.position = new Vector3(cam.transform.position.x + xOffset, cam.transform.position.y, cam.transform.position.z);
    }
	// Moves the fish to the right by an offset
	void MovePlayer()
	{
		fish.transform.position = new Vector3(fish.transform.position.x + xOffset, fish.transform.position.y, fish.transform.position.z);
	}

	// Callback for ending the level
    public void EndLevel()
    {
		////////////////////////////////////////////////////////////////////////// SAVE CURRENT LEVEL HERE
		// update current user level and score
		// No more allowed to pause
        allowedToPause = false;
		// Fade ending panel
		endOfLevelPanel.SetActive(true);
		endBg.canvasRenderer.SetAlpha(0.0f);
        endBg.CrossFadeAlpha(0.5f, fadeSpeed, false);
	}

	// Callback for returning to mainmenu scene
	public void BackToMain()
	{
		////////////////////////////////////////////////////////////////////////// SAVE CURRENT LEVEL HERE
        SceneManager.LoadScene(0);
	}

	// Jump to next level
	public void NextLevel()
	{
		// Go to next scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	// Callback for when the fish dies
    public void GameOver()
    {   
        StartCoroutine(CheckPointRoutine());
    }

	// Updates checkpoint position
    public void SavePosition()
    {
	    checkpointFishPos = new Vector3(fishTransform.position.x,0,0);
        checkpointCamPos = new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);
	}
			
	// Routine for fading and respawning when the fish dies
	public IEnumerator CheckPointRoutine()
	{
		// Set allowed to pause false
		allowedToPause = false;
			            
		// Make a simpler reference i to the black image
		SpriteRenderer i = blackImageSpriteComp;
			
		// Wait in the beginning
		yield return new WaitForSeconds(1.5f);
			
		// Fade in slower
		float t = 1.2f;
			
		// Fade In
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
			            
		// Wait some time
		yield return new WaitForSeconds(0.5f);
		// Change Position of the fish and reset forces
		fishTransform.position = checkpointFishPos;
        cameraTransform.position = checkpointCamPos;
		fish.GetComponent<Rigidbody>().velocity = Vector3.zero;
		fish.SetActive(true);
			
		// Fade Out faster
		t = 0.5f;
		// Fade Out
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
		    i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
			
		// Set allowed to pause true
		allowedToPause = true;
	}

}
