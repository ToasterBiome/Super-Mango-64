using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerBanana : MonoBehaviour
{
    private AudioSource source;
    public AudioClip clip;
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
                source.PlayOneShot(clip);
            
        }

    }
}
