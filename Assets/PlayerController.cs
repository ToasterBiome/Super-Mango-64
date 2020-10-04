using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //rb.AddRelativeForce(new Vector3(Input.GetAxisRaw("Vertical") * 10f, 0, -Input.GetAxisRaw("Horizontal") * 10f));

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * Input.GetAxisRaw("Vertical") * 2f + cameraRight * Input.GetAxisRaw("Horizontal");
        Debug.Log(Input.GetAxis("Mouse X"));
        if(Input.GetAxis("Mouse X") == 0)
        {
            rb.angularVelocity = Vector3.zero;
        }
        rb.AddForce(moveDirection * 10);
        if(rb.velocity.magnitude > 8f)
        {
            rb.velocity = rb.velocity.normalized * 8f;
        }
        rb.AddRelativeTorque(0, Input.GetAxis("Mouse X"),0);

        //jump

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 15f, ForceMode.Impulse);
        }
    }
}
