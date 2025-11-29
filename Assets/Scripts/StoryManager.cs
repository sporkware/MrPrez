using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    public enum Act { Act1, Act2, Act3 }
    public Act currentAct = Act.Act1;

    private PlayerController player;
    private QuestManager questManager;

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
        player = FindObjectOfType<PlayerController>();
        questManager = FindObjectOfType<QuestManager>();
        StartAct1();
    }

    void Update()
    {
        CheckActProgression();
    }

    void CheckActProgression()
    {
        if (currentAct == Act.Act1 && player.approvalRating >= 70f && player.influence >= 60f)
        {
            StartAct2();
        }
        else if (currentAct == Act.Act2 && player.corruptionLevel >= 50f)
        {
            StartAct3();
        }
        else if (currentAct == Act.Act3)
        {
            // Check for end game
            if (player.approvalRating <= 10f)
            {
                EndGame(false); // Impeached
            }
            else if (player.corruptionLevel >= 90f)
            {
                EndGame(true); // Dictator
            }
            else if (player.approvalRating >= 90f && player.influence >= 90f)
            {
                EndGame(true); // Re-elected
            }
        }
    }

    void StartAct1()
    {
        currentAct = Act.Act1;
        Debug.Log("Starting Act 1: Ascension");
        // Assign initial quests
        if (questManager != null && questManager.mainQuests.Count > 0)
        {
            questManager.StartQuest(questManager.mainQuests[0]);
        }
    }

    void StartAct2()
    {
        currentAct = Act.Act2;
        Debug.Log("Starting Act 2: Corruption");
        // Trigger conspiracies, tougher choices
        RandomEventManager.Instance.TriggerRandomEvent(); // Example
    }

    void StartAct3()
    {
        currentAct = Act.Act3;
        Debug.Log("Starting Act 3: Downfall");
        // Impeachment proceedings
        player.ModifyApproval(-20f);
        // Final quest
    }

    public void EndGame(bool victory)
    {
        Debug.Log(victory ? "Player wins!" : "Player loses!");
        // Show end screen
    }
}