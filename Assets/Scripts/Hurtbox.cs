using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public GameObject loot;
    public GameObject deatheffect;
    private AudioSource source;
    public AudioClip frogdeathSound;
    public Vector3 thePosition;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pickup")
        {
            if(other.GetComponent<Pickup>().type == Pickup.PickupType.Barrel)
            {
                Destroy(other.gameObject);
                CreateParticles();
                DropBanana();
                source.PlayOneShot(frogdeathSound);
            }
            Destroy(gameObject);
        }
    }

    void CreateParticles()
    {
        thePosition = transform.TransformPoint(Vector3.up * 1 / 2);
        GameObject spawnedParticles = Instantiate(deatheffect, thePosition, Quaternion.identity);

    }
    void DropBanana()
    {
        thePosition = transform.TransformPoint(Vector3.up * 1/2);
        GameObject drop = Instantiate(loot, thePosition, Quaternion.identity);

    }
}
