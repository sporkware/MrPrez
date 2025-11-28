using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public float mouseSensitivity = 1f;
    public int graphicsQuality = 2; // 0 low, 1 medium, 2 high

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        PlayerPrefs.SetInt("GraphicsQuality", graphicsQuality);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        graphicsQuality = PlayerPrefs.GetInt("GraphicsQuality", 2);
        ApplySettings();
    }

    public void ApplySettings()
    {
        // Apply to AudioManager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.musicSource.volume = musicVolume * masterVolume;
            AudioManager.Instance.sfxSource.volume = sfxVolume * masterVolume;
        }
        // Apply graphics
        QualitySettings.SetQualityLevel(graphicsQuality);
        // Apply sensitivity to player
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.rotationSpeed = mouseSensitivity * 100f; // Example
        }
    }
}