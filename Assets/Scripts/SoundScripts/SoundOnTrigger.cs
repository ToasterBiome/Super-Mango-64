using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnTrigger : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        source.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("KASUDKASHD");

            AudioClip clip = GetRandomClip(clips);

            source.PlayOneShot(clip);

        }



    }
    private AudioClip GetRandomClip(AudioClip[] array)
    {
        // return a random AudioClip from the array that is passed as an argument, chosen between index 0 and array.Length (the length of the array)
        return array[Random.Range(0, array.Length)];
    }
}
