using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        //Fix later
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<PlayerController>();

        if (other.CompareTag("Player"))
        {
            Player.Die();
        }
    }

}
