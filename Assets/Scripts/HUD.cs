using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite[] HeartSprites;
    public Image HeartUI;
    public Image GlovesIcon;

    public PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

    }

}
