using UnityEngine;
using UnityEngine.UI;

public class NewsSystem : MonoBehaviour
{
    public Text newsText;
    public GameObject newsPanel;

    private string[] newsHeadlines = {
        "President's Approval Rating Soars!",
        "Scandal Rocks White House!",
        "New Policy Passed Amid Controversy",
        "Economic Boom Predicted",
        "International Tensions Rise"
    };

    void Start()
    {
        newsPanel.SetActive(false);
        InvokeRepeating("BroadcastNews", 120f, 300f); // Every 2-5 minutes
    }

    private void BroadcastNews()
    {
        string headline = newsHeadlines[Random.Range(0, newsHeadlines.Length)];
        newsText.text = "Breaking News: " + headline;
        newsPanel.SetActive(true);
        Invoke("HideNews", 10f);

        // Affect player based on news
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            if (headline.Contains("Approval"))
            {
                player.ModifyApproval(Random.Range(-5f, 5f));
            }
            else if (headline.Contains("Scandal"))
            {
                player.ModifyApproval(-10f);
                player.ModifyCorruption(2f);
            }
        }
    }

    private void HideNews()
    {
        newsPanel.SetActive(false);
    }
}