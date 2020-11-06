using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollector : MonoBehaviour
{

    public bool hasPickedUpGloves;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.tag == "Player")
        {
            hasPickedUpGloves = true;
            other.GetComponent<PlayerController>().EquipGloves();
            Destroy(gameObject);
        }
    }
}
