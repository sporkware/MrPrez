using UnityEngine;
using UnityEngine.UI;

public class CardGameMiniGame : MonoBehaviour
{
    public Text gameText;
    public Button[] actionButtons;
    public GameObject cardGamePanel;

    private int playerScore = 0;
    private int opponentScore = 0;
    private int rounds = 3;
    private int currentRound = 0;
    private int playerWins = 0;
    private int opponentWins = 0;
    private System.Action<bool> onGameEnd;

    void Start()
    {
        cardGamePanel.SetActive(false);
        actionButtons[0].onClick.AddListener(() => PlayCard("Hit"));
        actionButtons[1].onClick.AddListener(() => PlayCard("Stand"));
    }

    public void StartGame(System.Action<bool> callback)
    {
        onGameEnd = callback;
        currentRound = 0;
        playerWins = 0;
        opponentWins = 0;
        StartRound();
        cardGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void StartRound()
    {
        currentRound++;
        playerScore = 0;
        opponentScore = Random.Range(15, 22);
        gameText.text = "Round " + currentRound + ". Your turn. Opponent has " + opponentScore + " points.";
    }

    private void PlayCard(string action)
    {
        if (action == "Hit")
        {
            int card = Random.Range(1, 11);
            playerScore += card;
            gameText.text = "Round " + currentRound + ". You drew " + card + ". Total: " + playerScore;
            if (playerScore > 21)
            {
                opponentWins++;
                if (currentRound < rounds)
                {
                    StartRound();
                }
                else
                {
                    EndGame(playerWins > opponentWins);
                }
            }
        }
        else if (action == "Stand")
        {
            if (playerScore > opponentScore || opponentScore > 21)
            {
                playerWins++;
            }
            else
            {
                opponentWins++;
            }
            if (currentRound < rounds)
            {
                StartRound();
            }
            else
            {
                EndGame(playerWins > opponentWins);
            }
        }
    }

    private void EndGame(bool win)
    {
        cardGamePanel.SetActive(false);
        Time.timeScale = 1f;
        onGameEnd?.Invoke(win);

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            if (win)
            {
                player.ModifyWealth(500f * playerWins);
                player.ModifyInfluence(playerWins);
                if (AchievementManager.Instance != null)
                {
                    AchievementManager.Instance.UnlockAchievement("Card Shark");
                }
            }
            else
            {
                player.ModifyWealth(-200f * opponentWins);
            }
        }
    }
}
