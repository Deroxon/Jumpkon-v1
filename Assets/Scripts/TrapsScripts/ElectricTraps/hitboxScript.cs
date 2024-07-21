using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GameManager.Instance.LoseHealth(1));
        }
        
    }
}
