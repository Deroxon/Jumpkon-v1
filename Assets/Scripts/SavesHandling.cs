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

    private void Update()
    {
    }

    public void Save(bool nextLevel = false)
    {
        Debug.Log("test");
        if (nextLevel)
            PlayerPrefs.SetInt("currentLevelID", SceneManager.GetActiveScene().buildIndex + 1);
        else
            PlayerPrefs.SetInt("currentLevelID", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("saveExists", 1);
        PlayerPrefs.SetInt("health", GameManager.Instance.Health);
        PlayerPrefs.SetFloat("timer", TimerScript.Instance.GetTime());
        PlayerPrefs.SetFloat("spawnPositionX", GameManager.Instance.checkpointposition.ToVector3().x);
        PlayerPrefs.SetFloat("spawnPositionY", GameManager.Instance.checkpointposition.ToVector3().y);
        PlayerPrefs.SetFloat("spawnPositionZ", GameManager.Instance.checkpointposition.ToVector3().z);
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
        PlayerPrefs.DeleteKey("currentLevelID");
        PlayerPrefs.DeleteKey("health");
        PlayerPrefs.DeleteKey("timer");
        PlayerPrefs.DeleteKey("spawnPositionX");
        PlayerPrefs.DeleteKey("spawnPositionY");
        PlayerPrefs.DeleteKey("spawnPositionZ");
    }
}
