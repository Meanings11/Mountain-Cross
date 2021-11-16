using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人发射的子弹
public class Bullect : MonoBehaviour
{
    public Vector3 OnMoveV3;

     
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(OnMoveV3*Time.deltaTime*3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            collision.GetComponent<PlayerController>().OnHit(transform);
            Destroy(this.gameObject);
        }
    }

}
