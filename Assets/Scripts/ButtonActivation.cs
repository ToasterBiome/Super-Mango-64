using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivation : MonoBehaviour
{
    [SerializeField] GameObject spawnable;

    // Start is called before the first frame update
    void Start()
    {
        spawnable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            spawnable.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            spawnable.SetActive(false);
        }
    }
}
