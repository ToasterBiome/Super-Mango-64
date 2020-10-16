using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public int curHealth;
    public int maxHealth = 5;

    public Animator animator;

    public GameObject pickupPoint;

    public PickupZone pickupZone;

    public GameObject targetPickup;

    public GameObject currentPickup;
    public Rigidbody pickupRB;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        curHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        pickupZone.controller = this;
    }

    public void PickupStart()
    {
        currentPickup = targetPickup;
        Rigidbody pickupRB = currentPickup.GetComponent<Rigidbody>();

        pickupRB.useGravity = false;
        currentPickup.transform.position = pickupPoint.transform.position;
        currentPickup.transform.parent = this.gameObject.transform;
        pickupRB.isKinematic = true;
        currentPickup.GetComponent<MeshCollider>().enabled = false;
    }

    public void SetPickupObject(GameObject pickupObject)
    {
        this.targetPickup = pickupObject;
        this.pickupRB = targetPickup.GetComponent<Rigidbody>();
    }

    public void PickupEnd()
    {

        pickupRB.useGravity = true;
        currentPickup.transform.parent = null;
        currentPickup.GetComponent<MeshCollider>().enabled = true;
        pickupRB.isKinematic = false;
        pickupRB.AddForce(30f * transform.forward + transform.up,ForceMode.Impulse);

        currentPickup = null;
        pickupRB = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            Die();
        }

        if(Input.GetMouseButtonDown(0))
        {
            PickupStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            PickupEnd();
        }





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
        if (Input.GetAxis("Mouse X") == 0)
        {
            rb.angularVelocity = Vector3.zero;
        }
        rb.AddForce(moveDirection * 10);
        if (rb.velocity.magnitude > 8f)
        {
            rb.velocity = rb.velocity.normalized * 8f;
        }
        rb.AddRelativeTorque(0, Input.GetAxis("Mouse X"), 0);

        //jump

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 15f, ForceMode.Impulse);
        }

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;

        animator.SetFloat("Speed", horizontalVelocity.magnitude);

        if(currentPickup != null)
        {
            pickupRB.MovePosition(pickupPoint.transform.position);
            pickupRB.MoveRotation(pickupPoint.transform.rotation);
        }
    }

    void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }
}
