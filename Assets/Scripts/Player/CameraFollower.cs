using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform followTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followTransform.position, Time.deltaTime * 10);
        transform.rotation = Quaternion.Lerp(transform.rotation, followTransform.rotation, Time.deltaTime * 10);
    }

}
