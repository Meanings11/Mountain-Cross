using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {
    public Transform tran;

	// Use this for initialization
	void Start () {
        tran.position = new Vector3(Random.Range(-5.0f,5.0f), Random.Range(3.6f, 6.0f), 0);
	}
	
	// Update is called once per frame
	void Update () {
        tran.position = tran.position + new Vector3(0 , -0.03f, 0);
	}
}
