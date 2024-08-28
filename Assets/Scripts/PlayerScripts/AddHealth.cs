using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddHealth();
            Destroy(gameObject);
            AudioManager.Instance.PlaySFX("CollectCoin");
        }
    }
}
