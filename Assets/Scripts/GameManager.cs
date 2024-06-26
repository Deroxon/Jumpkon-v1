using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    // add colliders to the falling platform collider, make it same logic to disable collision from it, and if player stands on it, delete this gameobject from the game (czat podpowiada jak to zrobi�)

    // All section
    private int health;
    public bool isAlive;
    private bool isImmortal;
    public GameObject backgroundGameOver;
    [SerializeField] private GameObject backgroundVictory;


    // Checkpoints section
    private Vector3Double checkpointposition;
    [SerializeField]
    private GameObject prefabCheckpoint, CheckpointLister;

    // positions of checkpoint of the double type
    List<Vector3Double> checkPointsList = new List<Vector3Double>
    {
    new Vector3Double(12.95, 33.52, 0),
    new Vector3Double(6.99, 79.53, 0),
    };


    // Health section
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
        isImmortal = false;
        isAlive = true;
        checkpointposition = new Vector3Double(-18, -3, 1);
        SpawnCheckPoints();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Minus Health")]
    public IEnumerator LoseHealth(int i)
    {
        if(Health > 0 && !isImmortal)
        {
            isImmortal = true;
            // jumping after getting damage
            PlayerManager.Instance.playerRigidbody2D.velocity = new Vector2(PlayerManager.Instance.playerRigidbody2D.velocity.x, 12);
            Health = Health - i;

            if(Health <= 0)
            {
                isAlive = false;
                Death();
            }

            // Start coroutine with a delay  
            StartCoroutine(backToCheckPoint());
            yield return new WaitForSeconds(0.5f);
            isImmortal = false;
        }
        

    }

    public void Victory()
    {
        backgroundVictory.SetActive(true);
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


    public void SpawnCheckPoints()
    {

        for (int i = 0; i < checkPointsList.Count; i++)
        {
            // Quaternion - no rotation
            Instantiate(prefabCheckpoint, checkPointsList[i].ToVector3(), Quaternion.identity, CheckpointLister.transform);
        }

    }


    public void setCheckPoint(Transform transform)
    {
        checkpointposition = new Vector3Double(transform.position.x, transform.position.y, 0);
    }


    public IEnumerator backToCheckPoint()
    {
        if (isAlive)
        {
            yield return new WaitForSeconds(0.5f);
            PlayerManager.Instance.player.transform.position = checkpointposition.ToVector3();
        }
        
    }

}
