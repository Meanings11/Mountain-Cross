using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpriteData : MonoBehaviour
{
    public static FruitSpriteData Instance;

    public FruitPartData[] fruitPartDatas;
    public FruitPart fruitPartPf;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    public void CreatePartFruit(int fruitIndex, Transform trans)
    {
        FruitPart obj = Instantiate(fruitPartPf);

        obj.Init(fruitPartDatas[fruitIndex]);
        obj.transform.position = trans.position;
        obj.transform.localEulerAngles = trans.localEulerAngles;
    }
}

[System.Serializable]
public struct FruitPartData
{
    public Sprite part1;
    public Sprite part2;
}