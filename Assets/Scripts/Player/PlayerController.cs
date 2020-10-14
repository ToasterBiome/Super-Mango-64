﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public int curHealth;
    public int maxHealth = 5;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        curHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
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
