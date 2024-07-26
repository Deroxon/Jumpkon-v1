using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !GameManager.Instance.isImmortal)
        {
            StartCoroutine(PlayerMovement.Instance.AnimationDamage());
            StartCoroutine(GameManager.Instance.LoseHealth(1));
        }
    }
}

