using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Animator animator;
    public bool isGameStarting = false;
    public FaceController wakeUp;
    public ParticleSystem zees;

    public CanvasGroup creditsGroup;
    public float fadeSpeed = 2f;
    public IEnumerator currentFade;

    public Image fade;


    //option stuff
    public GameObject optionsMenu;

    public Slider sensitivitySlider;
    public Toggle invertToggle;

    public TextMeshProUGUI bestTimeText;

    bool cameraInverted;
    float cameraSensitivity;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        wakeUp.ChangeFace("Closed_eyes", "OOO_OOO");
        TimeSpan span = TimeSpan.FromSeconds((double)PlayerPrefs.GetFloat("bestTime", 0f));
        string formattedSpan = $"{span.Minutes:D2}:{span.Seconds:D2}.{span.Milliseconds:D3}";
        bestTimeText.text = $"Best Time: {formattedSpan}";
    }

    public void StartGame()
    {
        if (isGameStarting == false)
        {
            isGameStarting = true;
            StartCoroutine(GameFade());
        }
        
    }

    public IEnumerator GameFade()
    {
        animator.SetTrigger("GameStart");
        zees.Stop();
        wakeUp.ChangeFace("Default_Eyes", "smile");
        yield return new WaitForSeconds(5);
        creditsGroup.gameObject.SetActive(true);
        while (fade.color.a < 1)
        {
            fade.color = new Color(0, 0, 0, fade.color.a + Time.deltaTime * 4f);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("W1L1");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        Debug.Log("Show Credits");
        if(currentFade != null) StopCoroutine(currentFade);
        currentFade = FadeIn();
        StartCoroutine(currentFade);
    }

    public void HideCredits()
    {
        Debug.Log("Hide Credits");
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = FadeOut();
        StartCoroutine(currentFade);
    }

    private IEnumerator FadeIn()
    {
        creditsGroup.gameObject.SetActive(true);
        while (creditsGroup.alpha < 1)
        {
            creditsGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FadeOut()
    {
        while (creditsGroup.alpha > 0)
        {
            creditsGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        creditsGroup.gameObject.SetActive(false);
    }

    public void OpenOptions()
    {

        invertToggle.isOn = (PlayerPrefs.GetInt("cameraInvert", 0) == 1 ? true : false);
        sensitivitySlider.value = PlayerPrefs.GetFloat("cameraSensitivity", 1);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        //close without saving?
        optionsMenu.SetActive(false);
    }

    public void SaveOptions()
    {
        //save these
        PlayerPrefs.SetInt("cameraInvert", cameraInverted ? 1 : 0);
        PlayerPrefs.SetFloat("cameraSensitivity", cameraSensitivity);
    }

    public void OptionInvertCamera(bool value)
    {
        cameraInverted = value;
    }

    public void OptionCameraSensitivity(float value)
    {
        cameraSensitivity = value;
    }
}
