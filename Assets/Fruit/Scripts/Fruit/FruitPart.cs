using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPart : MonoBehaviour
{
    public SpriteRenderer part1;
    public SpriteRenderer part2;
    public Rigidbody2D[] rigs;

    public void Init(FruitPartData data)
    {
        //this.part1.sprite = data.part1;
        //this.part2.sprite = data.part2;
        for (int i = 0, length = rigs.Length; i < length; i++)
            rigs[i].AddForce(Random.insideUnitCircle * 100);

        Destroy(gameObject, 3);
    }
}
