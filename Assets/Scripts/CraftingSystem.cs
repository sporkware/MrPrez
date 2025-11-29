using UnityEngine;
using System.Collections.Generic;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    public List<CraftingRecipe> recipes;

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

    public bool CraftItem(string recipeName)
    {
        CraftingRecipe recipe = recipes.Find(r => r.recipeName == recipeName);
        if (recipe == null) return false;

        InventoryManager im = InventoryManager.Instance;
        foreach (Item ingredient in recipe.ingredients)
        {
            if (!im.HasItem(ingredient.itemName))
            {
                return false;
            }
        }

        // Remove ingredients
        foreach (Item ingredient in recipe.ingredients)
        {
            im.RemoveItem(ingredient);
        }

        // Add result
        im.AddItem(recipe.result);
        Debug.Log("Crafted " + recipe.result.itemName);
        return true;
    }
}

[System.Serializable]
public class CraftingRecipe
{
    public string recipeName;
    public List<Item> ingredients;
    public Item result;
}
