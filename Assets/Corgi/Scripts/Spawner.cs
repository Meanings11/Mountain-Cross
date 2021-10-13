using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Spawner : MonoBehaviour
{
    public GameObject corgiPrefab;
    public GameObject bonePrefab;
    private Text point;
    public Text pointText;
    public Text timer;

    public int playerPoint = 0;
    private float spawnSpeed = 1.5f;
    private float nextTimeToSpawn = 0f;
    private float nextBoneTime = 0f;
    private float timeRemaining = 30f;
    private float radius = 1f;

    private void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            timer.text = "00:00:" + Mathf.RoundToInt(timeRemaining).ToString("d2");

            if (Time.time >= nextBoneTime) {
                if (Time.time >= 2.55) {
                    playerPoint += 10;
                    pointText.text = "$" + string.Format("{0:0,0}", Int32.Parse(playerPoint.ToString()));
                }
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
        } else {
            timer.text = "00:00:00";
        }

        if (Time.time >= nextTimeToSpawn) {
            Instantiate(corgiPrefab);
            nextTimeToSpawn = Time.time + spawnSpeed;
        }
    }
}
