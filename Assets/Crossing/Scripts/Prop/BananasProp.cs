using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//香蕉
public class BananasProp : PropBase
{

    public override void OnProp()
    {
        base.OnProp();
        PlayerController.HP += 1;

    }
}
