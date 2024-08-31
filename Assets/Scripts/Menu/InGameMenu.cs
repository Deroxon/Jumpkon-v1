using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameMenu : MonoBehaviour
{
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
}
