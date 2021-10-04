using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public bool clockwise = true;
    private float rotate = 1f;
    void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;

        if (clockwise)
        {
            rotate = rotate * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 30f * rotate);
    }
}
