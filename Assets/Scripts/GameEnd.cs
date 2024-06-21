using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private Animator endAnimation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Victory();
            GameManager.Instance.isAlive = false;
            endAnimation.SetTrigger("Interact");
        }
    }
}
