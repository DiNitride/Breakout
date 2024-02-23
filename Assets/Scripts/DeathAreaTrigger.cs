using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAreaTrigger : MonoBehaviour
{   
    GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("puck")) {
            gameController.LoseLife();
        }
    }
}
