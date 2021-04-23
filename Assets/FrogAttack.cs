using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrogAttack : MonoBehaviour
{
    public Transform player;
    static Animator anim;
    public Transform[] points;
    int current;
    public float speed;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //current = 0;
    }

    private void Update()
    {
        //Waypoint
        //if(transform.position != points[current].position)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
        //}
        //else
        //{
        //    current = (current + 1) % points.Length;
        //}

        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if(Vector3.Distance(player.position,this.transform.position) < 10 && angle < 90)
        {
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction),0.1f);

            anim.SetBool("IsIdle", false);
            if(direction.magnitude > .95)
            {
                this.transform.Translate(0, 0, 0.05f);
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsAttacking", false);
            }

            else if(Vector3.Distance(player.position, this.transform.position) < 1.1 && angle < 90)
            {
                anim.SetBool("IsAttacking", true);
                anim.SetBool("IsWalking", false);
            }
        }

        else
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", false);
        }
    }
}
