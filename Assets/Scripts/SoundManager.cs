﻿using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    public AudioClip player_attack;
    public AudioClip player_death;
    public AudioClip player_hit;
    public AudioClip player_jump;
    public AudioClip demon_attack;
    public AudioClip flyingknight_attack;
    public AudioClip skeleton_attack;
    public AudioClip dash;
    public AudioClip extra_damage;
    public AudioClip health_potion;
    public AudioClip invincibility;
    public AudioClip second_jump;
    public AudioClip slime_attack;
    public AudioClip Slime_jump;
    public AudioClip enemy_death;


    private AudioSource soundEffectAudio;
    // Start is called before the first frame update
    void Start()
    {
        // This is a singleton that makes sure you only
        // ever have one Sound Manager
        // If there is any other Sound Manager created destroy it
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        AudioSource theSource = GetComponent<AudioSource>();
        soundEffectAudio = theSource;
    }
    public void playSound(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
