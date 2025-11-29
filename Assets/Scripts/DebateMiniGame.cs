using UnityEngine;
using UnityEngine.UI;

public class DebateMiniGame : MonoBehaviour
{
    public Text debateText;
    public Button[] choiceButtons;
    public GameObject debatePanel;

    private Debate currentDebate;
    private System.Action<bool> onDebateEnd;

    void Start()
    {
        debatePanel.SetActive(false);
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[i].onClick.AddListener(() => MakeChoice(index));
        }
    }

    public void StartDebate(Debate debate, System.Action<bool> callback)
    {
        currentDebate = debate;
        onDebateEnd = callback;
        debateText.text = debate.question;
        for (int i = 0; i < choiceButtons.Length && i < debate.choices.Length; i++)
        {
            choiceButtons[i].GetComponentInChildren<Text>().text = debate.choices[i];
        }
        debatePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void MakeChoice(int choiceIndex)
    {
        bool win = choiceIndex == currentDebate.correctChoice;
        debatePanel.SetActive(false);
        Time.timeScale = 1f;
        onDebateEnd?.Invoke(win);

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            if (win)
            {
                player.ModifyApproval(5f);
                player.ModifyInfluence(2f);
                if (AchievementManager.Instance != null)
                {
                    AchievementManager.Instance.UnlockAchievement("Debate Champion");
                }
            }
            else
            {
                player.ModifyApproval(-3f);
            }
        }
    }
}

[System.Serializable]
public class Debate
{
    public string question;
    public string[] choices;
    public int correctChoice;
}
