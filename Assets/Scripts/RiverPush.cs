using UnityEngine;

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
                player.inWater = false;
                player.waterVelocity = Vector3.zero;
            }
            playerInside = false;
            player = null;
        }
    }
}