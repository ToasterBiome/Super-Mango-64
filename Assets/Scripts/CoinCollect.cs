using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{

    public ParticleSystem collect;
    private bool isCoroutineExecuting = false;
    public bool isCollected = false;

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
        if(other.tag == "Player" && isCollected == false)
        {
            CreateParticles();
            other.GetComponent<PlayerPoints>().points++;
            isCollected = true;

            GetComponent<Renderer>().enabled = false;

            StartCoroutine(ExecuteAfterTime(1f, () =>
            {
                Destroy(gameObject);
            }));

        }
    }

    void CreateParticles()
    {
        collect.Play();
    }
    IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }
}
