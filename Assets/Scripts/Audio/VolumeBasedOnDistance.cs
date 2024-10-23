using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeBasedOnDistance : MonoBehaviour
{
    private GameObject player;

    private AudioSource audioSource;
    public AudioMixer audioMixer;

    private float maxDistance = 20f;
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
        audioMixer = Resources.Load<AudioMixer>("Sounds/AudioMixer");
        // setting AudioMixer group for the AudioMixer
        AudioMixerGroup[] foundGroups = audioMixer.FindMatchingGroups("MonsterSFX");
        audioSource.outputAudioMixerGroup = foundGroups[0];
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
