using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float bpm = 95.0f; //all the musical clips need to be the same BPM and Tempo (to make it easy)
    public int numBeatsPerSegment = 4;
    //for this demo I am layering three layers of music each with two variations
    //so i am setting up arrays to hold the variations for each layer
    public AudioClip[] chord = new AudioClip[2];
    public AudioClip[] arp = new AudioClip[4];
    public AudioClip[] pad = new AudioClip[4];

    //the next event applyse to all the tracks (though you could change this for more complex compositions
    //think of this as a down beat (count 1) that will repeatedly sync the first beat of all the
    //tracks that reference it
    //the flip is primarily used to switch between tracks ie switching to the preloaded
    private double nextEventTime;
    private int flip1 = 0;
    private int flip2 = 0;
    private int flip3 = 0;

    //note we need to create a AudioSource for each track - 2 trackes per sound to acomidat sync
    private AudioSource[] audioSources = new AudioSource[6];
    //private bool running = false;
    protected bool layer1 = true;
    protected bool layer2 = false;
    protected bool layer3 = false;


    void Start()
    {
        //this loop is based on the number of tracks
        for (int i = 0; i < 6; i++)
        {
            GameObject child = new GameObject("Player");
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
        }
        nextEventTime = AudioSettings.dspTime + 2.0f;
        //running = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Layer1"))
        {
            layer1 = true;
            layer2 = false;
            layer3 = false;
            Debug.Log("COLLISION");
        }
        if (other.CompareTag("Layer2"))
        {
            layer1 = true;
            layer2 = false;
            layer3 = true;
            Debug.Log("COLLISION");
        }

        if (other.CompareTag("Layer3"))
        {
            layer1 = true;
            layer2 = true;
            layer3 = true;
            Debug.Log("COLLISION");
        }
    }


    void Update()
    {
        double time = AudioSettings.dspTime;

        //now we will check if the other tracks should be playing



        // note that by saying 
        //if time + 1 is > start time 
        //we are actually saying 
        //if next scheduled start time is less than 1 second from current time then get ready with the next clip

        if (time + 1.0f > nextEventTime)
        {
            Debug.Log("Layer2 is: " + layer2);
            if (layer1 == true)
            {
                //this process will happen when ever the Player Capusule is alive
                audioSources[flip1].clip = chord[Random.Range(0, 2)]; //note that because i have used ints range returns ints
                audioSources[flip1].PlayScheduled(nextEventTime);
                // Flip between two audio sources so that the loading process 
                //of one does not interfere with the one that's playing out
                // note that I am placing it with
                flip1 = 1 - flip1;

            }
            if (layer2 == true)
            {
                //this process will happen when ever the Player Capusule is alive
                audioSources[flip2 + 2].clip = arp[Random.Range(0, 4)]; //note that because i have used ints range returns ints
                audioSources[flip2 + 2].PlayScheduled(nextEventTime);
                // Flip between two audio sources so that the loading process 
                //of one does not interfere with the one that's playing out
                // note that I am placing it with
                flip2 = 1 - flip2;
                Debug.Log("2 should be playing");
            }
            if (layer3 == true)
            {
                //this process will happen when ever the Player Capusule is alive
                audioSources[flip3 + 4].clip = pad[Random.Range(0, 4)]; //note that because i have used ints range returns ints
                audioSources[flip3 + 4].PlayScheduled(nextEventTime);
                // Flip between two audio sources so that the loading process 
                //of one does not interfere with the one that's playing out
                // note that I am placing it with
                flip3 = 1 - flip3;
                Debug.Log("3 should be playing");
            }

            // Place the next event 16 beats from here at a rate of 140 beats per minute
            nextEventTime += 60.0f / bpm * numBeatsPerSegment;

        }





    }


}
