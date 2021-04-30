﻿using UnityEngine;

public class RiverPush : MonoBehaviour
{
    public float PushForce = 2f;

    bool playerInside;
    BetterPlayerController player;
    public static int waterCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInside = true;
            player = other.gameObject.GetComponent<BetterPlayerController>();
            waterCount++;
            if(waterCount == 1)
            {
                //this will play once when he enters the water
            }
            player.inWater = true;
            player.waterVelocity = -Vector3.right * PushForce;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            waterCount--;
            if (waterCount <= 0)
            {
                //you can also play a sound here when he "exits" all water
                player.inWater = false;
                player.waterVelocity = Vector3.zero;
            }
            playerInside = false;
            player = null;
        }
    }
}