using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float currentTime;
    private string currentTimeInString;
    private bool isRunning =true;

    public static TimerScript Instance;
    // Start is called before the first frame update

    // needs to be deleted after merge
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

    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimeDisplayed(currentTime);
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
    public void SaveTime()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            string currentStringsInHistory = PlayerPrefs.GetString("HighScore", "");
            PlayerPrefs.SetString("HighScore", (currentStringsInHistory + " W " + currentTimeInString));
        } else
        {
            PlayerPrefs.SetString("HighScore", currentTimeInString);
        }
        
    }

    [ContextMenu("GetTime")]

    public string[] GetTimes()
    {
        string[] arrayStrings = PlayerPrefs.GetString("HighScore", "").Replace(" ","").Split("W");

        return arrayStrings;  
    }

    // Only for debbugging
    [ContextMenu("Clear Highscores")]
    public void ClearPlayerPrefTimes()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}