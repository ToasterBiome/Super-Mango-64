using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimationTriggers : MonoBehaviour
{
    public AudioClip AttackSound;
    public AudioClip JumpSound;
    private AudioSource source;
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        source.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Attack()
    {
        source.PlayOneShot(AttackSound);
    }
    void Jump()
    {
        source.PlayOneShot(JumpSound);
    }
}
