using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float cooldown;
    public float maxCooldown = 5f;

    public Vector3 force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        } else if (cooldown < 0)
        {
            cooldown = 0;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(cooldown <= 0)
        {
            if(other.tag == "Player")
            {
                other.GetComponent<Rigidbody>().AddForce(force * 10f);
                cooldown = maxCooldown;
            }
        }
    }
}
