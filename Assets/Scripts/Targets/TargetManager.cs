using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public float spawnInterval;
    public float spawnTimer = 0f;

    public GameObject targetPrefab;
    public GameManagement gameManager;

    //spawn locations are "hard coded" into the scene, I think it would be better to do it fully randomly
    //However I did not have enough time to implement that so I took the less error prone approach
    public List<GameObject> spawnLocations; 

    // Update is called once per frame
    // Checks if the game is currently in the game state
    // If so it starts spawning targets randomly
    void Update()
    {
        if(gameManager.currentState == GameManagement.gameState.game)
        {
            SpawnTargets();
        }

    }

    /// <summary>
    /// Spawns targets after a certain interval that gets shorter as the game progresses
    /// </summary>
    private void SpawnTargets()
    {
        spawnTimer += Time.deltaTime;

        //spawns a target if the interval has passed and there is room for another in the scene
        if(spawnTimer >= spawnInterval && spawnLocations.Count > 0)
        {
            //Chooses a random location from the list of possible locations and creates a target
            int locationNum = Random.Range(0, spawnLocations.Count);
            
            //This line both creates a new target and sets its spawnedLocation parameter
            Instantiate(targetPrefab, spawnLocations[locationNum].transform).GetComponent<Target>().spawnedLocation = spawnLocations[locationNum];

            //removes location from location list to prevent multiple targets spawning in the same place
            spawnLocations.RemoveAt(locationNum); 

            spawnTimer -= spawnInterval;
            if(spawnInterval >= 1.5f) { spawnInterval -= .25f; }
        }
    }
}
