using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {
    public Transform tran;

	private float cloudSpeed = 0.02f;

	// Use this for initialization
	void Start () {
        tran.position = new Vector3(Random.Range(-7.0f,7.0f), Random.Range(3.6f, 6.0f), Random.Range(-1.0f, 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
        tran.position = tran.position + new Vector3(0 , -cloudSpeed, 0);
	}
}
