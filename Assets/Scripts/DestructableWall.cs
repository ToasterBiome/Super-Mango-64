using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
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
        if (other.tag == "Pickup")
        {
            Pickup.PickupType type = other.GetComponent<Pickup>().type;

            if(type == Pickup.PickupType.Boulder)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            
        }
    }
}
