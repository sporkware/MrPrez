using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HackingMiniGame : MonoBehaviour
{
    public Text codeText;
    public InputField inputField;
    public Button submitButton;
    public GameObject hackingPanel;
    public float timeLimit = BalanceConstants.HackingTimeLimit;

    private string correctCode;
    private float timeRemaining;
    private System.Action<bool> onHackEnd;

    void Start()
    {
        hackingPanel.SetActive(false);
        submitButton.onClick.AddListener(SubmitCode);
    }

    public void StartHack(string code, System.Action<bool> callback)
    {
        correctCode = code;
        onHackEnd = callback;
        timeRemaining = timeLimit;
        codeText.text = "Hack the system! Enter code: " + ScrambleCode(code);
        inputField.text = "";
        hackingPanel.SetActive(true);
        Time.timeScale = 0f;
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    private string ScrambleCode(string code)
    {
        // Simple scramble for display
        return new string(code.ToCharArray().OrderBy(c => Random.value).ToArray());
    }

    void UpdateTimer()
    {
        timeRemaining -= 1f;
        if (timeRemaining <= 0f)
        {
            EndHack(false);
        }
    }

    void SubmitCode()
    {
        bool success = inputField.text == correctCode;
        EndHack(success);
    }

    private void EndHack(bool success)
    {
        CancelInvoke("UpdateTimer");
        hackingPanel.SetActive(false);
        Time.timeScale = 1f;
        onHackEnd?.Invoke(success);

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            if (success)
            {
                player.ModifyInfluence(3f);
                if (AchievementManager.Instance != null)
                {
                    AchievementManager.Instance.UnlockAchievement("Master Hacker");
                }
            }
            else
            {
                player.ModifyApproval(-2f);
            }
        }
    }
}