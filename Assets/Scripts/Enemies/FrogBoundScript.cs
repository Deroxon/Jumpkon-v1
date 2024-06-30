using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBoundScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D platform)
    {
        if (platform.CompareTag("Frog"))
        {
           StartCoroutine(FrogScript.Instance.Bounce());
        }
    }
}
