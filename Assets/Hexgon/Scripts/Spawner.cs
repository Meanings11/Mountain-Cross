using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject hexagonPrefab;
    public Text pointText;

    private int point = 0;
    private float spawnSpeed = 1.5f;
    private float nextTimeToSpawn = 0f;
    private float startTime = 0f;

    private void Start() {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTimeToSpawn)
        {
            if (Time.time >= startTime + 4.55) {
                point++;
                pointText.text = point.ToString();
            }
            Instantiate(hexagonPrefab);
            nextTimeToSpawn = Time.time + spawnSpeed;
        }
    }
}
