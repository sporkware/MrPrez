using UnityEngine;

public class EasterEggManager : MonoBehaviour
{
    public static EasterEggManager Instance { get; private set; }

    private bool secretDiscovered = false;

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

    public void DiscoverSecret()
    {
        if (!secretDiscovered)
        {
            secretDiscovered = true;
            Debug.Log("Easter Egg Found: Secret Presidential Bunker!");
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.ModifyInfluence(10f);
                player.ModifyWealth(1000f);
            }
            if (AchievementManager.Instance != null)
            {
                AchievementManager.Instance.UnlockAchievement("Secret Explorer");
            }
        }
    }

    // Call this from hidden objects
    public void TriggerEasterEgg(string type)
    {
        switch (type)
        {
            case "Bunker":
                DiscoverSecret();
                break;
            case "Alien":
                Debug.Log("Easter Egg: Alien Encounter!");
                // Add alien dialogue or event
                break;
            default:
                Debug.Log("Unknown Easter Egg");
                break;
        }
    }
}