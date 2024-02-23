using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIController : MonoBehaviour
{
    GameController gameController;
    LevelController levelController;
    TextMeshProUGUI scoreCounter, livesCounter, centerText, notification;
    PuckMovementController puckMovementController;
    PaddleController paddleController;
    GameObject quitButton, resetButton, nextLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        levelController = GameObject.Find("GameManager").GetComponent<LevelController>();
        paddleController = GameObject.Find("Paddle").GetComponent<PaddleController>();
        puckMovementController = GameObject.Find("Puck").GetComponent<PuckMovementController>();

        scoreCounter = GameObject.Find("ScoreCounter").GetComponent<TextMeshProUGUI>();
        livesCounter = GameObject.Find("LivesCounter").GetComponent<TextMeshProUGUI>();
        notification = GameObject.Find("Notification").GetComponent<TextMeshProUGUI>();

        centerText = GameObject.Find("CenterText").GetComponent<TextMeshProUGUI>();

        quitButton = GameObject.Find("QuitButton");
        resetButton = GameObject.Find("ResetButton");
        nextLevelButton = GameObject.Find("ContinueButton");
    }

    // Update is called once per frame
    void Update()
    {
        livesCounter.text = gameController.GetLives().ToString();
        scoreCounter.text = gameController.GetScore().ToString();
        string notifs = "";
        if (puckMovementController.AtMaxSpeed()) {
            notifs += "Puck at max speed!\n";
        }
        if (paddleController.IsMaxWidth()) {
            notifs += "Paddle reached max size!";
        }
        notification.text = notifs;

        if (gameController.Running()) {
            if (!puckMovementController.moving) {
                centerText.text = "Click to start!";
            }
            quitButton.SetActive(gameController.Paused());
            resetButton.SetActive(gameController.Paused());
            nextLevelButton.SetActive(gameController.Paused());
        } else {
            // Game is in end state, did they win or lose?
            if (levelController.GameComplete()) {
                centerText.text = "You won!\nCongratulations. Final score: " + gameController.GetScore();
                quitButton.SetActive(true);
            } else {
                centerText.text = "Game over!\nFinal score: " + gameController.GetScore();
                resetButton.SetActive(true);
                quitButton.SetActive(true);
            }
        }
        
    }

    public void ClearCenter() {
        centerText.text = "";
    }

    public void Quit() {
        SceneManager.LoadScene("Menu");
    }

    public void ResetLevel() {
        gameController.Reset();
    }

    public void Continue() {
        gameController.Unpause();
    }

}
