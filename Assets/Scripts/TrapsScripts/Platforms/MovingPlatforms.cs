using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : Singleton<MovingPlatforms>
{ 
    [SerializeField] private Rigidbody2D platform;
    [SerializeField] private float platformSpeedHorizontal = 0f;
    [SerializeField] private float platformSpeedVertical = 0f;

    void Update()
    {
        platform.velocity = new Vector2(platformSpeedHorizontal, platformSpeedVertical);
    }

    public void Bound()
    {
        platformSpeedHorizontal *= -1f;
        platformSpeedVertical *= -1f;
    }
}
