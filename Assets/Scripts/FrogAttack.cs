using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrogAttack : MonoBehaviour
{
    public Transform player;
    Animator anim;

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotSpeed = 2f;
    public float speed = 2f;
    public float accuracyWP = 1.0f;
    public float distanceNear = 5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if(state == "patrol" && waypoints.Length > 0)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalking", true);
            if(Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {
                currentWP = Random.Range(0, waypoints.Length);
            }

            direction = waypoints[currentWP].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }

        if (Vector3.Distance(player.position, this.transform.position) < distanceNear && (angle < 90 || state == "pursuing")) 
        {
            state = "pusuing";
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction),rotSpeed*Time.deltaTime);

            if(direction.magnitude > .95)
            {
                this.transform.Translate(0, 0, Time.deltaTime * speed);
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsAttacking", false);
            }
            else
            {
                anim.SetBool("IsAttacking", true);
                anim.SetBool("IsWalking", false);
            }
        }
        else
        {
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsAttacking", false);
            state = "patrol";
        }
    }
}
