using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHit(Hitbox box, Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.gameObject.GetComponent<BetterPlayerController>().Damage(box.damage,false);

            /*
            Vector3 force = other.transform.position - box.transform.position;
            force = force.normalized;
            Vector3 clampedForce = new Vector3(force.x, 0.125f, force.z);
            other.GetComponent<BetterPlayerController>().extraForce = clampedForce * 8;
            Debug.Log(clampedForce);
            */
        }
    }
}
