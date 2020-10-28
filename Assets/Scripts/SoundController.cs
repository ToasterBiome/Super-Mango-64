using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Array to hold Footstep Assets
    [SerializeField]
    private AudioClip[] stepClips;
    // create an Audiosource so we can access the one on this gameobject
    private AudioSource stepAudioSource;
    [SerializeField]
    private AudioSource jumpAudioSource;
    // Awake is called when the script instance is being loaded

    [SerializeField]
    private AudioClip jumpClip;

    private void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
    }
    private IEnumerator Step()
    {
        //add an audio component attatched to this device
        stepAudioSource = gameObject.AddComponent<AudioSource>(); 
        // call the method that gets a random clip from array
        AudioClip clip = GetRandomClip(stepClips);
       
        // play the clip chosen by GetRandomClip
        stepAudioSource.PlayOneShot(clip);   
        // wait for the length of the clip + 0.2f before destroying audiosource
        yield return new WaitForSeconds(clip.length + 0.2f);
        Destroy(stepAudioSource);
       
        
    

    }
        
    public void Jump()
    {
        
        // play the clip chosen by GetRandomClip
        jumpAudioSource.PlayOneShot(jumpClip);
        Debug.Log("23play clip");

        // may not need to destroy this audiosource but keep eye on it
    }
     
   
    private AudioClip GetRandomClip(AudioClip [] array)
    {
        // return a random AudioClip from the array that is passed as an argument, chosen between index 0 and array.Length (the length of the array)
        return array[UnityEngine.Random.Range(0, array.Length)];
    }


   
}
