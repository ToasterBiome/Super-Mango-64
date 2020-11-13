﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pickup;

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
    public bool inAir = false;

    [Header("Things to test!")]
    public float jumpForce = 15f;
    public float throwForce = 30f;
    public float walkSpeed = 10f;

    public GameObject gloves;
    public GameObject hands;
    public bool hasGloves = false;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        curHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        pickupZone.controller = this;
    }

    public void PickupStart()
    {
        if (targetPickup == null) return;
        //get type
        PickupType type = targetPickup.GetComponent<Pickup>().type;
        if(type == PickupType.Boulder)
        {
            if(!hasGloves)
            {
                return; //don't attempt pickup if you don't have the gloves
            }
        }
        currentPickup = targetPickup;
        Rigidbody pickupRB = currentPickup.GetComponent<Rigidbody>();

        pickupRB.useGravity = false;
        currentPickup.transform.position = pickupPoint.transform.position;
        currentPickup.transform.parent = this.gameObject.transform;
        pickupRB.isKinematic = true;
        currentPickup.GetComponent<MeshCollider>().enabled = false;
        animator.SetTrigger("Pickup");
        animator.SetBool("Holding", true);
    }

    public void SetPickupObject(GameObject pickupObject)
    {
        if(pickupObject != null)
        {
            this.targetPickup = pickupObject;
            this.pickupRB = targetPickup.GetComponent<Rigidbody>();
        } else
        {
            this.targetPickup = null;
            this.pickupRB = null;
        }
        
    }

    public void PickupEnd()
    {
        if (currentPickup == null) return;
        pickupRB.useGravity = true;
        currentPickup.transform.parent = null;
        currentPickup.GetComponent<MeshCollider>().enabled = true;
        pickupRB.isKinematic = false;
        pickupRB.AddForce(throwForce * transform.forward + transform.up,ForceMode.Impulse);

        currentPickup = null;
        pickupRB = null;
        animator.SetBool("Holding", false);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!inAir)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                animator.SetTrigger("Jump");
                inAir = true;
            }
            
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

        //Vector3 moveDirection = cameraForward * Input.GetAxisRaw("Vertical") * 2f;
        Vector3 moveDirection = cameraForward * Input.GetAxisRaw("Vertical") * 2f + cameraRight * Input.GetAxisRaw("Horizontal");
        if (Input.GetAxis("Mouse X") == 0)
        {
            rb.angularVelocity = Vector3.zero;
        }
        //rb.AddForce(moveDirection * walkSpeed);
        rb.velocity = new Vector3(0,rb.velocity.y,0) + moveDirection * walkSpeed;
        if (rb.velocity.magnitude > 8f)
        {
            rb.velocity = rb.velocity.normalized * 8f;
        }
        rb.AddRelativeTorque(0, Input.GetAxis("Mouse X") * 0.8f, 0);


        //jump



        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;

        animator.SetFloat("Speed", horizontalVelocity.magnitude);

        if(currentPickup != null && pickupRB != null)
        {
            pickupRB.MovePosition(pickupPoint.transform.position);
            pickupRB.MoveRotation(pickupPoint.transform.rotation);
        }
        Debug.DrawRay(transform.position, -transform.up * 0.75f, Color.red);
        if(Physics.Raycast(transform.position,-transform.up,0.75f))
        {
            inAir = false;
            animator.SetBool("isInAir", false);
        } else
        {
            inAir = true;
            animator.SetBool("isInAir", true);
        }
    }

    public void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }
    public void EquipGloves()
    {
        gloves.SetActive(true);
        hands.SetActive(false);
        hasGloves = true;
    }
}
