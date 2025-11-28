using UnityEngine;

public class TestRunner : MonoBehaviour
{
    void Start()
    {
        RunTests();
    }

    private void RunTests()
    {
        Debug.Log("Running unit tests...");

        // Test PlayerController stats
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyApproval(10f);
            Debug.Assert(player.approvalRating == 60f, "Approval test failed");
            Debug.Log("Player stats test passed");
        }

        // Test Inventory
        InventoryManager im = InventoryManager.Instance;
        if (im != null)
        {
            Item testItem = new Item { itemName = "Test" };
            bool added = im.AddItem(testItem);
            Debug.Assert(added, "Inventory add test failed");
            Debug.Log("Inventory test passed");
        }

        Debug.Log("All tests completed");
    }
}