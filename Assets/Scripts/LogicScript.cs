using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private GameObject victroryScreen;
    public void GenerateGround()
    {




    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void victory()
    {
        victroryScreen.SetActive(true);
    }

}
