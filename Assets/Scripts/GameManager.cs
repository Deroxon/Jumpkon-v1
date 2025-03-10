using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    // add colliders to the falling platform collider, make it same logic to disable collision from it, and if player stands on it, delete this gameobject from the game (czat podpowiada jak to zrobi?)

    // All section
    private int health;
    public bool isAlive;
    public bool isPaused = false;
    public bool isImmortal;
    public GameObject background, gameOverMenu, victoryMenu, mainMenu;

    public bool victoryGame;
    public GameObject menuVictoryGame;

    // Checkpoints section
    public Vector3Double checkpointposition;
    [SerializeField]
    private GameObject prefabCheckpoint, CheckpointLister;

    // positions of checkpoint of the double type
    /* List<Vector3Double> checkPointsList = new List<Vector3Double>
    {
    new Vector3Double(12.95, 33.52, 0),
    new Vector3Double(6.99, 79.53, 0),
    };
    */
    // All valids tags of enemies
    public List<string> EnemiesTags = new List<string>();

    public List<EnemyStructure> enemiesList = new List<EnemyStructure>();
    public bool initalisedEnemys = false;
    public int numberOfJumps;


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


    private void Awake()
    {
        SavesHandling.Instance.Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Generating GUI
        if (SceneManager.GetActiveScene().name == "GUI")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        initalizeEnemies(); // testing
        health = PlayerPrefs.GetInt("health", 10);
        menuVictoryGame.SetActive(false);
        isImmortal = false;
        isAlive = true;
        victoryGame = false;
        checkpointposition = new Vector3Double(-18, -3, 1);
        StartCoroutine(CheckIfPlayerIsNotInvicible());


        // Steam initialization of achievements terms
        if(!PlayerPrefs.HasKey("MariuszDeaths"))
        {
            PlayerPrefs.SetInt("MariuszDeaths", 0);
        }

        if(!PlayerPrefs.HasKey("Welcome"))
        {
            SavesHandling.Instance.SaveAchievementPrefs("Welcome");
        }

        if(PlayerPrefs.HasKey("NumberOfJumps"))
        {
            numberOfJumps = PlayerPrefs.GetInt("NumberOfJumps");
        } else
        {
            numberOfJumps = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && isAlive && !victoryMenu.activeInHierarchy)
        {
            PlayerPrefs.SetInt("NumberOfJumps", numberOfJumps);
            PlayerPrefs.Save();
            PauseMenu();
        }
    }
    public void AddHealth() => Health++;

    [ContextMenu("Minus Health")]
    public IEnumerator LoseHealth(int i)
    {
        if(Health == 10 && !PlayerPrefs.HasKey("First_death"))
        {
            AchievementHandling.Instance.setGameAchievement("First_death");
            PlayerPrefs.SetString("First_death", "true");
            PlayerPrefs.Save();
        }

        if(Health > 0 && !isImmortal)
        {
            PlayerPrefs.SetString("FinishHARD", "true"); // Setting this playerPref to avoid situation that user get the achievement for free

            StartCoroutine(PlayerMovement.Instance.AnimationDamage());
            isImmortal = true;
            // jumping after getting damage
            PlayerManager.Instance.playerRigidbody2D.velocity = new Vector2(PlayerManager.Instance.playerRigidbody2D.velocity.x, 16);
            Health = Health - i;
            AudioManager.Instance.PlaySFX("Hitdamage");

            // achievement section
            TimerScript.Instance.countTimeAchievement = 0;
            TimerScript.Instance.countHealth = Health;
            PlayerPrefs.SetInt("MariuszDeaths", PlayerPrefs.GetInt("MariuszDeaths") + 1);
            AchievementHandling.Instance.checkDeathAchievements();

            SavesHandling.Instance.Save();
            if (Health <= 0)
            {
                SavesHandling.Instance.DeleteSave();
                isAlive = false;
                yield return new WaitForSeconds(0.50f);
                Death();
            }
            // Start coroutine with a delay, it needs to be exactly 0.3 to make animation looks well and did not ruin the damage appliance from bullets
            yield return new WaitForSeconds(0.30f);
            backToCheckPoint();
            isImmortal = false;
        }
    }

    public void Victory()
    {
        if (victoryGame)
        {
            if(!PlayerPrefs.HasKey("FinishHARD"))
            {
                SavesHandling.Instance.SaveAchievementPrefs("FinishHARD");
            }
            TimerScript.Instance.SaveTime();
            SavesHandling.Instance.DeleteSave();
            AudioManager.Instance.StopAllAudio();
            AudioManager.Instance.PlaySFX("Finish");
            menuVictoryGame.SetActive(true);
            background.SetActive(true);
            PauseGame();
        } else
        {
            victoryMenu.SetActive(true);
            background.SetActive(true);
            PauseGame();
            AudioManager.Instance.PlaySFX("Finish");
            InGameMenu.Instance.SetLevelNameToDisplay();
            // if player win game, then we save his time
        }
    }
    public void setVictoryGame()
    {
        victoryGame = true;
    }


    public void Death()
    {
        gameOverMenu.SetActive(true);
        background.SetActive(true);
        AudioManager.Instance.PlaySFX("LoseAllHealths");
        PauseGame();
    }

    /* Not usable anymore
    public void SpawnCheckPoints()
    {

        for (int i = 0; i < checkPointsList.Count; i++)
        {
            // Quaternion - no rotation
            Instantiate(prefabCheckpoint, checkPointsList[i].ToVector3(), Quaternion.identity, CheckpointLister.transform);
        }

    }
    */

    public void setCheckPoint(Transform transform)
    {
        checkpointposition = new Vector3Double(transform.position.x, transform.position.y, 0);
    }


    public void backToCheckPoint()
    {
        if (isAlive)
        {
            PlayerManager.Instance.player.transform.position = checkpointposition.ToVector3();
        }
    }

     private void initalizeEnemies()
    {
            
        enemiesList.Add(
            new EnemyStructure(
                "Frog",
                0.5f,
                0.2f,
                4f,
                1f,
                0.2f));

        enemiesList.Add(
           new EnemyStructure(
               "Barrel",
               0.5f,
               2f,
               15f,
               1.3f,
               0.1f));

        enemiesList.Add(
          new EnemyStructure(
              "Cannon",
              0.5f,
              0.5f,
              3f,
              1f,
              0.4f));

        initalisedEnemys = true;

        if(enemiesList.Count > 0) { Debug.Log("Initialized"); }
        else { throw new System.Exception("The enemies were not initialized"); }
    }

    private void PauseMenu()
    {
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        background.SetActive(!background.activeInHierarchy);
        PauseGame();
    }

    public void PauseGame()
    {
        if(Time.timeScale == 1)
            Time.timeScale = 0;
        else if(Time.timeScale == 0)
            Time.timeScale = 1;
        isPaused = !isPaused;
    }

    // It needs to be in GameManager because it cause the issues when it is in PlatformScript
    public void InitializeSpawnPlatform(Vector3 positionToPass, GameObject objectToPass)
    {
        StartCoroutine(SpawnPlatform(positionToPass, objectToPass));
    }
    public IEnumerator SpawnPlatform(Vector3 position, GameObject obiektZniszczenia)
    {
        Destroy(obiektZniszczenia);
        yield return new WaitForSeconds(2f);

        GameObject respawningPlatform = Resources.Load<GameObject>("Prefabs/Traps/RespawningPlatform");
        Instantiate(respawningPlatform, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }

    // Safety function to avoid getting immortal by all the time
    private IEnumerator CheckIfPlayerIsNotInvicible()
    {
        while (true)
        {
            if(isImmortal)
            {
                isImmortal = !isImmortal;
            }
            yield return new WaitForSeconds(4f);
        }
    }

}
