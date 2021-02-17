using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public List<GameObject> eyes;
    public List<GameObject> mouths;

    public GameObject currentEye;
    public GameObject currentMouth;

    public string defaultEyes = "Default_Eyes";
    public string defaultMouth = "smile";

    public float timeSinceLastBlink = 0f;
    public float blinkInterval = 4f;

    public void ChangeEyes(string eyeName)
    {
        if (currentEye != null)
        {
            currentEye.SetActive(false);
        }
        foreach (GameObject eye in eyes)
        {
            if (eye.name == eyeName)
            {
                currentEye = eye;
                eye.SetActive(true);
                return;
            }
        }
        Debug.LogError("There is no eye with the name: " + eyeName);
    }

    public void ChangeMouth(string mouthName)
    {
        if(currentMouth != null)
        {
            currentMouth.SetActive(false);
        }
        foreach(GameObject mouth in mouths)
        {
            if (mouth.name == mouthName)
            {
                currentMouth = mouth;
                mouth.SetActive(true);
                return;
            }
        }
        Debug.LogError("There is no mouth with the name: " + mouthName);
    }

    public void ChangeFace(string eyeName, string mouthName)
    {
        ChangeEyes(eyeName);
        ChangeMouth(mouthName);
    }

    public void ResetFace()
    {
        ChangeFace(defaultEyes, defaultMouth);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetFace();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastBlink += Time.deltaTime;
        if(timeSinceLastBlink >= blinkInterval)
        {
            if(currentEye.name == defaultEyes)
            {
                StartCoroutine("Blink");
            }
            timeSinceLastBlink -= blinkInterval;
        }
    }

    IEnumerator Blink()
    {
        ChangeEyes("Closed_eyes");
        yield return new WaitForSeconds(0.2f);
        if(currentEye.name == "Closed_eyes") //only reset it back to default if it hasn't been changed yet.
        {
            ResetFace();
        }
        
    }
}
