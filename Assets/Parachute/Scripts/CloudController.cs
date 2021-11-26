using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;

    public float freq = 0.5f;


    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("cloudGenerator", 1.0f , freq);
	}
	
    void cloudGenerator()
    {
        GameObject cloudObj = cloud1;
        int type = Random.Range(1, 4);
        // Debug.Log(type);
        switch (type)
        {
            case 1: cloudObj = cloud1; break;
            case 2: cloudObj = cloud2; break;
            case 3: cloudObj = cloud3; break;
        }
        Instantiate(cloudObj);
    }

	// Update is called once per frame
	void Update ()
    {
	}
}
