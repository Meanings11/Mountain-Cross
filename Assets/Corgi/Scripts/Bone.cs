using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    // private float startTime = 0f;
    // private float nextBoneTime = 5f;

    void Start()
    {
        // startTime = Time.time;
    }

    void Update()
    {
        // // hide bone while timeout
        // if (Time.time >= startTime + 4.55f && Time.time < nextBoneTime) {
        //     gameObject.SetActive(false);
        //     nextBoneTime = Time.time + 5f;
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }

}
