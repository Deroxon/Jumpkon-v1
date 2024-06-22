using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    [SerializeField] private BoxCollider2D platform;
    [SerializeField] private MovingPlatforms other;
    private void OnTriggerEnter2D(Collider2D platform)
    {
        if (platform.CompareTag("Platform"))
        {
            other.Bounce();
        }
    }
}
