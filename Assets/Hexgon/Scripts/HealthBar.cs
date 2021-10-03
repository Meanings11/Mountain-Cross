using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int currentHealth = 2;
    public int maxHealth = 5;

    public Image[] healthImgs;
    public Sprite bone;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < healthImgs.Length; ++i) {
            if (i < currentHealth) {
                healthImgs[i].enabled = true;
            } else {
                healthImgs[i].enabled = false;
            }
        }  
    }
}
