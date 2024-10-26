using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private Animator endAnimation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SavesHandling.Instance.Save(true);
            endAnimation.SetTrigger("Interact");
            string LevelName = SceneManager.GetActiveScene().name;
            char lastString = LevelName[LevelName.Length - 1];
            string lastStringinString = lastString.ToString();
  
            if (lastStringinString == "9")
            {
                GameManager.Instance.setVictoryGame();

            }
                GameManager.Instance.Victory();
        }
    }
}
