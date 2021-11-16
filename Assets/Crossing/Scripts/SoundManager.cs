using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpClip, collectionClip, hurtClip;
    public AudioSource bgmAudioSource;

    public static SoundManager instance;

    void Awake()
    {
        instance = this;
    }

    public void JumpAudioPlay()
    {
        audioSource.clip = jumpClip;
        audioSource.Play();
    }

    public void CollectionAudioPlay()
    {
        audioSource.clip = collectionClip;
        audioSource.Play();
    }

    public void HurtAudioPlay()
    {
        audioSource.clip = hurtClip;
        audioSource.Play();
    }

    public void BgmControl(bool enable)
    {
        bgmAudioSource.enabled = enable;
    }

}
