using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : Singleton<TimerScript>
{
    private TextMeshProUGUI textMesh;
    public float currentTime;
    private string currentTimeInString;
    private bool isRunning =true;
    public int countHealth;
    public float countTimeAchievement;

    //public static TimerScript Instance;
    // Start is called before the first frame update

    private void Awake()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void Start()
    {
        countHealth = GameManager.Instance.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            countTimeAchievement += Time.deltaTime; // for only achievemnt purpose
            UpdateTimeDisplayed(currentTime);
            checkTimeAchievements();
        }
    }

    public void StartStoper() => isRunning = true;
    public void StopStoper() => isRunning = false;
    public void ResetStoper()
    {
        currentTime = 0;
        StopStoper();
    }


    private void UpdateTimeDisplayed(float time)
    {
        float hours = Mathf.FloorToInt(time / 3600);
        float minutes= Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        // we taking the current time and from this math we are returning the decimal of number for example 125.678 seconds, we taking 0.678 and multiple it by 1000
        float miliseconds = Mathf.FloorToInt((time % 1) * 1000);

        currentTimeInString = string.Format("{0:0}:{1:00}:{2:00}:{3:000}", hours, minutes, seconds, miliseconds);
        textMesh.text = currentTimeInString;
    }

    [ContextMenu("SaveTime")]
    public void SaveTime(string userName  = "Player")
    {
        DataBaseManager.Instance.SaveHighScore(userName, currentTimeInString);
        

        // Steam achievemnt handling
        if(!PlayerPrefs.HasKey("Finish"))
        {
            AchievementHandling.Instance.setGameAchievement("Finish");
        }
        if(!PlayerPrefs.HasKey("Finish1h") && currentTime < 3600)
        {
            AchievementHandling.Instance.setGameAchievement("Finish1h");
        }
        if (!PlayerPrefs.HasKey("Finish30") && currentTime < 1800)
        {
            AchievementHandling.Instance.setGameAchievement("Finish30");
        }


    }

    // Only for debbugging
    [ContextMenu("Clear Highscores")]
    public void ClearPlayerPrefTimes()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    public float GetTime()
    {
        return currentTime;
    }


    public void checkTimeAchievements()
    {
        if (countTimeAchievement > 300 && countHealth >= GameManager.Instance.Health && !PlayerPrefs.HasKey("Invincible"))
        {
            SavesHandling.Instance.SaveAchievementPrefs("Invincible");
        }
    }
}