using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GameData
{
    public static Transform Target;
    public static int SelectedCharacter = 0;
    //public static int CurrentLevel => PlayerPrefs.GetInt("current_level", 0);
    public static int CurrentLevel;
    //public static int CurrentAttempt => PlayerPrefs.GetInt("current_attempt", 0);
    public static int CurrentAttempt;

    public static int Highscore => PlayerPrefs.GetInt("highscore", 0);

    /*public static void NextLevel()
    {
        PlayerPrefs.SetInt("current_level", CurrentLevel + 1);
        PlayerPrefs.SetInt("current_attempt", 0);
    }*/
    public static void NextLevel()
    {
        CurrentLevel++;
    }

    public static void SetHighscore(int score)
    {
        if (Highscore< score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public static void PublishHighscore()
    {
         //Create my object
        var jsonOutput = new
        {
            highscore = GameData.Highscore.ToString()
        };

        //Tranform it to Json object
        string jsonData = JsonConvert.SerializeObject(jsonOutput);
        WebGLPluginJS.SendMessageToPage(jsonData);
    }

    public static int InsreasePerLevel = 25;



    /*public static void RestartLevel()
    {
        PlayerPrefs.SetInt("current_attempt", CurrentAttempt+1);
    }*/
    public static void RestartLevel()
    {
        CurrentAttempt++;
    }
}
