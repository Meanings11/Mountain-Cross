using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokenGenerator : MonoBehaviour
{
	public float queueTime = 1.5f;
	private float time = 0;
	public GameObject mos;
	public GameObject big_mos;

	public float height;



    // Update is called once per frame
    void Update()
    {
        if(time > queueTime)
        {
            GameObject go;
            if (Random.Range(0,1f) > 0.8) {
        	    go = Instantiate(big_mos);
            } else {
        	    go = Instantiate(mos);
            }

        	go.transform.position = transform.position + new Vector3(0, Random.Range(-height, height),0);

        	time = 0;

        	Destroy(go, 10);
        }

        time += Time.deltaTime;
    }
}
