using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Socket.Quobject.SocketIoClientDotNet.Client;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float resetDelay;

    public float time;
    public bool timerStarted = false;

    protected QSocket socket = null;

    public BetterPlayerController player;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    void Start()
    {
        /*
        if(socket == null)
        {
            Debug.Log("start");
            socket = IO.Socket("http://localhost:3000");

            socket.On(QSocket.EVENT_CONNECT, () => {
                Debug.Log("Connected");
                socket.Send("Hi!");
            });

            socket.On("chat", data => {
                Debug.Log("data : " + data);
            });

            socket.On(QSocket.EVENT_CONNECT_ERROR, (error) => {
                Debug.Log(error);
                Debug.Log("Broken");
            });

            socket.On(QSocket.EVENT_CONNECT_TIMEOUT, () => {
                Debug.Log("Broken2");
            });

            socket.On(QSocket.EVENT_ERROR, () => {
                Debug.Log("Broken3");
            });
        } else
        {
            Debug.Log("Socket already exists");
        }
        */

        player = FindObjectOfType<BetterPlayerController>();

        GameObject cutscene = GameObject.Find("Cutscene");

        if (cutscene != null)
        {
            if (cutscene.activeInHierarchy)
            {
                StartCoroutine(EndCutscene());
            }
            else
            {
                player.canMove = true;
            }
        }
        else
        {
            player.canMove = true;
        }



        
        
    }

    void OnDestroy()
    {
        if(socket != null) socket.Close();
    }

    public IEnumerator EndCutscene() //do not ever do this ever, it's horrible programming, BE BETTER THAN ME! - Alex D.
    {
        //if we simply know the time of the introductory cutscene....
        yield return new WaitForSeconds(20f); //we can just wait that long and then enable the player.
        player.canMove = true;
        yield return null;
    }

    public void Win()
    {
        StopTimer();
        Time.timeScale = 1f;
        StartCoroutine(player.WinAnimation());
        StartCoroutine(DoWin());
        PlayerPrefs.SetFloat("bestTime", time);
    }

    public IEnumerator DoWin()
    {
        yield return new WaitForSeconds(3f);
        HUD.instance.vignetteAnimator.SetTrigger("FadeOut");
        Invoke("Reset", resetDelay);
        yield return null;
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

        if(timerStarted)
        {
            time += Time.deltaTime;
        }

        float minutes = Mathf.FloorToInt(time / 60);

        float seconds = Mathf.FloorToInt(time % 60);

        TimeSpan span = TimeSpan.FromSeconds((double)time);

        string formattedSpan = $"{span.Minutes:D2}:{span.Seconds:D2}.{span.Milliseconds:D3}";

        //ui stuff
        if(HUD.instance.timerText != null)
        {
            HUD.instance.timerText.text = formattedSpan;
        }
        
    }

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void StopTimer()
    {
        timerStarted = false;
    }
}
