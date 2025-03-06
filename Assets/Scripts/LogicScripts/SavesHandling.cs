using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavesHandling : Singleton<SavesHandling>
{
    public static new SavesHandling Instance;
    public bool saveExists;
    public bool saveLoaded = false;
    //List of keys cleared by function DeleteSave()
    List<string> keys = new List<string>()
    {
        "currentLevelID",
        "health",
        "timer",
        "spawnPositionX",
        "spawnPositionY",
        "spawnPositionZ",
        "FinishHARD",

        // steam commented but leaving for testing
        // "NumberOfJumps",
        // "Welcome",
        // "First_death",
        // "Invincible",
        // "First_blood",
        // "Finish",
        // "Finish1h",
        // "Finish30",
        // "MariuszDeaths",
        // "Hard",
        // "Hard1",
        // "Hard2"

    };
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        saveExists = PlayerPrefs.GetInt("saveExists") == 1 ? true : false;
    }

    private void Start()
    {
    }

    public void Save(bool nextLevel = false)
    {
        if (nextLevel)
        {
            PlayerPrefs.SetInt("currentLevelID", SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            PlayerPrefs.SetInt("currentLevelID", SceneManager.GetActiveScene().buildIndex);
        }
        PlayerPrefs.SetInt("saveExists", 1);
        PlayerPrefs.SetInt("NumberOfJumps", GameManager.Instance.numberOfJumps);
        PlayerPrefs.SetInt("health", GameManager.Instance.Health);
        PlayerPrefs.SetFloat("timer", TimerScript.Instance.GetTime());
        PlayerPrefs.SetFloat("spawnPositionX", GameManager.Instance.checkpointposition.ToVector3().x);
        PlayerPrefs.SetFloat("spawnPositionY", GameManager.Instance.checkpointposition.ToVector3().y);
        PlayerPrefs.SetFloat("spawnPositionZ", GameManager.Instance.checkpointposition.ToVector3().z);
        PlayerPrefs.Save();
    }

    // player prefs without clearing
    /// <summary>
    /// Steam Achievements:
    /// - Welcome
    /// - First_death
    /// - Invincible
    /// - First_blood
    /// - Finish
    /// - Finish1h
    /// - Finish30
    /// - Hard
    /// - Hard1
    /// - Hard2
    /// - MariuszDeaths
    /// </summary>
    /// 

    // Player prefs with clearing important!
    ///
    /// - FinishHARD
    public void SaveAchievementPrefs(string achievement_name)
    {
        AchievementHandling.Instance.setGameAchievement(achievement_name);
        PlayerPrefs.SetString(achievement_name, "true");
        PlayerPrefs.Save();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        saveExists = PlayerPrefs.GetInt("saveExists") == 1 ? true : false;
        if (saveExists)
        {
            GameManager.Instance.Health = PlayerPrefs.GetInt("health");
            SceneManager.LoadScene(PlayerPrefs.GetInt("currentLevelID"));
            TimerScript.Instance.currentTime = PlayerPrefs.GetFloat("timer");
        }
    }

    [ContextMenu("Clear")]
    public void DeleteSave()
    {
        PlayerPrefs.SetInt("saveExists", 0);
        saveExists = false;
        foreach(var key in keys)
        {
            PlayerPrefs.DeleteKey(key);
        }
        
    }
}
