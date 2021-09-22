using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{

    public enum gameState { tutorial, game, endGame };
    public gameState currentState;
    public GameData gameData;
    public Canvas scoreBoard;
    private Text scoreText;

    public float gameLength;
    private float gameTime;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentState = gameState.tutorial;
        scoreText = scoreBoard.GetComponent<Text>();

    }

    // Update is called once per frame
    // Displays different text for the user depending on the current state of the game
    // Additionally checks if the game should move to the endGame state
    void Update()
    {

        if(currentState == gameState.tutorial)
        {
            TutorialText();
        }
        else if (currentState == gameState.game)
        {
            
            ScoreBoardText();
            EndGame();
        }
        else if( currentState == gameState.endGame)
        {
            EndGameText();
        }
          
    }

    /// <summary>
    /// Moves the game to the endGame state if enough time has passed
    /// </summary>
    private void EndGame()
    {

        gameTime += Time.deltaTime;

        if(gameTime >= gameLength)
        {
            currentState = gameState.endGame;
            gameData.ClearTargets();

            Instantiate(gameObject.GetComponent<TargetManager>().targetPrefab).transform.position = new Vector3(0, .5f, 3);
               
        }
    }

    /// <summary>
    /// Displays text with information about how to play the game
    /// </summary>
    private void TutorialText()
    {
        scoreText.fontSize = 30;
        scoreText.text = "Pick up the crossbow by grabbing the handle.\n" +
            "It can be loaded by grabbing the string.\n" +
            "Shoot by pressing the trigger while holding the handle\n" +
            "Start the game by shooting the target in front of you!\n" +
            "Shoot as many targets as you can in one minute!";
    }

    /// <summary>
    /// Displays the user's current score and time remaning
    /// </summary>
    private void ScoreBoardText()
    {
        scoreText.fontSize = 500;
        scoreText.text = "Score: " + gameData.score + "           Time Left: " + (gameLength - gameTime).ToString("0.00");
    }

    /// <summary>
    /// Displays endGame text containing final score and instructions for restarting the game
    /// </summary>
    private void EndGameText()
    {
        scoreText.text = "You shot " + gameData.score + " targets!\n" +
            "To play again shoot the target in front of you!";
    }

    /// <summary>
    /// Sets intial parameters for the game so a new game can be launched correctly after on is finished
    /// Additionally progresses the game to the game state
    /// </summary>
    public void StartGame()
    {
        gameData.score = 0;
        gameTime = 0;

        GetComponent<TargetManager>().spawnInterval = 3f;
        currentState = gameState.game;
    }
}
