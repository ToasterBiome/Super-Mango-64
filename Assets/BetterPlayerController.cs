using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float jumpHeight = 15f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Animator animator;

    Vector3 horizontalMovement;
    Vector3 verticalMovement;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        horizontalMovement = new Vector3(h, 0, v);

        verticalMovement += Physics.gravity * Time.deltaTime;

        

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalMovement.y = jumpHeight;
            animator.SetTrigger("Jump");
        }

        animator.SetBool("isInAir", !controller.isGrounded);

        if (horizontalMovement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(horizontalMovement.x, horizontalMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime + verticalMovement * Time.deltaTime);
        } else
        {
            controller.Move(verticalMovement * Time.deltaTime);
        }

        animator.SetFloat("Speed", horizontalMovement.magnitude);



    }
}
