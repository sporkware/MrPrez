using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public List<Quest> mainQuests;
    public List<Quest> sideQuests;
    public Quest currentQuest;
    private static bool firstQuest = false;

    void Start()
    {
        // Initialize quests based on GDD
        InitializeQuests();
    }

    void InitializeQuests()
    {
        // Load quests from Resources/Quests folder
        // For now, assume assigned in inspector or create manually
        // mainQuests and sideQuests should be assigned in Unity
    }

    public void StartQuest(Quest quest)
    {
        currentQuest = quest;
        quest.isActive = true;
        // Trigger quest start events
        Debug.Log("Started quest: " + quest.title);
    }

    public void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        quest.isActive = false;
        // Award rewards
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyApproval(quest.approvalReward);
            player.ModifyInfluence(quest.influenceReward);
            player.ModifyWealth(quest.wealthReward);
        }
        Debug.Log("Completed quest: " + quest.title);

        // Achievement
        if (!firstQuest && AchievementManager.Instance != null)
        {
            firstQuest = true;
            AchievementManager.Instance.UnlockAchievement("First Quest Completed");
        }
    }

    public void CheckQuestCompletion(Quest quest)
    {
        if (quest.objectives.All(obj => obj.isCompleted))
        {
            CompleteQuest(quest);
        }
    }

    public void UpdateQuestProgress(Quest quest, string objectiveDescription)
    {
        foreach (QuestObjective obj in quest.objectives)
        {
            if (obj.description == objectiveDescription)
            {
                obj.isCompleted = true;
                CheckQuestCompletion(quest);
                break;
            }
        }
    }
}

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public QuestType type;
    public float approvalReward;
    public float influenceReward;
    public float wealthReward;
    public QuestObjective[] objectives;

    [System.NonSerialized] public bool isActive;
    [System.NonSerialized] public bool isCompleted;
}

public enum QuestType
{
    Main,
    Side
}

[System.Serializable]
public class QuestObjective
{
    public string description;
    public ObjectiveType type;
    public int requiredAmount;
    public int currentAmount;

    [System.NonSerialized] public bool isCompleted;

    public void UpdateProgress(int amount = 1)
    {
        currentAmount += amount;
        if (currentAmount >= requiredAmount)
        {
            isCompleted = true;
        }
    }
}

public enum ObjectiveType
{
    Kill,
    Collect,
    Deliver,
    Dialogue,
    Location
}
