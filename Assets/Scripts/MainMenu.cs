using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ResolutionButton;
    [SerializeField] private TextMeshProUGUI ModeButton;
    void Start()
    {
        DataManager.Instance.resolutions = Screen.resolutions;
        DataManager.Instance.resolutionsCount = DataManager.Instance.resolutions.Length;
        ChangeResolution();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextResolution()
    {
        if (DataManager.Instance.currentResolution < DataManager.Instance.resolutionsCount -1)
            DataManager.Instance.currentResolution ++;
        else
            DataManager.Instance.currentResolution = 0;
        ChangeResolution();
    }

    public void PreviousResolution()
    {
        if (DataManager.Instance.currentResolution > 0)
            DataManager.Instance.currentResolution --;
        else
            DataManager.Instance.currentResolution = DataManager.Instance.resolutionsCount -1;
        ChangeResolution();
    }

    public void NextMode()
    {
        if (DataManager.Instance.currentMode < 2)
            DataManager.Instance.currentMode++;
        else DataManager.Instance.currentMode = 0;
        ChangeResolution();
    }
    public void PreviousMode()
    {
        if (DataManager.Instance.currentMode > 0)
            DataManager.Instance.currentMode--;
        else
            DataManager.Instance.currentMode = 2;
        ChangeResolution();
    }

    public void ChangeResolution()
    {
        if (DataManager.Instance.currentMode == 0)
        {
            Screen.SetResolution(DataManager.Instance.resolutions[DataManager.Instance.currentResolution].width, DataManager.Instance.resolutions[DataManager.Instance.currentResolution].height, FullScreenMode.FullScreenWindow);
            ModeButton.text = "BORDERLESS";
        }
        else if (DataManager.Instance.currentMode == 1)
        {
            Screen.SetResolution(DataManager.Instance.resolutions[DataManager.Instance.currentResolution].width, DataManager.Instance.resolutions[DataManager.Instance.currentResolution].height, FullScreenMode.ExclusiveFullScreen);
            ModeButton.text = "FULLSCREEN";
        }
        else if (DataManager.Instance.currentMode == 2)
        {
            Screen.SetResolution(DataManager.Instance.resolutions[DataManager.Instance.currentResolution].width, DataManager.Instance.resolutions[DataManager.Instance.currentResolution].height, FullScreenMode.Windowed);
            ModeButton.text = "WINDOWED";
        }
        ResolutionButton.text = DataManager.Instance.resolutions[DataManager.Instance.currentResolution].width + "x" + DataManager.Instance.resolutions[DataManager.Instance.currentResolution].height;
    }

}
