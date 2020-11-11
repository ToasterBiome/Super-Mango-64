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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnable.SetActive(true);
        }
    }
}
