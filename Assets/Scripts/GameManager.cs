using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerController player;
    public QuestManager questManager;
    public UIManager uiManager;
    public WorldGenerator worldGenerator;

    public enum GameState { Menu, Playing, Paused, GameOver }
    public GameState currentState;

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
        currentState = GameState.Playing;
        InitializeGame();
    }

    void InitializeGame()
    {
        // Find or create essential components
        player = FindObjectOfType<PlayerController>();
        questManager = FindObjectOfType<QuestManager>();
        uiManager = FindObjectOfType<UIManager>();
        worldGenerator = FindObjectOfType<WorldGenerator>();

        // Start main quest
        if (questManager != null && questManager.mainQuests.Count > 0)
        {
            questManager.StartQuest(questManager.mainQuests[0]);
        }
    }

    void Update()
    {
        HandleGameState();
    }

    void HandleGameState()
    {
        switch (currentState)
        {
            case GameState.Playing:
                // Normal gameplay
                break;
            case GameState.Paused:
                // Pause logic
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                // Game over logic
                break;
        }
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        // Show game over screen
    }

    public void SaveGame()
    {
        // Save player stats, quest progress, etc.
        PlayerPrefs.SetFloat("ApprovalRating", player.approvalRating);
        PlayerPrefs.SetFloat("Influence", player.influence);
        PlayerPrefs.SetFloat("Wealth", player.wealth);
        PlayerPrefs.SetFloat("Corruption", player.corruptionLevel);
        PlayerPrefs.SetString("SavedGame", "true");
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        // Load saved data
        player.approvalRating = PlayerPrefs.GetFloat("ApprovalRating", 50f);
        player.influence = PlayerPrefs.GetFloat("Influence", 50f);
        player.wealth = PlayerPrefs.GetFloat("Wealth", 1000f);
        player.corruptionLevel = PlayerPrefs.GetFloat("Corruption", 0f);
    }
}