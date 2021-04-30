using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup creditsGroup;
    public float fadeSpeed = 2f;
    public IEnumerator currentFade;

    public Image fade;

    public void StartGame()
    {
        StartCoroutine(GameFade());
    }

    public IEnumerator GameFade()
    {
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
}
