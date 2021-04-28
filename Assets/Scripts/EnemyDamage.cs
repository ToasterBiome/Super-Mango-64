using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // add audiocomponent and audioclip array
    private AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        // tell the computer which audiosource to use
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<BetterPlayerController>().Damage(1,false);

            /*
            Vector3 force = other.transform.position - transform.position;
            Vector3 clampedForce = new Vector3(force.x, 0.1f, force.y);
            other.GetComponent<Rigidbody>().AddForce(clampedForce * 32f,ForceMode.Impulse);
            */
            Debug.Log("sound should play");
            source.PlayOneShot(clip);
        }
    }

}
