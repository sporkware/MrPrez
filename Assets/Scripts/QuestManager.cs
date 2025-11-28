using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public List<Quest> mainQuests;
    public List<Quest> sideQuests;
    public Quest currentQuest;

    void Start()
    {
        // Initialize quests based on GDD
        InitializeQuests();
    }

    void InitializeQuests()
    {
        // Main storyline quests
        mainQuests = new List<Quest>
        {
            new Quest("Ascension", "Rise to power and deal with initial scandals", QuestType.Main),
            new Quest("Corruption", "Navigate conspiracies and make tough choices", QuestType.Main),
            new Quest("Downfall", "Face impeachment and decide the nation's fate", QuestType.Main)
        };

        // Side missions
        sideQuests = new List<Quest>
        {
            new Quest("Rally Speech", "Deliver a speech to gain public support", QuestType.Side),
            new Quest("Business Negotiation", "Negotiate a deal with corporate lobbyists", QuestType.Side),
            new Quest("Diplomatic Meeting", "Host a state dinner for foreign diplomats", QuestType.Side),
            new Quest("Covert Operation", "Eliminate a political rival discreetly", QuestType.Side),
            new Quest("Scandalous Affair", "Navigate a romantic scandal with a celebrity (adult humor)", QuestType.Side),
            new Quest("Casino Night", "Win big at a high-stakes poker game with politicians", QuestType.Side),
            new Quest("Undercover Journalist", "Seduce and extract information from a reporter", QuestType.Side),
            new Quest("Penthouse Party", "Attend an exclusive party with questionable activities", QuestType.Side)
        };
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
    }

    public void UpdateQuestProgress(Quest quest, string objective)
    {
        // Update specific objective progress
        // Implementation depends on objective type
    }
}

[System.Serializable]
public class Quest
{
    public string title;
    public string description;
    public QuestType type;
    public bool isActive;
    public bool isCompleted;
    public float approvalReward;
    public float influenceReward;
    public float wealthReward;

    public Quest(string title, string description, QuestType type)
    {
        this.title = title;
        this.description = description;
        this.type = type;
        this.isActive = false;
        this.isCompleted = false;
        this.approvalReward = 10f; // Default rewards
        this.influenceReward = 5f;
        this.wealthReward = 100f;
    }
}

public enum QuestType
{
    Main,
    Side
}