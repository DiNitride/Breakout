using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckMovementController : MonoBehaviour
{   
    public bool dev = false; // set true to enable dev puck control
    public bool hDir = true;
    public bool vDir = true;
    public bool moving = false;
    public float speed = 2;
    private Rigidbody2D rb2d;
    private float[] directions = { 45f, 135f, 225f, 315f };

    static Vector2 START_LOCATION = new Vector2(0, -4);

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!moving && Input.GetMouseButtonDown(0)) Launch();

        // Old movement system before switching to physics engine
        // if (moving) {
        //     transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
            
        //     float dir = 0f;
        //     if (vDir && hDir) {
        //         dir = directions[0];
        //     } else if (vDir && !hDir) {
        //         dir = directions[1];
        //     } else if (!vDir && hDir) {
        //         dir = directions[3];
        //     } else if (!vDir && !vDir) {
        //         dir = directions[2];
        //     }
        //     transform.eulerAngles = new Vector3(0, 0, dir);
        // }
        
        // if (dev) {
        //     if (Input.GetKeyDown("[")) FlipHorizontal();
        //     if (Input.GetKeyDown("]")) FlipVertical();
        // }
        
    }

    public void FlipHorizontal() {
        hDir = !hDir;
    }

    public void FlipVertical() {
        vDir = !vDir;
    }

    public void Launch() {
        moving = true;
        rb2d.velocity = new Vector2(speed, speed);
    }

    public void Reset() {
        moving = false;
        rb2d.velocity = Vector2.zero;
        transform.position = START_LOCATION;
    }
}
