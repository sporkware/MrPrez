using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<Item> inventory = new List<Item>();
    public int maxSlots = 20;

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

    public bool AddItem(Item item)
    {
        if (inventory.Count < maxSlots)
        {
            inventory.Add(item);
            Debug.Log("Added " + item.itemName + " to inventory");
            return true;
        }
        Debug.Log("Inventory full!");
        return false;
    }

    public void RemoveItem(Item item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log("Removed " + item.itemName + " from inventory");
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.Exists(item => item.itemName == itemName);
    }

    public Item GetItem(string itemName)
    {
        return inventory.Find(item => item.itemName == itemName);
    }

    public void UseItem(Item item)
    {
        // Implement item usage logic
        Debug.Log("Used " + item.itemName);
        // Apply effects, e.g., modify player stats
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyApproval(item.approvalEffect);
            player.ModifyInfluence(item.influenceEffect);
            player.ModifyCorruption(item.corruptionEffect);
        }
        // Remove consumable items
        if (item.consumable)
        {
            RemoveItem(item);
        }
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public Sprite icon;
    public bool consumable;
    public float approvalEffect;
    public float influenceEffect;
    public float corruptionEffect;
    public float value; // For romance gifts
}