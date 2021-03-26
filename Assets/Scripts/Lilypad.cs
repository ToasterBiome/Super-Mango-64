using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    private Animator padAnim;
    private readonly int padAnimParam = Animator.StringToHash("isOnPad");
    public bool isOnPad;

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
            StartCoroutine(UpdateLilypad());
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
        padAnim.SetBool(padAnimParam, isOnPad);
    }

    IEnumerator UpdateLilypad()
    {
        yield return new WaitForSeconds(2f);
        UpdateAnimation();
    }

}
