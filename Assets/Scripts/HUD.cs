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
    public PowerUpCollector collector;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        collector = GameObject.FindGameObjectWithTag("GlovesPickup").GetComponent<PowerUpCollector>();
        GlovesIcon.enabled = false;
    }

    void Update()
    {
        HeartUI.sprite = HeartSprites[player.curHealth];

        if (collector.hasPickedUpGloves)
        {
            GlovesIcon.enabled = true;
        }

    }

}
