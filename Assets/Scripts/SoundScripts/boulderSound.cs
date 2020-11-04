using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;

    private float distance;
    private float prox = 2;
    public GameObject player;

   


    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        source.enabled = false;


    


    }
    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
      
       // Debug.Log("distance - 2 =" + (distance - prox));
        if (distance < prox)
        {
            source.enabled = true;
           // Debug.Log("enable" + distance);
        }
        if (distance > prox)
        {
            wait();
            
          //  Debug.Log("enable" + distance);
        }

    }
    public IEnumerator wait()
    {
        Debug.Log("enter");
        yield return new WaitForSeconds(3);
        Debug.Log("done ");
        source.enabled = false;
    }
    // Update is called once per frame


    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag != "Player")
        {
            
            
              //  Debug.Log("KASUDKASHD");

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
