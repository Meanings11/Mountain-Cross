using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//兔子
public class Rubbit : Enemy
{
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        StartCoroutine(MoveMent());
    }


}
