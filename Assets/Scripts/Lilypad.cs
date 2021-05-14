using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    private Animator padAnim;
    private readonly int padAnimParam = Animator.StringToHash("isOnPad");
    private bool isOnPad;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        isOnPad = false;

        padAnim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if(isOnPad == true)
        {
            StartCoroutine(UpdateLilypad(waitTime));
        }
        if(isOnPad == false)
        {
            UpdateAnimation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOnPad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOnPad = false;
        }
    }

    public void UpdateAnimation()
    {
        if(padAnim != null)
        {
            padAnim.SetBool(padAnimParam, isOnPad);
        }
    }

    IEnumerator UpdateLilypad(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UpdateAnimation();
    }

}
