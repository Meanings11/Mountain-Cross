using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//道具父类
public class PropBase : MonoBehaviour
{


    public virtual void OnProp()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            SoundManager.instance.CollectionAudioPlay();
            OnProp();
            Destroy(this.gameObject);
        }
    }
}
