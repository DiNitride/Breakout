using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckCollisionController : MonoBehaviour
{   

    private PaddleController paddleController;
    private PuckMovementController puckMovementController;
    private Transform blockTransform;

    // Start is called before the first frame update
    void Start()
    {   
        puckMovementController = gameObject.GetComponent<PuckMovementController>();
        paddleController = GameObject.Find("Paddle").GetComponent<PaddleController>();
        blockTransform = GameObject.Find("test-block").GetComponent<Transform>();
    }

    // private void Update() {
    //     float angle = Vector3.Angle(blockTransform.position, transform.position);
    //     Debug.Log(angle);
    // }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("death")) {
            Reset();
        } else if (other.gameObject.CompareTag("verticalCollider")) {
            puckMovementController.FlipHorizontal();
        } else if (other.gameObject.CompareTag("horizontalCollider")) {
            puckMovementController.FlipVertical();
        } else if (other.gameObject.CompareTag("block")) {
            // TODO: Calculate what side
            float dist = Vector3.Distance(other.gameObject.transform.position, transform.position);
            Debug.Log(dist);
            if (dist > 0.5) {
                puckMovementController.FlipVertical();
            } else {
                puckMovementController.FlipHorizontal();
            }
        }
    }

    void Reset() {
        puckMovementController.Reset();
        paddleController.Reset();
    }
}
