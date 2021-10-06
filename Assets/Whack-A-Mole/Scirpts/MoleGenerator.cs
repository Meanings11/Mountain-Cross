using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleGenerator : MonoBehaviour
{

    public GameObject moleObject;
    public GameObject mole;
    public Transform[] spawnPoints;
    AudioSource hitAudio;
    
    public AudioClip moleAudio;

    private int moleId;
    private bool expire;

    private HammerController hc;

    // Start is called before the first frame update
    void Start()
    {
        hc = GetComponent<HammerController>();

        expire = true;
        moleId = 0;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (hc.timeRemaining < 0) {
            StartCoroutine(WaitThenDie());
        }
    }

     IEnumerator WaitThenDie() {
         if (expire == true) {
            expire = false;
            int currentId = moleId;
            yield return new WaitForSeconds(2);
            if (currentId == moleId) {
                DestroyImmediate(mole, true);
                // moleObject.gameObject.SetActive(false);
                Spawn();
            }
            expire = true;
         }
    }

    public void Spawn() {
        moleId += 1;
        // mole = Instantiate(moleObject) as GameObject;
        mole = Instantiate(moleObject);
        hitAudio.PlayOneShot(moleAudio);
        mole.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;       
    }
}
