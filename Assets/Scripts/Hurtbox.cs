using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{

    public GameObject deatheffect;

    // Start is called before the first frame update
    void Start()
    {
        
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
            }
            Destroy(gameObject);
        }
    }

    void CreateParticles()
    {
        GameObject spawnedParticles = Instantiate(deatheffect, transform.position, Quaternion.identity);
       
    }
}
