using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePowerup : BasePowerup
{
    public GameObject explosion;

    public override void Trigger() {
        GameObject e = Instantiate(explosion, transform.position, new Quaternion());
        Collider2D[] neighbours = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.5f, 1f), 0f);
        foreach (var collider in neighbours) {
            if (collider.CompareTag("block")) {
                if (collider.GetComponentInParent<Transform>() == transform.parent) {
                    continue;
                }
                collider.gameObject.GetComponent<BlockBehaviour>().Trigger();
            }
        }
    }
}
