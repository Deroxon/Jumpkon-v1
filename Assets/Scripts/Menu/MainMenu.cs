using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System;

public class MainMenu : Singleton<MainMenu>
{
    [SerializeField] private TextMeshProUGUI ResolutionButton;
    [SerializeField] private TextMeshProUGUI ModeButton;
    [SerializeField] GameObject ContinueButton;
    //0 is borderless, 1 is fullscreen, 2 is windowed
    private int currentMode;
    private Resolution[] resolutions;
    private int resolutionsCount;
    private int currentResolution;


    void Start()
    {
        //Getting all supported resolutions without duplicates, skipping framerate
        var temp = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct();
        resolutions = temp.ToArray();
        resolutionsCount = resolutions.Length;
        LoadSettings();
        ModeButtonRender();
        ResolutionButton.text = resolutions[currentResolution].width + "x" + resolutions[currentResolution].height;
        // Reseting GUI and Game Manager
        if (DontDestroy.Instance != null) DontDestroy.Instance.menu();
        ContinueButtonVisibility();
        if(PlayerPrefs.GetInt("PlayAgain") == 1)
        {
            PlayerPrefs.SetInt("PlayAgain", 0);
            PlayerPrefs.Save();
            PlayGame();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
  
    public void NextResolution()
    {
        if (currentResolution < resolutionsCount -1)
            currentResolution ++;
        else
            currentResolution = 0;
        ResolutionButton.text = resolutions[currentResolution].width + "x" + resolutions[currentResolution].height;
    }

    public void PreviousResolution()
    {
        if (currentResolution > 0)
            currentResolution --;
        else
            currentResolution = resolutionsCount -1;
        ResolutionButton.text = resolutions[currentResolution].width + "x" + resolutions[currentResolution].height;
    }

    public void NextMode()
    {
        if (currentMode < 2)
            currentMode++;
        else 
            currentMode = 0;
        ModeButtonRender();
    }
    public void PreviousMode()
    {
        if (currentMode > 0)
            currentMode--;
        else
            currentMode = 2;
        ModeButtonRender();
    }
    public void ModeButtonRender()
    {
        if (currentMode == 0)
            ModeButton.text = "BORDERLESS";
        else if (currentMode == 1)
            ModeButton.text = "FULLSCREEN";
        else if (currentMode == 2)
            ModeButton.text = "WINDOWED";
        else 
            ModeButton.text = "error";
    }

    public void ChangeResolution()
    {
        //0 is borderless, 1 is fullscreen, 2 is windowed
        if (currentMode == 0)
            Screen.SetResolution(resolutions[currentResolution].width, resolutions[currentResolution].height, FullScreenMode.FullScreenWindow);
        else if (currentMode == 1)
            Screen.SetResolution(resolutions[currentResolution].width, resolutions[currentResolution].height, FullScreenMode.ExclusiveFullScreen);
        else if (currentMode == 2)
            Screen.SetResolution(resolutions[currentResolution].width, resolutions[currentResolution].height, FullScreenMode.Windowed);
        ResolutionButton.text = resolutions[currentResolution].width + "x" + resolutions[currentResolution].height;
        ModeButtonRender();

        var savingValues = new KeyValuePair<string, float>[]
        {
            new KeyValuePair<string, float>("resolution", currentResolution),
            new KeyValuePair<string, float>("screenMode", currentMode)
        };

        SaveSettings(savingValues);
    }
    
    public void LoadSettings()
    {
        currentResolution = PlayerPrefs.GetInt("resolution", resolutionsCount - 1);
        currentMode = PlayerPrefs.GetInt("screenMode", 0);
    }

    public void SaveSettings(KeyValuePair<string, float>[] ItemsToSave)
    {
        
        foreach(var pair in ItemsToSave)
        {
            PlayerPrefs.SetFloat(pair.Key, pair.Value);
        }
        PlayerPrefs.Save();
    }

    public void ResetSettings()
    {
        PlayerPrefs.DeleteKey("resolution");
        PlayerPrefs.DeleteKey("screenMode");
        LoadSettings();
        ChangeResolution();
    }

    public void ContinueButtonVisibility()
    {
        if (PlayerPrefs.GetInt("saveExists") == 1 ? true : false)
        {
            ContinueButton.SetActive(true);
        }
        else
        {
            ContinueButton.SetActive(false);
        }
    }
    public void DeleteSave()
    {
        SavesHandling.Instance.DeleteSave();
    }
}
