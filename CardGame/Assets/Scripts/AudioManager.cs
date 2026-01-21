using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip backgroundMusic;
    public AudioClip notEnoughMana;


    private static AudioManager instance;
    public AudioSource audioSource;
    public AudioSource audioSourceCards;
    public static AudioManager Instance
    {
        get 
        { 
            if (instance == null)
            {

            }
            return instance; 
        }
    }

    private void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        PlaySound(backgroundMusic);
    }

    public void PlaySound (AudioClip clip)
    {
       audioSource.clip = clip;
       audioSource.Play();
    }

    public void PlayOnce(AudioSource location, AudioClip clip)
    {
        location.clip = clip;
        location.PlayOneShot(clip);
    }
}
