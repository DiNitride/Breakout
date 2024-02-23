using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerup : MonoBehaviour
{   
    protected PaddleController paddleController;
    protected PuckMovementController puckMovementController;

    protected virtual void Start() {
        puckMovementController = GameObject.Find("Puck").GetComponent<PuckMovementController>();
        paddleController = GameObject.Find("Paddle").GetComponent<PaddleController>();
    }

    public abstract void Trigger();
}
