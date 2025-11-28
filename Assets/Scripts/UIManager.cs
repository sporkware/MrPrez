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

    public GameObject pauseMenu;
    public GameObject hud;

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
}