using UnityEngine.Audio;
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
    public AudioClip slime_jump;
    public AudioClip enemy_death;
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip shop_music;
    public AudioClip titlescreen_music;
    AudioSource theSource;

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

        theSource = GetComponent<AudioSource>();
        soundEffectAudio = theSource;
    }
    public void playSound(AudioClip clip)
    {
        Debug.Log(clip == null);
        theSource.PlayOneShot(clip, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
