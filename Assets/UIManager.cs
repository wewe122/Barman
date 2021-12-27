using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/*
 * This script is in charge on changing the UI numbers values
 * such as: time, score, glasses quantities
 */

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Time;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI smallGlassTmp;
    public TextMeshProUGUI largeGlassTmp;


    public void SetTime(int minutes, int seconds){
        Time.SetText("{{0:00}:{1:00}", minutes, seconds);
    }
    public void SetScore(int score){
        Score.SetText(score+"");
    }
    public void SetSmallGlassQuantity(int quantity){
        smallGlassTmp.SetText(quantity+"");
    }
    public void SetLargeGlassQuantity(int quantity){
        largeGlassTmp.SetText(quantity+"");
    }
}
