using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableObject : MonoBehaviour
{

    private Vector3 originalPosition; 
    private Quaternion originalRotation;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalRotation = new Quaternion(0, 0, 0, 0);
        rb = GetComponent<Rigidbody>();
    }

    public void ReturnToOriginalPosition()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.Sleep();

        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;        
    }
}
