using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{   
    private bool running;
    private bool paused;

    private LevelController levelController;
    private GameUIController uiController;
    private PaddleController paddleController;
    private PuckMovementController puckMovementController;

    private int lives = 3;
    private int score;

    private Vector3 tempPuckVel;

    public int GetLives() { return lives; }
    public int GetScore() { return score; }

    // Start is called before the first frame update
    void Start()
    {   
        levelController = gameObject.GetComponent<LevelController>();
        uiController = gameObject.GetComponent<GameUIController>();
        paddleController = GameObject.Find("Paddle").GetComponent<PaddleController>();
        puckMovementController = GameObject.Find("Puck").GetComponent<PuckMovementController>();
        running = true;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running && Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
                paused = false;
                puckMovementController.Unfreeze();
            } else{
                if (puckMovementController.moving) {
                    puckMovementController.Freeze();
                }
                paused = true;
            }
        }
        if (levelController.LevelComplete()) {
            puckMovementController.Reset();
            Pause();
            levelController.NextLevel();
        }
        if (levelController.GameComplete()) {
            running = false;
            puckMovementController.Reset();
            paddleController.ResetPosition();
        }
    }

    public void IncrementScore(int scoreInc = 1) {
        score += scoreInc;
    }

    public void LoseLife() {
        if (paddleController.IsPaddleGrown()) {
            paddleController.ResetWidth();
        } else {
            lives--;
        }
        puckMovementController.Reset();
        paused = true;
        
        if (lives == 0) {
            running = false;
        }
    }

    public bool Running() { return running; }
    public bool Paused() { return paused; }

    public void Pause() { paused = true; }
    public void Unpause() { paused = false; }

    public void Reset() {
        if (!running) {
            // if in end state, restart entire game
            levelController.currentLevel = 0;
        }
        levelController.ResetLevel();
        puckMovementController.ResetSpeed();
        puckMovementController.Reset();
        paddleController.ResetWidth();
        paddleController.ResetPosition();
        lives = 3;
        score = 0;
        paused = false;
        running = true;
    }
}
