using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject youWinText;
    public float resetDelay;
    public AudioSource music;
    public AudioSource win;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    public void Win()
    {
        youWinText.SetActive (true);
        Time.timeScale = .5f;
        Invoke("Reset", resetDelay);
        music.Stop();
        win.Play();
    }

    private void Reset()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }
}
