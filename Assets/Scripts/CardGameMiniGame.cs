using UnityEngine;
using UnityEngine.UI;

public class CardGameMiniGame : MonoBehaviour
{
    public Text gameText;
    public Button[] actionButtons;
    public GameObject cardGamePanel;

    private int playerScore = 0;
    private int opponentScore = 0;
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
        playerScore = 0;
        opponentScore = Random.Range(15, 22);
        gameText.text = "Your turn. Opponent has " + opponentScore + " points.";
        cardGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void PlayCard(string action)
    {
        if (action == "Hit")
        {
            int card = Random.Range(1, 11);
            playerScore += card;
            gameText.text = "You drew " + card + ". Total: " + playerScore;
            if (playerScore > 21)
            {
                EndGame(false);
            }
        }
        else if (action == "Stand")
        {
            if (playerScore > opponentScore || opponentScore > 21)
            {
                EndGame(true);
            }
            else
            {
                EndGame(false);
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
                player.ModifyWealth(500f);
                player.ModifyInfluence(1f);
            }
            else
            {
                player.ModifyWealth(-200f);
            }
        }
    }
}