using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    // Array to hold Footstep Assets
    [SerializeField]
    private AudioClip[] stepClips;
    // create an Audiosource so we can access the one on this gameobject
    private AudioSource audioSource;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // get the audio component attatched to this device
        audioSource = GetComponent<AudioSource>();
    }

  
    private void Step() 
    {
        // call the method that gets a random clip from array
        AudioClip clip = GetRandomClip();
        // play the clip chosen by GetRandomClip
        audioSource.PlayOneShot(clip);
    }
    private AudioClip GetRandomClip()
    {
        // return a random AudioClip from the stepClips array, chosen between index 0 and stepClips.Length (the length of the array)
        return stepClips[UnityEngine.Random.Range(0, stepClips.Length)];
    }
}
