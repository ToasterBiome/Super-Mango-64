﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    public BetterPlayerController controller;

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
            if(controller.pickupRB == null)
            {
                controller.SetPickupObject(other.gameObject);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Pickup")
        {
            if(controller.targetPickup == other.gameObject)
            {
                controller.SetPickupObject(null);
            }
        }
    }
}
