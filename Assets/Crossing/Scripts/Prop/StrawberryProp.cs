using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryProp : PropBase
{

    public override void OnProp()
    {
        base.OnProp();
        //   PlayerController.Ins.HP = 0;
        PlayerController.Ins.IsDownsizingTimer =15;
        PlayerController.Ins.IsBulkTimer = 0;
    }
}
