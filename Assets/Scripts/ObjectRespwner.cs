using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespwner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            var pickup = other.gameObject.GetComponent<RespawnableObject>();

            pickup.ReturnToOriginalPosition();
        }
    }

}
