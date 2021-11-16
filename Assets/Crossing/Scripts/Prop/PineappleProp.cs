using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleProp : PropBase
{

    public override void OnProp()
    {
        base.OnProp();
        PlayerController.Ins.IsDownsizingTimer =0;
        PlayerController.Ins.IsBulkTimer =15;
    }
}
