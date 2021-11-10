using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLine : MonoBehaviour
{
    public int linePosCount = 20;
    public LineRenderer lineRenderer;

    List<Vector3> linePos;

    public void Init()
    {
        linePos = new List<Vector3>(linePosCount);
    }

    public void AddPos(Vector3 pos)
    {
        if (linePos.Count == linePosCount)
            linePos.RemoveAt(0);
        else
            lineRenderer.positionCount = linePos.Count + 1;

        linePos.Add(pos);
        for (int i = 0, length = linePos.Count; i < length; i++)
            lineRenderer.SetPosition(i, linePos[i]);
    }
}
