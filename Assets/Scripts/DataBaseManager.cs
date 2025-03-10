using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using Unity.VisualScripting.FullSerializer;


public class DataBaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;
    public static DataBaseManager Instance;
    public List<HighScore> highScoreList;

    public float cooldownTime = 2f;  // Na przyk�ad 5 sekund cooldownu

    private DateTime lastRequestTime = DateTime.MinValue;

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
        string configFilePath = "config"; // Plik config.json w Resources
        TextAsset configTextAsset = Resources.Load<TextAsset>(configFilePath);

        if (configTextAsset != null)
        {
            Config config = JsonUtility.FromJson<Config>(configTextAsset.text);

            FirebaseApp.Create(new AppOptions()
            {
                ApiKey = config.ApiKey,
                DatabaseUrl = new System.Uri(config.DatabaseUrl),
                ProjectId = config.ProjectId,
                StorageBucket = config.ProjectId,
                AppId = config.AppId
            });

            // Po��cz si� z Realtime Database
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

            Debug.Log("Firebase initialized successfully!");
            CheckPossibilitytoFetch();
        }
        else
        {
            Debug.LogError("Couldn't load firebase configuration");
        }


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

    public void CheckPossibilitytoFetch()
    {
        TimeSpan timeSinceLastRequest = DateTime.UtcNow - lastRequestTime;

        if (timeSinceLastRequest.TotalSeconds >= cooldownTime)
        {
            // Je�li min�� cooldown, wykonujemy zapytanie
            GetHighScores();

            // Ustawiamy czas ostatniego zapytania na teraz
            lastRequestTime = DateTime.UtcNow;
        }
        else
        {
            // Je�li u�ytkownik pr�buje wywo�a� funkcj� zbyt szybko, wy�wietlamy komunikat
            Debug.Log("Musisz poczeka�, aby ponownie pobra� wyniki.");
        }
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
       // Debug.Log("savingPlayerPrefs");
        string swapArrayToString = "";

        PlayerPrefs.DeleteKey("Highscores");

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

        PlayerPrefs.SetString("Highscores", swapArrayToString);
        PlayerPrefs.Save();

        GameObject grabHighScoreUI = GameObject.Find("HighScoreMenu");

        if(grabHighScoreUI != null)
        {
                grabHighScoreUI.transform.GetChild(1).GetComponent<HighScores>().LoadHighscoresUI();
        }
        yield return new WaitForSeconds(0.5f);
    }


        [System.Serializable]
        public class Config
        {
        public string ApiKey;
        public string DatabaseUrl;
        public string ProjectId;
        public string StorageBucket;
        public string AppId;
        }

}
