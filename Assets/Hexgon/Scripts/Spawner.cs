using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Spawner : MonoBehaviour
{
    public GameObject hexagonPrefab;
    public GameObject bonePrefab;
    private Text point;
    public Text pointText;

    private int playerPoint = 0;
    private float spawnSpeed = 1.5f;
    private float nextTimeToSpawn = 0f;
    private float nextBoneTime = 0f;
    private float startTime = 0f;
    private float radius = 1f;

    private void Start() {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTimeToSpawn)
        {
            if (Time.time >= startTime + 4.55) {
                playerPoint += 100;
                pointText.text = "$" + string.Format("{0:0,0}", Int16.Parse(playerPoint.ToString()));
            }
            Instantiate(hexagonPrefab);
            nextTimeToSpawn = Time.time + spawnSpeed;
        }

        if (Time.time >= nextBoneTime) {
            if (!bonePrefab.gameObject.activeSelf) {
                // change bone to new position
                float angle = UnityEngine.Random.Range(0, Mathf.PI*2);
                Vector2 pos2d = new Vector2(Mathf.Sin(angle)*radius,Mathf.Cos(angle)*radius);
                bonePrefab.gameObject.transform.position = new Vector3(pos2d.x,pos2d.y,0);

                // show new bone
                bonePrefab.gameObject.SetActive(true);
            }
            nextBoneTime = Time.time + 5f;
        }
    }
}
