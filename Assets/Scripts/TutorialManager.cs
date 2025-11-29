using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public GameObject tutorialPanel;
    public UnityEngine.UI.Text tutorialText;

    private int tutorialStep = 0;
    private string[] tutorialMessages = {
        "Welcome to American President! Use WASD to move.",
        "Press F near NPCs to interact.",
        "Drive vehicles with W/S, steer with A/D.",
        "Complete quests to progress.",
        "Watch your approval rating!"
    };

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
        if (!PlayerPrefs.HasKey("TutorialCompleted"))
        {
            ShowTutorial();
        }
    }

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = tutorialMessages[tutorialStep];
    }

    public void NextStep()
    {
        tutorialStep++;
        if (tutorialStep < tutorialMessages.Length)
        {
            tutorialText.text = tutorialMessages[tutorialStep];
        }
        else
        {
            EndTutorial();
        }
    }

    public void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("TutorialCompleted", 1);
    }
}
