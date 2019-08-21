using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Game controller for endless mode
public class GameController : MonoBehaviour
{
    // Testing for checkpoints
    public bool checkpointFunctionality = true;
    // Static variable to manages allowing to pause or not
	public static bool allowedToPause = false;
    // Fish object
	public GameObject fish;
    // Black Image Object
	public GameObject blackImage;

    // Spawn Related Objects
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardBoxWaveCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    // Texts for information displayed
    public TextMesh scoreText;
    public TextMesh restartText;
    public TextMesh gameOverText;

    // Bools
    private bool gameOver;
    private bool restart;
    private int score;

    // Spawns boxes
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0;i < hazardBoxWaveCount;i++)
            {
                float xPos = Random.Range(-spawnValues.x, 75);
                Vector3 spawnPosition = new Vector3(xPos,spawnValues.y,spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            // Handles when the fish dies
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    // Updates text of store
    void UpdateScore()
    {
        scoreText.text = "Score: " + score; //automatically converted to single string of text characters
    }

    // Adds score and updates the text
    public void AddScore(int newScoreValue)
    {
        //Debug.Log(score);
        score += newScoreValue;
        //Debug.Log(newScoreValue);
        UpdateScore();
    }

    // When the fish dies
    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}
