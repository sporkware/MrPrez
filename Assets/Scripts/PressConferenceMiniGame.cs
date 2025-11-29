using UnityEngine;
using UnityEngine.UI;

public class PressConferenceMiniGame : MonoBehaviour
{
    public Text questionText;
    public Button[] choiceButtons;
    public GameObject conferencePanel;

    private PressConference currentConference;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private System.Action<int> onConferenceEnd;

    void Start()
    {
        conferencePanel.SetActive(false);
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[i].onClick.AddListener(() => AnswerQuestion(index));
        }
    }

    public void StartConference(PressConference conference, System.Action<int> callback)
    {
        currentConference = conference;
        onConferenceEnd = callback;
        currentQuestionIndex = 0;
        correctAnswers = 0;
        ShowNextQuestion();
        conferencePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ShowNextQuestion()
    {
        if (currentQuestionIndex < currentConference.questions.Length)
        {
            Question q = currentConference.questions[currentQuestionIndex];
            questionText.text = q.questionText;
            for (int i = 0; i < choiceButtons.Length && i < q.choices.Length; i++)
            {
                choiceButtons[i].GetComponentInChildren<Text>().text = q.choices[i];
            }
        }
        else
        {
            EndConference();
        }
    }

    private void AnswerQuestion(int choiceIndex)
    {
        if (choiceIndex == currentConference.questions[currentQuestionIndex].correctChoice)
        {
            correctAnswers++;
        }
        currentQuestionIndex++;
        ShowNextQuestion();
    }

    private void EndConference()
    {
        conferencePanel.SetActive(false);
        Time.timeScale = 1f;
        onConferenceEnd?.Invoke(correctAnswers);

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            float score = (float)correctAnswers / currentConference.questions.Length;
            player.ModifyApproval(score * 10f);
            player.ModifyInfluence(score * 5f);
            if (score >= 1f && AchievementManager.Instance != null)
            {
                AchievementManager.Instance.UnlockAchievement("Press Master");
            }
        }
    }
}

[System.Serializable]
public class PressConference
{
    public Question[] questions;
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] choices;
    public int correctChoice;
}