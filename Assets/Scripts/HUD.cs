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
