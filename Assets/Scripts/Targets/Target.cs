using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject gameManager;

    //keeps track of location it was spawned so when the target is destroyed
    //that location can be added back to the list of valid spawn locations
    public GameObject spawnedLocation; 

    void OnEnable()
    {
        gameManager = GameObject.Find("GameManager");
        if(gameManager.GetComponent<GameManagement>().currentState == GameManagement.gameState.game)
        {
            gameManager.GetComponent<GameData>().AddTarget(gameObject);
        }

    }

    /// <summary>
    /// Checks for a bolt hitting the target
    /// If the collision happens in the tutorial or the game end screen it starts a new game
    /// If it happens during the game it adds to the player score and removes the target and bolt from the scene
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        //if shot during tutorial or end game screen
        if(gameManager.GetComponent<GameManagement>().currentState != GameManagement.gameState.game)
        {
            if(collision.collider.gameObject.GetComponent<Bolt>() != null)
            {
                gameManager.GetComponent<GameManagement>().StartGame();
                Destroy(gameObject);
                Destroy(collision.collider.gameObject);
            }
        }

        //if shot during gameplay
        else if(collision.collider.gameObject.GetComponent<Bolt>() != null)
        {
            gameManager.GetComponent<GameData>().TargetHit(gameObject);
            Destroy(gameObject);
            gameManager.GetComponent<GameData>().PopBolt();
            Destroy(collision.collider.gameObject);
        }
    }

}
