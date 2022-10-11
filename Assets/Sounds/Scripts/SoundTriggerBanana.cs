using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerBanana : MonoBehaviour
{
    private AudioSource source;
    public AudioClip Bananaclip;
    public AudioClip Waterclip;
    void Start()
    {
       
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
            
                Debug.Log("banana trigger");
                source.PlayOneShot(Bananaclip);
            
        }

    }
    private void OnCollisionEnter(Collision other)
    {
         if (other.gameObject.CompareTag("Water"))
        {

            Debug.Log("water trigger");
            source.PlayOneShot(Waterclip);

        }
    }
}
