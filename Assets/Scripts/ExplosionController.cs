using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx = gameObject.GetComponent<AudioSource>();
        Destroy(gameObject, 1.5f);
    }

}
