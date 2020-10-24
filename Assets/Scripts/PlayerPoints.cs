using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour
{
    public int points = 0;

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 35, 100, 35),"Score : " + points);
    }
}
