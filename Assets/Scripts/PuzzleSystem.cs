using UnityEngine;
using UnityEngine.UI;

public class PuzzleSystem : MonoBehaviour
{
    public GameObject puzzlePanel;
    public Text puzzleText;
    public InputField answerInput;
    public Button submitButton;
    public Button hintButton;
    public Text hintText;

    private Puzzle currentPuzzle;
    private System.Action<bool> onPuzzleComplete;

    void Start()
    {
        puzzlePanel.SetActive(false);
        submitButton.onClick.AddListener(SubmitAnswer);
        hintButton.onClick.AddListener(ShowHint);
        hintText.gameObject.SetActive(false);
    }

    public void StartPuzzle(Puzzle puzzle, System.Action<bool> callback)
    {
        currentPuzzle = puzzle;
        onPuzzleComplete = callback;
        puzzleText.text = puzzle.question;
        answerInput.text = "";
        hintText.gameObject.SetActive(false);
        puzzlePanel.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    void SubmitAnswer()
    {
        string answer = answerInput.text.ToLower().Trim();
        bool correct = answer == currentPuzzle.answer.ToLower();
        puzzlePanel.SetActive(false);
        Time.timeScale = 1f;
        onPuzzleComplete?.Invoke(correct);

        if (correct)
        {
            Debug.Log("Puzzle solved!");
            // Reward player
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.ModifyApproval(5f);
                player.ModifyInfluence(2f);
            }
        }
        else
        {
            Debug.Log("Wrong answer!");
            // Penalty
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.ModifyApproval(-2f);
            }
        }
    }

    void ShowHint()
    {
        hintText.text = "Hint: " + currentPuzzle.hint;
        hintText.gameObject.SetActive(true);
        // Optional: penalty for using hint
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyApproval(-1f);
        }
    }
}

[System.Serializable]
public class Puzzle
{
    public string question;
    public string answer;
    public string hint;
}