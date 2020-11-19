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
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Damage(box.damage);
            Vector3 force = other.transform.position - box.transform.position;
            force = force.normalized;
            Vector3 clampedForce = new Vector3(force.x, 0.025f, force.z);
            other.GetComponent<Rigidbody>().velocity = clampedForce * 128f;
            Debug.Log(clampedForce);
        }
    }
}
