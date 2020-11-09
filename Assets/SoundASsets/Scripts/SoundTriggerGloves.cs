using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerGloves : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource source;
    public AudioClip clip;
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

        if (other.gameObject.CompareTag("Gloves"))
        {
            Debug.Log("glove trigger");
            source.PlayOneShot(clip);

        }

    }
}
