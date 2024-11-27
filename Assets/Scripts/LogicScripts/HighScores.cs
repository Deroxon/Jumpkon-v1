using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GameObject.Find("HighScoreList").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        LoadHighscoresUI();
    }


    [ContextMenu("Load Highscores")]
    public void LoadHighscoresUI()
    {
        // getting all Highcorse, make sure there are no empty space and split them
        List<HighScore> GetAllHighscores = new List<HighScore>();

        string savedData = PlayerPrefs.GetString("Highscores", "");

        if(!string.IsNullOrEmpty(savedData))
        {
            string[] records = savedData.Split('|');


            foreach(string record in records)
            {
                string[] fields = record.Split(';');

                if (fields.Length ==2)
                {
                    string playerName = fields[0];
                    string time = fields[1];

                    HighScore highscore = new HighScore(playerName, time);
                    GetAllHighscores.Add(highscore);
                }
            }

        }
        // just in case we don't have any HighScore
        if (GetAllHighscores.Count == 0)
        {
            Debug.LogWarning("No high scores available.");
            textMesh.text = "";
            return;
        }

        string displayedHighscore = "";


        // Sorting List by Converting values to Miliseconds
         var sortedTimeStrings = GetAllHighscores.OrderBy(timeStr => ConvertToMilliseconds(timeStr.time)).ToArray();

        // taking first 10 places
        sortedTimeStrings = sortedTimeStrings.Take(10).ToArray();


        for (int i = 0; i < sortedTimeStrings.Length; i++)
        {
            string playerName = sortedTimeStrings[i].playerName;
            string time = sortedTimeStrings[i].time;

            if (i == 9)
            {
                displayedHighscore = displayedHighscore + (i + 1) + ". " + playerName + " |  " + time + "<br>";
                break;
            }
            displayedHighscore = displayedHighscore + (i + 1) + ". " + playerName + " |  "+ time + "<br>";
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
