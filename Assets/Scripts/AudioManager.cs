using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Audio clips (assign in inspector)
    public AudioClip[] musicTracks;
    public AudioClip gunshotClip;
    public AudioClip carEngineClip;
    public AudioClip footstepsClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(0); // Play first track
    }

    public void PlayMusic(int trackIndex)
    {
        if (musicTracks.Length > trackIndex && musicSource != null)
        {
            musicSource.clip = musicTracks[trackIndex];
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayGunshot()
    {
        PlaySFX(gunshotClip);
    }

    public void PlayCarEngine()
    {
        PlaySFX(carEngineClip);
    }

    public void PlayFootsteps()
    {
        PlaySFX(footstepsClip);
    }
}