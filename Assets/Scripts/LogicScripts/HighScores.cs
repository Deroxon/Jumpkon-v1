using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GameObject.Find("HighScoreList").GetComponent<TextMeshProUGUI>();
    }

    [ContextMenu("Load Highscores")]
    private void LoadHighscores()
    {
        string[] GetAllHighscores = TimerScript.Instance.GetTimes();
        string displayedHighscore = "";

        // Sorting List by Converting values to Miliseconds
         var sortedTimeStrings = GetAllHighscores.OrderBy(timeStr => ConvertToMilliseconds(timeStr)).ToArray();


         for (int i = 0; i < sortedTimeStrings.Length - 1; i++)
         {
            displayedHighscore = displayedHighscore + (i + 1) + ". Name  |  " + sortedTimeStrings[i] + "<br>";
         }
     
        textMesh.text = displayedHighscore;
    }

    private long ConvertToMilliseconds(string timeStr)
    {
        var Parts = timeStr.Split(":");
        if(Parts.Length != 4)
        {
            Debug.LogError("Invalid format of the time");
        }

        int hours = int.Parse(Parts[0]);
        int min = int.Parse(Parts[1]);
        int sec = int.Parse(Parts[2]);
        int milisec = int.Parse(Parts[3]);

        // Returning miliseconds to Order proparly the List
        return hours * 3600000 + min * 60000 + sec * 1000 + milisec;
    }


}
