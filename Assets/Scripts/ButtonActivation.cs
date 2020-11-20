using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivation : MonoBehaviour
{
    [SerializeField] GameObject spawnable;

    public int pressureWeight;
    private int cumulativeWeight;

    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        spawnable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pressureWeight == cumulativeWeight)
        {
            spawnable.SetActive(true);
            
        }

        if (pressureWeight != cumulativeWeight)
        {
            spawnable.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var worldObject = other.GetComponent<RespawnableObject>();

        if (other.CompareTag("Pickup"))
        {
            cumulativeWeight += worldObject.weight;
            source.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var worldObject = other.GetComponent<RespawnableObject>();

        if (other.CompareTag("Pickup"))
        {
            cumulativeWeight -= worldObject.weight;
        }
    }
}
