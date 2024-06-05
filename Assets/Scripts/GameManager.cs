using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    // All section
    private int health;
    public bool isAlive;
    public GameObject backgroundGameOver;


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
        if(Health > 0)
        {
            Health = Health - i;

            if(Health <= 0)
            {
                isAlive = false;
                Death();
            }

            // Start coroutine with a delay  
            StartCoroutine(backToCheckPoint());
            yield return new WaitForSeconds(0.1f);
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
            // there is need to make the animation of losing health
            yield return new WaitForSeconds(0.1f);
            PlayerManager.Instance.player.transform.position = checkpointposition.ToVector3();
        }
        
    }

}
