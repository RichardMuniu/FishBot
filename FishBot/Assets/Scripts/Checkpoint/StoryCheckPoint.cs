using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles Checkpoints for Endless Mode
public class StoryCheckPoint : MonoBehaviour
{
    // Story mode game controller
    private StoryModeController gameController;

    // Check if player collided with the checkpoint
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Save positions in gamecontroller
            gameController.SavePosition();
            // Deactivate the checkpoint
            gameObject.SetActive(false);
        }
    } 

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<StoryModeController>();
    }
}
