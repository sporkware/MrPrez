using UnityEngine;
using System.Collections.Generic;

public class RandomEventManager : MonoBehaviour
{
    public static RandomEventManager Instance { get; private set; }

    public List<RandomEvent> events;

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
        InvokeRepeating("TriggerRandomEvent", 600f, 1200f); // Every 10-20 minutes
    }

    private void TriggerRandomEvent()
    {
        if (events.Count == 0) return;

        RandomEvent evt = events[Random.Range(0, events.Count)];
        Debug.Log("Random Event: " + evt.eventName);
        evt.Execute();

        // Show notification
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.ShowNotification(evt.eventName + ": " + evt.description);
        }
    }
}

[System.Serializable]
public class RandomEvent
{
    public string eventName;
    public string description;

    public void Execute()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            // Example effects
            if (eventName.Contains("Protest"))
            {
                player.ModifyApproval(-5f);
            }
            else if (eventName.Contains("Donation"))
            {
                player.ModifyWealth(1000f);
            }
            // Add more logic
        }
    }
}
