using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private int health;
    public bool isAlive;
    public GameObject backgroundGameOver;

    [SerializeField]
    private Text healthTxt;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            this.health = value;
            this.healthTxt.text = "x" + value.ToString();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Minus Health")]
    public void LoseHealth(int i)
    {
        Health = Health - i;

        if (Health <= 0)
        {
            isAlive = false;
            Death();
        }

    }

    public void Death()
    {
        backgroundGameOver.SetActive(true);

    }

    // i think we need to change location of this function, also function for itself, for now it is only for testing
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);
    }
}
// zrób aby dzia³a³ przycisk restart tam coœ trzeba zrobiæ z funkcj¹