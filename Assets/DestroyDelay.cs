using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 12f);
    }
}
