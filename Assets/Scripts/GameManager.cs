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
   
    private Vector3 checkpoinposition;

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
        checkpoinposition = new Vector3(-18, -3, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Minus Health")]
    public IEnumerator LoseHealth(int i)
    {
        if(Health > 0)
        {
            Health = Health - i;

            // Start coroutine with a delay  
            StartCoroutine(backToCheckPoint());
            yield return new WaitForSeconds(0.1f);
        }
        

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


    public void setCheckPoint()
    {
        // there would be needed the script which set the position from the GameObject "checkpoint"
    }


    public IEnumerator backToCheckPoint()
    {
        // there is need to make the animation of losing health
        yield return new WaitForSeconds(0.1f);
        PlayerManager.Instance.player.transform.position = checkpoinposition;
        
    }

}
