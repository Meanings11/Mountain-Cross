using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{
    [Header("Gameplay")]
    public GameObject cutPrefab;
    public float cutLifetime;

    private bool dragging;
    private Vector2 swipeStart;

    public HitLine linePf;
    Dictionary<int, HitLine> lineInsts;
    Camera mainCamera;

    private void Start()
    {
        lineInsts = new Dictionary<int, HitLine>(10);
        mainCamera = Camera.main;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            swipeStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && dragging)
        {
            SpawnCut();
        }
        if (Input.GetMouseButton(0))
        {
            Hit(Input.mousePosition);
        }
#endif
        if (Input.touchCount > 0)
        {
            for (int i = 0, length = Input.touchCount; i < length; i++)
            {
                int index = i;
                int fingerId = Input.touches[index].fingerId;
                Vector3 point = mainCamera.ScreenToWorldPoint(Input.touches[index].position);
                point.Set(point.x, point.y, -5);
                switch (Input.touches[index].phase)
                {
                    case TouchPhase.Began:
                        if (!lineInsts.ContainsKey(fingerId))
                        {
                            lineInsts[fingerId] = Instantiate(linePf);
                            lineInsts[fingerId].Init();
                        }

                        lineInsts[fingerId].AddPos(point);
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if (lineInsts.TryGetValue(fingerId, out HitLine hl))
                            hl.AddPos(point);
                        break;
                    default:
                        if (lineInsts.ContainsKey(fingerId))
                        {
                            Destroy(lineInsts[fingerId].gameObject);
                            lineInsts.Remove(fingerId);
                        }
                        break;
                }

                Hit(Input.touches[index].position);
            }
        }
    }

    void Hit(Vector2 screenPos)
    {
        RaycastHit2D c = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(screenPos), Vector2.zero);
        if (c.collider != null && c.collider.gameObject.TryGetComponent(out CuttableObject v))
            v.OnHit();
    }

    private void SpawnCut()
    {
        Vector2 swipeEnd = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        GameObject cutInstance = Instantiate(cutPrefab, swipeStart, Quaternion.identity);
        cutInstance.GetComponent<LineRenderer>().SetPosition(0, swipeStart);
        cutInstance.GetComponent<LineRenderer>().SetPosition(1, swipeEnd);

        //Vector2[] colliderPoints = new Vector2[2];
        //colliderPoints[0] = Vector2.zero;
        //colliderPoints[1] = swipeEnd - swipeStart;
        //cutInstance.GetComponent<EdgeCollider2D>().points = colliderPoints;

        Destroy(cutInstance, cutLifetime);
    }
}
