using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoleGenerator : MonoBehaviour
{
    public GameObject mole;
    public GameObject[] emptyMoleList;
    public Transform[] spawnPoints;

    public AudioSource moleAudio;
    private HammerController hammerCtr;

    private float nextTimeToSpawn = 0f;
    private float spawnSpeedRange = 2.5f;

    public int hiddenIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        moleAudio = GetComponent<AudioSource>();
        hammerCtr = GameObject.Find("Hammer").GetComponent<HammerController>();

        mole = Instantiate(mole);

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (hammerCtr.timeRemaining > 0) {
            if (Time.time >= nextTimeToSpawn) {
                mole.gameObject.SetActive(false);
                emptyMoleList[hiddenIndex].gameObject.SetActive(true);
                // Destroy(mole);
                Spawn();
                nextTimeToSpawn = Time.time + UnityEngine.Random.Range(0.2f, spawnSpeedRange);
            }
        }
    }

    void Spawn() {
        mole.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;

        for (int i = 0; i < emptyMoleList.Length; i++) {
    	    if (emptyMoleList[i].transform.position == mole.transform.position) {
                emptyMoleList[i].gameObject.SetActive(false);
                hiddenIndex = i;
            }
        }

        mole.gameObject.SetActive(true);

        moleAudio.Play();
    }
}
