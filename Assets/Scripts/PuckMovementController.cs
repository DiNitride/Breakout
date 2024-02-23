using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckMovementController : MonoBehaviour
{   
    private GameController gameController;
    private GameUIController uiController;
    public bool moving = false;
    public float maxSpeed;
    public float speed;
    public float defaultSpeed;

    private Transform paddleTransform;
    private Rigidbody2D rb2d;
    private Vector3 tempVelocity;
    
    static Vector2 START_LOCATION = new Vector2(0, -4.3f);

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        uiController = GameObject.Find("GameManager").GetComponent<GameUIController>();
        paddleTransform = GameObject.Find("Paddle").GetComponent<Transform>();
        Reset();
    }

    // Physics calc here
    void FixedUpdate() {
        // Enforce velocity components to move exactly 45 degrees
        if (gameController.Running() && !gameController.Paused() && moving) {
            float x, y;
            if (rb2d.velocity.x > 0) {
                x = speed;
            } else {
                x = -speed;
            }
            if (rb2d.velocity.y > 0) {
                y = speed;
            } else {
                y = -speed;
            }
            rb2d.velocity = new Vector2(x, y);
        }
    }

    public void Freeze() {
        tempVelocity = rb2d.velocity;
        rb2d.velocity = Vector3.zero;
    }

    public void Unfreeze() {
        if (tempVelocity != Vector3.zero) {
            rb2d.velocity = tempVelocity;
            tempVelocity = Vector3.zero;
        }   
    }

    // Update is called once per frame
    void Update()
    {   
        if (gameController.Paused()) { return; }
        if (!moving && Input.GetMouseButtonDown(0)) Launch(-1);
        if (!moving && Input.GetMouseButtonDown(1)) Launch(1);
        if (!moving) {
            transform.position = new Vector2(paddleTransform.position.x, transform.position.y);
        } 
    }

    public void Launch(int dir) {
        if (gameController.Running() && !gameController.Paused()) { // Ensure game is not in win or fail state or paused
            uiController.ClearCenter();
            moving = true;
            float xV = speed * dir;
            rb2d.velocity = new Vector2(xV, speed);
        }
    }

    public void Reset() {
        moving = false;
        rb2d.velocity = Vector2.zero;
        transform.position = START_LOCATION;
    }

    public void IncreaseSpeed() {
        if (speed < maxSpeed) {
            speed += 0.5f;
        }
    }

    public bool AtMaxSpeed() {
        return speed == maxSpeed;
    }

    public void ResetSpeed() {
        speed = defaultSpeed;
    }
}
