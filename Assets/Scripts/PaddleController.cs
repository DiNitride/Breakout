using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{      
    public bool mouseControl = true;
    public float movementSpeed = 10f;
    public float defaultPaddleSize;
    public float maxPaddleSize;
    public float paddleSize;

    private float transformSpeed = 1f;
    private Vector3 transformVelocity; 
    private PuckMovementController pmc;
    private GameController gameController;

    private static float HORIZONTAL_BOUND = 9f; // How many units left and right from the centre the gameplay area is

    void Start() {
        pmc = GameObject.Find("Puck").GetComponent<PuckMovementController>();
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        paddleSize = defaultPaddleSize;
    }

    // Update is called once per frame
    void Update()
    {   if (gameController.Running() && !gameController.Paused()) {
            if (mouseControl) {
                Vector2 mousePos = Input.mousePosition;
                Vector2 p = Camera.main.ScreenToWorldPoint(mousePos);
                if (p.x > (-HORIZONTAL_BOUND + (paddleSize / 2)) && p.x < (HORIZONTAL_BOUND - paddleSize / 2)) {
                    transform.position = new Vector3(p.x, transform.position.y, transform.position.z);
                }
            } else {
                float force = Input.GetAxis("Horizontal");
                transform.position = new Vector3(transform.position.x + (force * movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }

            // Grow or shrink paddle...
            transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(paddleSize, transform.localScale.y, transform.localScale.z), ref transformVelocity, transformSpeed);
        }
    }

    public void ResetPosition() {
        transform.position = new Vector3(0, -4.75f, 0);
    }

    public void IncreaseWidth() {
        if (paddleSize < maxPaddleSize) {
            paddleSize += 0.25f;
        }
    }

    public void ResetWidth() {
        paddleSize = 1.5f;
    }

    public bool IsPaddleGrown() {
        return paddleSize != defaultPaddleSize;
    }

    public bool IsMaxWidth() {
        return paddleSize == maxPaddleSize;
    }
}
