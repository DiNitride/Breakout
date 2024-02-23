using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Parent class that all block types should inherit.
    Implement Trigger() to add block specific functionality
*/
public abstract class BlockBehaviour : MonoBehaviour
{
    protected BasePowerup powerup;
    protected LevelController levelController;
    protected GameController gameController;
    public int x, y;
    
    protected virtual void Start() {
        GameObject gm = GameObject.Find("GameManager");
        levelController = gm.GetComponent<LevelController>();
        gameController = gm.GetComponent<GameController>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("puck")) {
            this.Trigger();
        }
    }

    public abstract void Trigger(); // Called on puck collision. Must call powerup trigger!!!

    protected void TriggerPowerup() {
        if (this.powerup) { this.powerup.Trigger(); }
    }

    public void SetPowerup(BasePowerup bp) {
        this.powerup = bp;
    }
}
