using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;
    public static DataBaseManager Instance;
    public List<HighScore> highScoreList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FirebaseApp.Create(new AppOptions()
        {
            ApiKey = "AIzaSyDu6Yxf1QkJHxJrMMWSuxljHVWQUUmWX7c",
            DatabaseUrl = new System.Uri("https://mariusz-the-game-default-rtdb.europe-west1.firebasedatabase.app"),
            ProjectId = "mariusz-the-game",
            StorageBucket = "mariusz-the-game.firebasestorage.app",
            AppId = "1:974350079432:web:8a4b2780993bf3334fdcfd",
        });

        // Po³¹cz siê z Realtime Database
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log("Firebase initialized successfully!");
        GetHighScores();
    }

    private void Update()
    {

    }

    public void SaveHighScore(string playerName, string time)
        {
            string key = databaseReference.Child("highscores").Push().Key;
            HighScore highScore = new HighScore(playerName, time);

            databaseReference.Child("highscores").Child(key).SetValueAsync(highScore.ToDictionary())
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("High score saved successfully!");
                    }
                    else
                    {
                        Debug.LogError("Failed to save high score: " + task.Exception);
                    }
                });
        }


   
    public void GetHighScores()
    {

        highScoreList = new List<HighScore>();

        databaseReference.Child("highscores").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    // Take all results
                    foreach (var childSnapshot in snapshot.Children)
                    {
                        string playerName = childSnapshot.Child("playerName").Value.ToString();
                        string time = childSnapshot.Child("time").Value.ToString();

                        // We create highscore element
                        HighScore highScore = new HighScore(playerName, time);
                        highScoreList.Add(highScore);

                        // Display them in console
                        if (highScoreList.Count > 0)
                        {
                            Debug.Log("Player: " + highScore.playerName + ", Score: " + highScore.time);
                        }
                    }
                    StartCoroutine(saveHighScoresToPlayerPrefs());

                }
                else
                {
                    Debug.Log("No high scores available.");
                }
            }
            else
            {
                Debug.LogError("Failed to fetch high scores: " + task.Exception);
            }
        });
        
    }


    public IEnumerator saveHighScoresToPlayerPrefs()
    {

        string swapArrayToString = "";
        yield return new WaitForSeconds(2f);

        foreach (HighScore highscore in highScoreList)
        {
            string record = highscore.playerName + ";" + highscore.time;

            //  checking is string is not null or empty
            if (!string.IsNullOrEmpty(swapArrayToString))
            {
                swapArrayToString += "|"; // Adding separator
            }

            swapArrayToString += record;
        }

        Debug.Log("saved " + swapArrayToString);
        PlayerPrefs.SetString("Highscores", swapArrayToString);
        PlayerPrefs.Save();

    }


}
