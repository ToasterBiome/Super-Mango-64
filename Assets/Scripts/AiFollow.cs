using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiFollow : MonoBehaviour
{
    private NavMeshAgent Mob;
    public GameObject Player;
    public float MobDistanceRun = 4.0f;
    public Transform[] movespots;
    public float waitTime;
    public float startWaitTime;
    private int randomSpot;

    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, movespots.Length);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movespots[randomSpot].position, MobDistanceRun * Time.deltaTime);

        if (Vector3.Distance(transform.position, movespots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
               randomSpot = Random.Range(0, movespots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if(distance < MobDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            Mob.SetDestination(newPos);
        }
    }
}
