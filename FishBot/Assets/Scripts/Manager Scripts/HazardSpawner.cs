using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles spawning hazards
public class HazardSpawner : MonoBehaviour
{
    public GameObject hazard;       // Hazard object
    public Vector3 spawnValues;     // Position of spawning
    public int hazardBoxWaveCount;  // Count of boxes spawned in one wave
    public float spawnWait;         // Wait between spawns of each box
    public float startWait;         // Beginning wait
    public float waveWait;          // Wait between waves

    // Routine for spawning hazards
    IEnumerator SpawnWaves()
    {
        // Beginning wait
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            // Loop through how many to spawn in a wave
            for (int i = 0;i < hazardBoxWaveCount;i++)
            {
                // Get a random x value near spawn values
                float xPos = Random.Range(-spawnValues.x, 75);
                // Defines spawn position
                Vector3 spawnPosition = new Vector3(xPos,spawnValues.y,spawnValues.z);
                // Identity rotation for the box
                Quaternion spawnRotation = Quaternion.identity;
                // Instantiate the box
                Instantiate(hazard, spawnPosition, spawnRotation);
                // Wait between spawns
                yield return new WaitForSeconds(spawnWait);
            }
            // Wait between waves
            yield return new WaitForSeconds(waveWait);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Starts the coroutine
        StartCoroutine (SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
