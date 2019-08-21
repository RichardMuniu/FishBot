using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles when player hits the end
public class EndingHandler : MonoBehaviour
{
    // Story mode game controller
    private StoryModeController gameController;

    // Handles collision between player and ending
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Tells game controller to end the level
            gameController.EndLevel();
        }
    } 

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<StoryModeController>();
    }
}
