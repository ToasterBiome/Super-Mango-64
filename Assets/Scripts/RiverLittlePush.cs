using UnityEngine;

public class RiverLittlePush : MonoBehaviour
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
            if (waterCount == 1)
            {
                RaycastHit hit;
                if (Physics.Raycast(player.transform.position, -player.transform.up, out hit))
                {
                    player.splash.transform.position = hit.point;
                }


                
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