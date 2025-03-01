using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementHandling : Singleton<AchievementHandling>
{
    private List<string> pendingAchievements = new List<string>();

    public new static AchievementHandling Instance;
    private class AchievementList
    {
        public List<string> achievements;
        public AchievementList(List<string> list) { achievements = list; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
        void Start()
    {
        LoadPendingAchievements();
        CheckAndUnlockOfflineAchievements();
    }
    private void Update()
    {
        if (IsSteamOnline())
        {
            CheckAndUnlockOfflineAchievements();
        }
    }

    public void setGameAchievement(string achivementName)
    {
        if(SteamManager.Initialized)
        {

            if(IsSteamOnline())
            {
                bool achievementUnlocked = IsAchievementUnlocked(achivementName);
                if (!achievementUnlocked)
                {
                    SteamUserStats.SetAchievement(achivementName);
                    SteamUserStats.StoreStats();
                    Debug.Log($"This Achievement unlocked {achivementName}");
                }
                else
                {
                    SavePendingAchievement(achivementName);
                    Debug.Log("User is already offline, saving values for later");
                }
            } 

            else
            {
                Debug.Log($"This achievement is already unlocked {achivementName}");
            }
        }
       
    }

    [ContextMenu("clear achievements")]
    private void ResetAchievements()
    {
        SteamUserStats.ResetAllStats(true); // Resetuje statystyki + achievementy
        SteamUserStats.StoreStats(); // Wysy³a zmiany do Steam
        Debug.Log("Wszystkie achievementy zosta³y zresetowane!");
    }

    private bool IsAchievementUnlocked(string achivementName)
    {
        SteamUserStats.GetAchievement(achivementName, out bool isUnlocked);
        return isUnlocked;
    }

    private bool IsSteamOnline()
    {
        return SteamManager.Initialized && SteamUser.BLoggedOn();
    }

    void SavePendingAchievement(string achivementName)
    {
        if (!pendingAchievements.Contains(achivementName))
        {
            pendingAchievements.Add(achivementName);
            PlayerPrefs.SetString("PendingAchievements", JsonUtility.ToJson(new AchievementList(pendingAchievements)));
            PlayerPrefs.Save();
        }
    }

    void LoadPendingAchievements()
    {
        if (PlayerPrefs.HasKey("PendingAchievements"))
        {
            string json = PlayerPrefs.GetString("PendingAchievements");
            AchievementList savedData = JsonUtility.FromJson<AchievementList>(json);
            pendingAchievements = savedData.achievements ?? new List<string>();
        }
    }

    void CheckAndUnlockOfflineAchievements()
    {
        if (pendingAchievements.Count > 0)
        {
            Debug.Log("Odblokowujê zaleg³e achievementy...");
            List<string> achievementsToRemove = new List<string>();

            foreach (string achivementName in pendingAchievements)
            {
                setGameAchievement(achivementName);
                achievementsToRemove.Add(achivementName);
            }

            // Usuñ odblokowane achievementy z listy
            foreach (string achivementName in achievementsToRemove)
            {
                pendingAchievements.Remove(achivementName);
            }

            // Zapisz zaktualizowan¹ listê
            PlayerPrefs.SetString("PendingAchievements", JsonUtility.ToJson(new AchievementList(pendingAchievements)));
            PlayerPrefs.Save();
        }
    }


    public void checkDeathAchievements()
    {
        int deaths = PlayerPrefs.GetInt("MariuszDeaths");

        if(deaths >= 10 && !PlayerPrefs.HasKey("Hard"))
        {
            SavesHandling.Instance.SaveAchievementPrefs("Hard");
        }
        if (deaths >= 100 && !PlayerPrefs.HasKey("Hard1"))
        {
            SavesHandling.Instance.SaveAchievementPrefs("Hard1");
        }
        if (deaths >= 1000 && !PlayerPrefs.HasKey("Hard2"))
        {
            SavesHandling.Instance.SaveAchievementPrefs("Hard2");
        }

    }

}
