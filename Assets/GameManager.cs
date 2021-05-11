using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float resetDelay;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    public void Win()
    {
        Time.timeScale = .5f;
        HUD.instance.vignetteAnimator.SetTrigger("FadeOut");
        Invoke("Reset", resetDelay);
    }

    private void Reset()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            Win();
        }
    }
}
