using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{ 
    private Rigidbody2D platform;
    //initial platforms speed
    [SerializeField] private float platformSpeedHorizontal = 0f;
    [SerializeField] private float platformSpeedVertical = 0f;
    private AudioSource audioSource;

    void Start()
    {
        platform = this.gameObject.GetComponent<Rigidbody2D>();
        platform.velocity = new Vector2(platformSpeedHorizontal, platformSpeedVertical);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
    }
    public void Bounce()
    {
        platform.velocity = new Vector2(platformSpeedHorizontal *= -1f, platformSpeedVertical *= -1f);
    }

}
