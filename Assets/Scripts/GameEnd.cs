using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private Animator endAnimation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SavesHandling.Instance.Save(true);
            GameManager.Instance.Victory();
            endAnimation.SetTrigger("Interact");
        }
    }
}
