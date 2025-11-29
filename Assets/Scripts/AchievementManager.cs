using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public List<Achievement> achievements;
    public GameObject achievementPopup;
    public UnityEngine.UI.Text popupText;

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

    public void UnlockAchievement(string title)
    {
        Achievement ach = achievements.Find(a => a.title == title);
        if (ach != null && !ach.unlocked)
        {
            ach.unlocked = true;
            ShowPopup(ach.title);
            Debug.Log("Achievement unlocked: " + title);
        }
    }

    private void ShowPopup(string title)
    {
        if (achievementPopup != null && popupText != null)
        {
            popupText.text = "Achievement Unlocked: " + title;
            achievementPopup.SetActive(true);
            Invoke("HidePopup", 3f);
        }
    }

    private void HidePopup()
    {
        achievementPopup.SetActive(false);
    }
}
