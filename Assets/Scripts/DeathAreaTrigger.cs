using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAreaTrigger : MonoBehaviour
{   

    PuckMovementController puckMovementController;

    // Start is called before the first frame update
    void Start()
    {
        puckMovementController = GameObject.Find("Puck").GetComponent<PuckMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("puck")) {
            puckMovementController.Reset();
        }
    }
}
