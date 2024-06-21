using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    [SerializeField] private BoxCollider2D platform;
    private void OnTriggerEnter2D(Collider2D platform)
    {
        if (platform.CompareTag("Platform"))
        {
            Debug.Log("hit");
            MovingPlatforms.Instance.Bound();
        }
    }
}
