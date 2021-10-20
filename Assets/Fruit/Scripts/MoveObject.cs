using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    [Header("Speed variables")]
    public float minimumXSpeed, maximumXSpeed, minimumYSpeed, maximumYSpeed;
    
    [Header("Gameplay")]
    public float lifetime;
    
    void Start() {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
            Random.Range(minimumXSpeed, maximumXSpeed),
            Random.Range(minimumYSpeed, maximumYSpeed)
        );
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        
    }
}
