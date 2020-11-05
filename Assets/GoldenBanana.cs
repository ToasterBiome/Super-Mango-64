using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBanana : MonoBehaviour
{
   void OnTriggerEnter()
   {
        GameManager.instance.Win();

        Destroy(gameObject);
   }

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }
}
