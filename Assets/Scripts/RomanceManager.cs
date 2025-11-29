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
        foreach (var kvp in relationships)
        {
            if (kvp.Value >= 50f)
            {
                kvp.Key.isInAffair = false;
                relationships[kvp.Key] = Mathf.Max(0f, kvp.Value - 30f);
            }
        }
    }

    public void FlirtWithNPC(NPCController npc)
    {
        float charmBonus = FindObjectOfType<PlayerController>().charm / 100f;
        float successChance = 0.5f + charmBonus;
        if (Random.value < successChance)
        {
            ModifyRelationship(npc, 10f);
            Debug.Log("Flirt successful with " + npc.npcType);
        }
        else
        {
            ModifyRelationship(npc, -5f);
            Debug.Log("Flirt failed with " + npc.npcType);
        }
    }

    public void GiveGift(NPCController npc, Item gift)
    {
        InventoryManager im = InventoryManager.Instance;
        if (im.HasItem(gift.itemName))
        {
            im.RemoveItem(gift);
            ModifyRelationship(npc, gift.value);
            Debug.Log("Gave gift to " + npc.npcType + ", relationship increased by " + gift.value);
        }
    }

    public void GoOnDate(NPCController npc)
    {
        if (relationships.ContainsKey(npc) && relationships[npc] >= 30f)
        {
            ModifyRelationship(npc, 15f);
            PlayerController player = FindObjectOfType<PlayerController>();
            player.ModifyWealth(-100f); // Date cost
            Debug.Log("Went on date with " + npc.npcType);
        }
        else
        {
            Debug.Log("Not close enough for a date with " + npc.npcType);
        }
    }
}

// Add to NPCController
public partial class NPCController
{
    public bool isInAffair = false;
}