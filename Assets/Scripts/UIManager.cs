using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text approvalText;
    public Text influenceText;
    public Text wealthText;
    public Text corruptionText;
    public Slider approvalSlider;
    public Slider influenceSlider;
    public Slider corruptionSlider;
    public Slider healthSlider;
    public Text healthText;

    public GameObject pauseMenu;
    public GameObject hud;
    public GameObject questPanel;
    public Text questListText;
    public GameObject inventoryPanel;
    public Text inventoryText;
    public GameObject settingsPanel;

    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestPanel();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleSettingsPanel();
        }
    }

    void UpdateUI()
    {
        if (player != null)
        {
            approvalText.text = "Approval: " + player.approvalRating.ToString("F1") + "%";
            influenceText.text = "Influence: " + player.influence.ToString("F1");
            wealthText.text = "Wealth: $" + player.wealth.ToString("F0");
            corruptionText.text = "Corruption: " + player.corruptionLevel.ToString("F1") + "%";

            approvalSlider.value = player.approvalRating / 100f;
            influenceSlider.value = player.influence / 100f;
            corruptionSlider.value = player.corruptionLevel / 100f;

            healthText.text = "Health: " + player.GetHealthPercentage().ToString("P0");
            healthSlider.value = player.GetHealthPercentage();
        }

        UpdateQuestUI();
        UpdateInventoryUI();
    }

    void UpdateQuestUI()
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        if (questManager != null && questListText != null)
        {
            string questText = "Active Quests:\n";
            foreach (Quest quest in questManager.mainQuests)
            {
                if (quest.isActive)
                {
                    questText += quest.title + "\n";
                }
            }
            foreach (Quest quest in questManager.sideQuests)
            {
                if (quest.isActive)
                {
                    questText += quest.title + "\n";
                }
            }
            questListText.text = questText;
        }
    }

    void UpdateInventoryUI()
    {
        // Placeholder for inventory items
        if (inventoryText != null)
        {
            inventoryText.text = "Inventory:\n- Presidential Seal\n- Speech Notes\n- Secret Documents";
        }
    }

    void TogglePauseMenu()
    {
        bool isPaused = pauseMenu.activeSelf;
        pauseMenu.SetActive(!isPaused);
        hud.SetActive(isPaused);
        Time.timeScale = isPaused ? 1f : 0f;
    }

    public void ResumeGame()
    {
        TogglePauseMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ToggleQuestPanel()
    {
        questPanel.SetActive(!questPanel.activeSelf);
    }

    void ToggleInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
