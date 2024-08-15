using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.PauseGame();
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.PauseGame();
    }
}
