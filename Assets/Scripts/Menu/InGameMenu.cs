using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameMenu : MonoBehaviour
{
    public void RestartButton()
    {
        GameManager.Instance.victoryMenu.SetActive(!GameManager.Instance.victoryMenu.activeInHierarchy);
        GameManager.Instance.background.SetActive(!GameManager.Instance.background.activeInHierarchy);
        GameManager.Instance.PauseGame();
        GameManager.Instance.backToCheckPoint();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.PauseGame();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
