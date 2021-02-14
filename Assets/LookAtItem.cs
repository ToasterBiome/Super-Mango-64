using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtItem : MonoBehaviour
{
    protected Animator animator;
    public Transform lookObj = null;
    public bool ikAcive = false;
    public float lookWeight = 2f;

    void Start()
    {
       animator = GetComponent<Animator>();
      //target = GameObject.Find("head").transform;
     // animator = transform.GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(animator)
        {
            if(ikAcive)
            {
                if(lookObj != null)
                {
                    animator.SetLookAtWeight(lookWeight);
                    animator.SetLookAtPosition(lookObj.position);
                }
            }

            else
            {
                animator.SetLookAtWeight(0);
            }
        }     
    }
}
