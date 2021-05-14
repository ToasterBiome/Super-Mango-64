using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public Sprite[] HeartSprites;
    public Image HeartUI;
    public Image GlovesIcon;

    public BetterPlayerController player;

    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestTimeText;

    public Animator vignetteAnimator;

    public GameObject quitMenu;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Debug.LogError("TWO UIs");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BetterPlayerController>();
        GlovesIcon.enabled = false;

        TimeSpan span = TimeSpan.FromSeconds((double)PlayerPrefs.GetFloat("bestTime", 0f));
        string formattedSpan = $"{span.Minutes:D2}:{span.Seconds:D2}.{span.Milliseconds:D3}";
        bestTimeText.text = $"Best Time: {formattedSpan}";

        Cursor.visible = false;

    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(quitMenu.activeInHierarchy)
            {
                CloseQuitMenu();
            } else
            {
                OpenQuitMenu();
            }
        }

        if(player == null)
        {
            return;
        }

        HeartUI.sprite = HeartSprites[player.curHealth];
        if (player.hasGloves)
        {
            GlovesIcon.enabled = true;
        }
        bananaText.text = player.bananas.ToString();

    }

    public void OpenQuitMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        quitMenu.SetActive(true);
    }

    public void CloseQuitMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        quitMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(GoToMainMenu());
    }

    public IEnumerator GoToMainMenu()
    {
        HUD.instance.vignetteAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }

}
