using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    }

    void Update()
    {
        


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

}
