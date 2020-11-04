using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerBanana : MonoBehaviour
{
    private AudioSource source;
    public Collider capsule;
    public AudioClip banClip;
    public AudioClip gloveClip;
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {



            if (other.gameObject.CompareTag("Banana"))
            {
                if (source.isPlaying != true)
                {
                    Debug.Log("banana trigger");
                    source.PlayOneShot(banClip);
                }
            }
            if (other.gameObject.CompareTag("GlovesPickup"))
            {
                if (source.isPlaying != true)
                {
                    Debug.Log("glove trigger");
                    source.PlayOneShot(gloveClip);
                }
            }
        
    }
}
