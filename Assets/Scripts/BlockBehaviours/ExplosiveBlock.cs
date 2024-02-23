using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBlock : BlockBehaviour
{   
    public GameObject explosion;

    public override void Trigger() {
        GameObject e = Instantiate(explosion, transform.position, new Quaternion());
        levelController.DestroyBlock(x, y); // Destory
        TriggerPowerup();
        Collider2D[] neighbours = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.5f, 1f), 0f);
        foreach (var collider in neighbours) {
            if (collider.CompareTag("block")) {
                collider.gameObject.GetComponent<BlockBehaviour>().Trigger();
            }
        }
    }
}
