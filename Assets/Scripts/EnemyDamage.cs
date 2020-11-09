using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private PlayerController player;
    // add audiocomponent and audioclip array
    private AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        // tell the computer which audiosource to use
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) ;
        player.Damage(1);
        // play sound if player collides 
       
            Debug.Log("sound should play");

            

            source.PlayOneShot(clip);

        
    }
    
}
