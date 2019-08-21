using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroyer script that handles collision with hazard boxes.
public class DestroyAtContact : MonoBehaviour
{
    // Hazard explosion animation game object
    public GameObject explosion;
    // Player explosion animation game object
    public GameObject playerDiesExplosion;
    // Score to be added upon destroying with projectile
    private int scoreValue = 5;
    // Story mode game controller
    private StoryModeController storyGameController;
    // Endless mode game controller
    private GameController endlessGameController;

    // Collision handling
    void OnTriggerEnter(Collider other) {
        // If player collides with hazard
        if (other.tag == "Player")
        {
            // Hazard explodes
            Instantiate(explosion, transform.position, transform.rotation);
            // Player goes inactive
            other.gameObject.SetActive(false);
            // Player explosion animation plays
            Instantiate(playerDiesExplosion, other.transform.position, other.transform.rotation);

            // Game controller is informed that the player has died
            if(endlessGameController == null)
            {
                storyGameController.GameOver();
            }
            else if(storyGameController == null)
            {
                endlessGameController.GameOver();
            }
            else
            {
                throw new Exception("Should not have arrived here.");
            }

            // Destroy the hazard
            Destroy(gameObject);
            return;
        }
        // If projectile collides with hazard
        if (other.tag == "Projectile")
        {
            // Hazard Explodes
            Instantiate(explosion, transform.position, transform.rotation);
            // Game controller is informed to add score
            if(endlessGameController != null)
            {
                endlessGameController.AddScore(scoreValue);
            }
            // Destroy the projectile
            Destroy(other.gameObject);
            // Destroy the hazard
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        // Try getting story mode controller
        storyGameController = gameControllerObject.GetComponent<StoryModeController>();
        if(storyGameController == null)
        {
            endlessGameController = gameControllerObject.GetComponent<GameController>();
        }
    }
}
