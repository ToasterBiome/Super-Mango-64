using UnityEngine;

public class RiverPush : MonoBehaviour
{
    public int PushForce = 2;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            other.transform.Translate(-Vector3.right * Time.deltaTime * PushForce, Space.World);

    }
}