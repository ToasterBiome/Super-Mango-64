using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDeathSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip frogdeathSound;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
