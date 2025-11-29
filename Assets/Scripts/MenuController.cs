using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button newGameButton;
    public Button loadGameButton;
    public Button settingsButton;
    public Button quitButton;

    void Start()
    {
        newGameButton.onClick.AddListener(StartNewGame);
        loadGameButton.onClick.AddListener(LoadGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartNewGame()
    {
        // Reset player stats and load game world
        PlayerPrefs.DeleteAll(); // Clear saved data
        SceneManager.LoadScene("GameWorld");
    }

    void LoadGame()
    {
        // Load saved game data
        if (PlayerPrefs.HasKey("SavedGame"))
        {
            SceneManager.LoadScene("GameWorld");
            // Load player position, stats, etc.
        }
        else
        {
            Debug.Log("No saved game found");
        }
    }

    void OpenSettings()
    {
        // Open settings menu (implement UI panel)
        Debug.Log("Settings menu not implemented yet");
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
