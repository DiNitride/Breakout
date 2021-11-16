using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float movementSpeed = 10f;
    private PuckMovementController pmc;

    void Start() {
        pmc = GameObject.Find("Puck").GetComponent<PuckMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pmc.moving) {
            float force = Input.GetAxis("Horizontal");
            transform.position = new Vector3(transform.position.x + (force * movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }

    public void Reset() {
        transform.position = new Vector3(0, -5, 0);
    }
}
