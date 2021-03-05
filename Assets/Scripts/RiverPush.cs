using UnityEngine;

public class RiverPush : MonoBehaviour
{
    public float PushForce = 2f;

    bool playerInside;
    GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInside = true;
            player = other.gameObject;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
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