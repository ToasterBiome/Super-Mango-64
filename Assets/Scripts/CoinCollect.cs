﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{

    public GameObject collect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CreateParticles();
            other.GetComponent<PlayerPoints>().points++;
            Destroy(gameObject);
        }
    }

    void CreateParticles()
    {
        GameObject spawnedParticles = Instantiate(collect, transform.position, Quaternion.identity);
        Destroy(spawnedParticles, 3.5f);
    }
}
