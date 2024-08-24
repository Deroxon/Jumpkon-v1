using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeBasedOnDistance : MonoBehaviour
{
    private GameObject player;

    private AudioSource audioSource;

    private float maxDistance = 30f;
    private float minDistance = 2f;


    void Start()
    {
        player = PlayerManager.Instance.player;
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogError("There is no audio source for this");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance <= maxDistance)
        {
           
            float volume = Mathf.Clamp01(1 - (distance - minDistance) / (maxDistance - minDistance));
            audioSource.volume = volume;
        } else
        {
            audioSource.volume = 0;
        }
        
    }
}
