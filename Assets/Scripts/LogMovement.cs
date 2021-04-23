using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject player;
    int current = 0;
    float rotspeed;
    public float speed;
    float wRadius = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
    private void Update()
    {
        if(Vector3.Distance(waypoints[current].transform.position,transform.position)< wRadius)
        {
            current++;
            if(current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }
}
