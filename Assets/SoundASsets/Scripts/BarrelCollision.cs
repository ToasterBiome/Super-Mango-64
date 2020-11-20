using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCollision : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;
    public AudioClip Wallclip;

    void Start()
    {
        gameObject.GetComponent<AudioSource>();
        source = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player") != true)
        {
           
            if (source.isPlaying != true)
            {
                if (collision.gameObject.CompareTag("Wall"))
                {
                   

                    source.PlayOneShot(Wallclip);
                }
                else
                {
                    AudioClip clip = GetRandomClip(clips);

                    source.PlayOneShot(clip);
                }
                }
        }
       



    }
    private AudioClip GetRandomClip(AudioClip[] array)
    {
        // return a random AudioClip from the array that is passed as an argument, chosen between index 0 and array.Length (the length of the array)
        return array[Random.Range(0, array.Length)];
    }
}
