using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//collision
public class Collection : MonoBehaviour
{
    public void Death()
    {
        // Debug.Log("Collection: " + name);
        if (name.Contains("Cherry"))
        {
            FindObjectOfType<PlayerController>().CherryPlus();
        }
        else if (name.Contains("Gem"))
        {
            FindObjectOfType<PlayerController>().CherryPlus();
        }

        Destroy(gameObject);
    }
}
