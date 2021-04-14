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
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            waterCount--;
            if(waterCount <= 0)
            {
                player.inWater = false;
            }
            playerInside = false;
            player = null;
        }
        
    }

    private void FixedUpdate()
    {
        if (playerInside)
        {
                player.transform.position = player.transform.position + (-Vector3.right * PushForce * Time.deltaTime);            
        }
    }
}