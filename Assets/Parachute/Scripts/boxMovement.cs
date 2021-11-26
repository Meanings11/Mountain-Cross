using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxMovement : MonoBehaviour {
    private float boxSpeed = 0.1f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // playerMovement.collisionHappend(-tran.position.y);
        // Debug.Log("collision happened");
    }

    // Use this for initialization
    void Start()
    {
        // boxSpeed = Random.Range(0.2f, 0.03f);
        transform.position = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(3.6f, 6.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, -boxSpeed, 0);
    }

}
