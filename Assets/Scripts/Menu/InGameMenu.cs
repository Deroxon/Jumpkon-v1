using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameMenu : Singleton<InGameMenu>
{
    private char lastString;
    private GameObject VictoryTxt;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isAlive)
        {
            PlayAgain();
        }
    }

    public void NextLevel()
    {
        GameManager.Instance.victoryMenu.SetActive(false);
        GameManager.Instance.background.SetActive(false);
        GameManager.Instance.backToCheckPoint();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.PauseGame();

    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.PauseGame();
    }

    public void PlayAgain()
    {
        PlayerPrefs.SetInt("PlayAgain", 1);
        PlayerPrefs.Save();
        GameManager.Instance.PauseGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void SetLevelNameToDisplay()
    {
        VictoryTxt = GameObject.Find("VictoryTxt");
        string LevelName = SceneManager.GetActiveScene().name;
        lastString = LevelName[LevelName.Length - 1];
        VictoryTxt.GetComponent<TextMeshProUGUI>().text = "Level  " + lastString + " completed";

    }
}
