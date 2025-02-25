using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class AchievementHandling : MonoBehaviour
{

    void Start()
    {
        // Sprawdzamy, czy Steam jest zainstalowany i zainicjowany
        ResetAchievements();
    }
    private void Update()
    {
        if (!SteamManager.Initialized) { return; }
        if( Input.GetKeyDown(KeyCode.Space)) {


            SteamUserStats.SetAchievement("Welcome");
            SteamUserStats.StoreStats();

        }
    }


    private void ResetAchievements()
    {
        SteamUserStats.ResetAllStats(true); // Resetuje statystyki + achievementy
        SteamUserStats.StoreStats(); // Wysy³a zmiany do Steam
        Debug.Log("Wszystkie achievementy zosta³y zresetowane!");
    }

}
