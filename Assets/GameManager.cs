using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI time;
    public GameObject congScreen;
    public GameObject TimeIsUpScreen;

    [SerializeField]
    public UIManager uiManager;

    [Tooltip("The amount of drunks to spawn in this level")]
    public int NumOfDrunks;

    [Tooltip("The amount of small glasses bartender can use to serve beer")]
    public int smallGlassQuantity;
    
    [Tooltip("The amount of large glasses bartender can use to serve beer")]
    public int largeGlassQuantity;

    [Tooltip("The amount of score that player recieve for single good serve")]
    public int scoreToAdd;

    [Tooltip("The amount of score that player needs to reach next level")]
    public int GoalScore;

    [Tooltip("The amount of score that player needs to reach next level")]
    public float LevelTimeInSeconds = 40;

    private int CurrentScore = 0;

    public static GameManager gameManager;
    void Awake()
    {
        if (gameManager == null)
            gameManager = this;
        uiManager.SetTime(Mathf.FloorToInt(LevelTimeInSeconds / 60), Mathf.FloorToInt(LevelTimeInSeconds % 60));
    }

 

    // Update is called once per frame
    void Update()
    {
        LevelTimeInSeconds -= Time.deltaTime;
        if(LevelTimeInSeconds <= 0)
        {
            // if the time is up and the player didn`t reach the goal score
            if (!TimeIsUpScreen.activeSelf && !congScreen.activeSelf)
                TimeIsUpScreen.SetActive(true); 
            return;
        }
        var minutes = Mathf.FloorToInt(LevelTimeInSeconds / 60);
        var seconds = Mathf.FloorToInt(LevelTimeInSeconds % 60);
        DateTime CurrentTime = DateTime.ParseExact(time.text, "mm:ss", CultureInfo.InvariantCulture);
        if(CurrentTime.Second != seconds)
            uiManager.SetTime(minutes, seconds);
        
        /*
         * if the player reached GoalScore and the are some time remaining 
         * load next level screen
         */
        if (minutes >= 0 && seconds > 0 && CurrentScore >= GoalScore)
            congScreen.SetActive(true);

    }
    public void AddScore()
    {
        CurrentScore += scoreToAdd;
        uiManager.SetScore(CurrentScore);
    }
    public void DecreaseScore()
    {
        CurrentScore -= scoreToAdd / 3; // for now
        CurrentScore = CurrentScore < 0 ? 0 : CurrentScore;
        uiManager.SetScore(CurrentScore);
    }

}
