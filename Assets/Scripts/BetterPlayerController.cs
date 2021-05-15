using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pickup;

public class BetterPlayerController : MonoBehaviour
{
    //Health
    public int curHealth;
    public int maxHealth = 5;

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float jumpHeight = 5f;
    public float throwForce = 18f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Animator animator;

    public Vector3 horizontalMovement;
    public Vector3 verticalMovement;

    //Pickup related objects
    public GameObject pickupPoint;
    public PickupZone pickupZone;
    public GameObject targetPickup;
    public GameObject currentPickup;
    public Rigidbody pickupRB;

    //Item Handling
    public GameObject gloves;
    public GameObject hands;
    public bool hasGloves = false;

    //Damage Handling
    public float damageCooldown = 0;
    public float maxDamageCooldown = 1f;

    public Vector3 extraForce = Vector3.zero;

    //Physics matrials
    public float accelerationSpeed, decelerationSpeed;
    public float maxSpeed;

    //trajectory prediction

    public GameObject trajectoryDummyPrefab;
    public GameObject trajectoryDummy;

    public float trajTime = 0f;
    public float maxTrajTime = 2f;

    //particle effects
    public ParticleSystem dust;
    public ParticleSystem stars;
    public ParticleSystem fountain;
    public ParticleSystem splash;

    //banana counter

    public int bananas = 0;

    public bool inWater = false;

    public float waterJumpModifier = 0.8f;

    public Vector3 waterVelocity;

    public Vector3 respawnPoint;
    public Quaternion respawnRotation;

    public CinemachineFreeLook thirdPersonCamera;

    //cannot move juice
    public bool canMove = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponentInChildren<Animator>();
        pickupZone.controller = this;
        respawnPoint = transform.position;
        respawnRotation = transform.rotation;

        thirdPersonCamera.m_XAxis.m_MaxSpeed = 280 + (280 * PlayerPrefs.GetFloat("cameraSensitivity", 1));
        thirdPersonCamera.m_YAxis.m_InvertInput = (PlayerPrefs.GetInt("cameraInvert", 0) == 1);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(!canMove)
        {
            h = 0;
            v = 0;
        }

        //timer does not start until you move
        if(h+v != 0 && !GameManager.instance.timerStarted)
        {
            GameManager.instance.StartTimer();
        }

        horizontalMovement = new Vector3(h, 0, v);

        if(verticalMovement.y > Physics.gravity.y)
        {
            verticalMovement += Physics.gravity * Time.deltaTime;
        }

        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            if(inWater)
            {
                verticalMovement.y = jumpHeight * waterJumpModifier;
               

            } else
            {
                verticalMovement.y = jumpHeight;
            }
            
            animator.SetTrigger("Jump");
            CreateDust();
        }

        animator.SetBool("isInAir", !isGrounded());



        if (horizontalMovement.magnitude >= 0.1f)
        {
            if(speed < maxSpeed)
            {
                speed += accelerationSpeed * Time.deltaTime;
            }
            float targetAngle = Mathf.Atan2(horizontalMovement.x, horizontalMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime + verticalMovement * Time.deltaTime);

        } else
        {
            if (speed > 0 && horizontalMovement.magnitude<0.1f)
            {
                speed -= decelerationSpeed * Time.deltaTime;
            }

            controller.Move(verticalMovement * Time.deltaTime);
        }

        if(inWater)
        {
            controller.Move(waterVelocity * Time.deltaTime);
            CreateSplash();
        }

        animator.SetFloat("Speed", horizontalMovement.magnitude);

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                damageCooldown = 0;
            }
        }

        if(currentPickup)
        {
            trajTime += Time.deltaTime;
            if(trajTime >= maxTrajTime)
            {
                trajTime -= maxTrajTime;
                //move it back
                trajectoryDummy.transform.position = currentPickup.transform.position;
                trajectoryDummy.GetComponent<TrailRenderer>().Clear();
                trajectoryDummy.GetComponent<Rigidbody>().AddForce(throwForce * transform.forward + transform.up, ForceMode.VelocityChange);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (animator.GetBool("Holding"))
            {
                PickupDrop();
            }
            else
            {
                PickupStart();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            PickupThrow();

        }

        if (currentPickup != null && pickupRB != null)
        {
            currentPickup.transform.position = pickupPoint.transform.position;
            currentPickup.transform.rotation = pickupPoint.transform.rotation;
        }

    }


    public void ExternalForce(Vector3 force)
    {
        verticalMovement += new Vector3(0, force.y, 0);
    }

    public void PickupStart()
    {
        if (targetPickup == null) return;
        //get type
        PickupType type = targetPickup.GetComponent<Pickup>().type;
        if (type == PickupType.Boulder)
        {
            if (!hasGloves)
            {
                return; //don't attempt pickup if you don't have the gloves
            }
        }
        currentPickup = targetPickup;
        pickupRB = currentPickup.GetComponent<Rigidbody>();
        pickupRB.useGravity = false;
        currentPickup.transform.position = pickupPoint.transform.position;
        currentPickup.transform.parent = this.gameObject.transform;
        pickupRB.isKinematic = true;
        targetPickup = null;
        currentPickup.GetComponent<MeshCollider>().enabled = false;
        animator.SetTrigger("Pickup");
        animator.SetBool("Holding", true);

        trajectoryDummy = Instantiate(trajectoryDummyPrefab);
        trajectoryDummy.transform.position = currentPickup.transform.position;
        trajectoryDummy.GetComponent<Rigidbody>().AddForce(throwForce * transform.forward + transform.up, ForceMode.VelocityChange);

    }

    public void SetPickupObject(GameObject pickupObject)
    {
        if (pickupObject != null)
        {
            this.targetPickup = pickupObject;
        }
        else
        {
            this.targetPickup = null;
        }

    }

    public void PickupThrow()
    {
        if (currentPickup == null) return;
        pickupRB.useGravity = true;
        currentPickup.transform.parent = null;
        currentPickup.GetComponent<MeshCollider>().enabled = true;
        pickupRB.isKinematic = false;

        pickupRB.AddForce(throwForce * transform.forward + transform.up, ForceMode.VelocityChange);

        currentPickup = null;
        pickupRB = null;
        animator.SetTrigger("PickupThrow");
        animator.SetBool("Holding", false);

        if(trajectoryDummy)
        {
            Destroy(trajectoryDummy);
            trajectoryDummy = null;
        }

    }

    public void PickupDrop()
    {
        if (currentPickup == null) return;
        pickupRB.useGravity = true;
        currentPickup.transform.parent = null;
        currentPickup.transform.position = pickupZone.transform.position;
        currentPickup.GetComponent<MeshCollider>().enabled = true;
        pickupRB.isKinematic = false;

        currentPickup = null;
        pickupRB = null;
        animator.SetBool("Holding", false);

        if (trajectoryDummy)
        {
            Destroy(trajectoryDummy);
            trajectoryDummy = null;
        }
    }

    public void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public IEnumerator DeathAnimation()
    {
        HUD.instance.vignetteAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        transform.position = respawnPoint;
        transform.rotation = respawnRotation;
        curHealth = maxHealth;
        damageCooldown = 0;
        HUD.instance.vignetteAnimator.SetTrigger("FadeIn");
    }

    public IEnumerator WinAnimation()
    {
        canMove = false;
        thirdPersonCamera.m_XAxis.m_InputAxisName = ""; //why does cinemachine make you do this instead of a bool?
        thirdPersonCamera.m_YAxis.m_InputAxisName = "";
        transform.rotation = Quaternion.Euler(transform.rotation.x, thirdPersonCamera.m_XAxis.Value + 180f, transform.rotation.z);
        animator.SetTrigger("WinAnimation");
        yield return null;

    }

    public void Damage(int dmg,bool forceThroughCooldown)
    {
        if ((damageCooldown <= 0) || forceThroughCooldown)
        {
            CreateStars();
            curHealth -= dmg;
            if(curHealth <= 0)
            {
                damageCooldown = 9999;
                StartCoroutine(DeathAnimation());
            }
        }


    }
    public void EquipGloves()
    {
        gloves.SetActive(true);
        hands.SetActive(false);
        hasGloves = true;
    }

    void CreateDust()
    {
        dust.Play();
    }
    void CreateStars()
    {
        stars.Play();
    }

    void CreateSplash()
    {
        splash.Play();
    }

    public bool isGrounded()
    {
        bool multiCheck = false;

        //center
        if(Physics.Raycast(transform.position, -Vector3.up, 0.535f))
        {
            multiCheck = true;
        }

        //right
        if (Physics.Raycast(transform.position + new Vector3(0.5f,0,0), -Vector3.up, 0.55f))
        {
            multiCheck = true;
        }

        //left
        if (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), -Vector3.up, 0.55f))
        {
            multiCheck = true;
        }

        //forward maybe?
        if (Physics.Raycast(transform.position + new Vector3(0, 0, 0.25f), -Vector3.up, 0.55f))
        {
            multiCheck = true;
        }

        //maybe backwards
        if (Physics.Raycast(transform.position + new Vector3(0, 0, -0.25f), -Vector3.up, 0.55f))
        {
            multiCheck = true;
        }

        return multiCheck;
        //return Physics.Raycast(transform.position, -Vector3.up, 0.535f);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.position + (-Vector3.up * 0.535f));
    }
}
