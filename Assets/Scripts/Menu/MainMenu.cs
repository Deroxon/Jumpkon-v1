using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System;
using UnityEditor;

public class MainMenu : Singleton<MainMenu>
{
    [SerializeField] private TextMeshProUGUI ResolutionButton;
    [SerializeField] private TextMeshProUGUI ModeButton;
    [SerializeField] GameObject ContinueButton;
    //0 is borderless, 1 is fullscreen, 2 is windowed
    private float currentMode;
    private Resolution[] resolutions;
    private int resolutionsCount;
    private float currentResolution;
    public LocalizedString localizedString;


    void Start()
    {
        //Getting all supported resolutions without duplicates, skipping framerate
        var temp = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct();
        resolutions = temp.ToArray();
        resolutionsCount = resolutions.Length;
        LoadSettings();
        ModeButtonRender();
        ResolutionButton.text = resolutions[(int)currentResolution].width + "x" + resolutions[(int)currentResolution].height;

        localizedString.TableReference = "Game_Strings";
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

    [ContextMenu("Load")]
    public void LoadTxt()
    {
        LoadSettings();
        Debug.Log("Loading");
        Debug.Log(currentResolution);
        Debug.Log(currentMode);

        ResolutionButton.text = resolutions[(int)currentResolution].width + "x" + resolutions[(int)currentResolution].height;
        ModeButtonRender();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        SceneManager.LoadScene("thanks");
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
        ResolutionButton.text = resolutions[(int)currentResolution].width + "x" + resolutions[(int)currentResolution].height;
    }

    public void PreviousResolution()
    {
        if (currentResolution > 0)
            currentResolution --;
        else
            currentResolution = resolutionsCount -1;
        ResolutionButton.text = resolutions[(int)currentResolution].width + "x" + resolutions[(int)currentResolution].height;
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
        {
            localizedString.TableEntryReference = "Borderless";
            localizedString.StringChanged += UpdateText;
            localizedString.RefreshString();
        }
        else if (currentMode == 1)
        {
            localizedString.TableEntryReference = "Full_screen";
            localizedString.StringChanged += UpdateText;
            localizedString.RefreshString();
        }
        else if (currentMode == 2)
        {
            localizedString.TableEntryReference = "Windowed";
            localizedString.StringChanged += UpdateText;
            localizedString.RefreshString();
        }
        else 
            ModeButton.text = "error";
    }
    void UpdateText(string value)
    {
        ModeButton.text = value;  // Zaktualizuj tekst przycisku
    }

    public void ChangeResolution()
    {
        //0 is borderless, 1 is fullscreen, 2 is windowed
        if (currentMode == 0)
            Screen.SetResolution(resolutions[(int)currentResolution].width, resolutions[(int)currentResolution].height, FullScreenMode.FullScreenWindow);
        else if (currentMode == 1)
            Screen.SetResolution(resolutions[(int)currentResolution].width, resolutions[(int)currentResolution].height, FullScreenMode.ExclusiveFullScreen);
        else if (currentMode == 2)
            Screen.SetResolution(resolutions[(int)currentResolution].width, resolutions[(int)currentResolution].height, FullScreenMode.Windowed);
        ResolutionButton.text = resolutions[(int)currentResolution].width + "x" + resolutions[(int)currentResolution].height;
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
        currentResolution = PlayerPrefs.GetFloat("resolution");
        currentMode = PlayerPrefs.GetFloat("screenMode");
    }

    public void SaveSettings(KeyValuePair<string, float>[] ItemsToSave)
    {
        
        foreach(var pair in ItemsToSave)
        {
            PlayerPrefs.SetFloat(pair.Key, pair.Value);
            Debug.Log(pair.Key);
            Debug.Log(pair.Value);
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
