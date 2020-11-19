using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToToggle;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
    }
}
