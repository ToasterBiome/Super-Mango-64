using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;

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

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("KASUDKASHD");
            if (source.isPlaying != true)
            {

                AudioClip clip = GetRandomClip(clips);

                source.PlayOneShot(clip);
            }
        }

       

    }
    private AudioClip GetRandomClip(AudioClip[] array)
    {
        // return a random AudioClip from the array that is passed as an argument, chosen between index 0 and array.Length (the length of the array)
        return array[Random.Range(0, array.Length)];
    }
}
