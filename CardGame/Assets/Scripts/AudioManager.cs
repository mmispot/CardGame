using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip backgroundMusic;


    private static AudioManager instance;
    public AudioSource audioSource;
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
}
