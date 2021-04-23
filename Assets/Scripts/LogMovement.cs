using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject player;
    public int current = 0;
    public float speed;
    float wRadius = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.SetParent(transform);
            //player.transform.localScale = Vector3.one;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.SetParent(null, true);
            player.transform.localScale = Vector3.one;
        }
    }
    private void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < wRadius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }
}
