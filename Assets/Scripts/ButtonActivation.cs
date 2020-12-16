using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivation : MonoBehaviour
{
    [SerializeField] GameObject spawnable;

    public int pressureWeight;
    private int cumulativeWeight;

    public AudioSource source;

    private Animator buttonAnim;
    private readonly int buttonAnimParam = Animator.StringToHash("isButtonDown");
    public bool isDown;

    // Start is called before the first frame update
    void Start()
    {
        spawnable.SetActive(false);
        isDown = false;

        buttonAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pressureWeight == cumulativeWeight)
        {
            spawnable.SetActive(true);
            
        }

        if (pressureWeight != cumulativeWeight)
        {
            spawnable.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var worldObject = other.GetComponent<RespawnableObject>();

        if (other.CompareTag("Pickup"))
        {
            cumulativeWeight += worldObject.weight;
            isDown = true;
            UpdateAnimation();
            source.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var worldObject = other.GetComponent<RespawnableObject>();

        if (other.CompareTag("Pickup"))
        {
            cumulativeWeight -= worldObject.weight;
            isDown = false;
            UpdateAnimation();
        }
    }

    public void UpdateAnimation()
    {
        buttonAnim.SetBool(buttonAnimParam, isDown);
    }
}
