using UnityEngine;
using System.Collections.Generic;

public class RomanceManager : MonoBehaviour
{
    public static RomanceManager Instance { get; private set; }

    public Dictionary<NPCController, float> relationships = new Dictionary<NPCController, float>();

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

    public void ModifyRelationship(NPCController npc, float amount)
    {
        if (!relationships.ContainsKey(npc))
        {
            relationships[npc] = 0f;
        }
        relationships[npc] = Mathf.Clamp(relationships[npc] + amount, -100f, 100f);
        CheckAffair(npc);
    }

    private void CheckAffair(NPCController npc)
    {
        if (relationships[npc] >= 50f && !npc.isInAffair)
        {
            StartAffair(npc);
        }
    }

    private void StartAffair(NPCController npc)
    {
        npc.isInAffair = true;
        Debug.Log("Affair started with " + npc.npcType);
        // Consequences: risk of scandal
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyCorruption(10f);
        }
        // Schedule scandal event
        Invoke("TriggerScandal", Random.Range(60f, 300f)); // 1-5 minutes
    }

    private void TriggerScandal()
    {
        Debug.Log("Scandal broke out!");
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyApproval(-20f);
            player.ModifyCorruption(5f);
        }
        // End affairs or something
    }
}

// Add to NPCController
public partial class NPCController
{
    public bool isInAffair = false;
}