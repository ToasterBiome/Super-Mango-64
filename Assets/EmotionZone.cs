using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionZone : MonoBehaviour
{
    public string eyes;
    public string mouth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<FaceController>().ChangeFace(eyes, mouth);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<FaceController>().ResetFace();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
