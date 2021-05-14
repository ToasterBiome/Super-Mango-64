using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    void Awake()
    {
        Camera.main.backgroundColor = Color.black;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.cullingMask = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadUI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadUI()
    {
        

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.cullingMask = ~0;

        yield return null;
    }
}
