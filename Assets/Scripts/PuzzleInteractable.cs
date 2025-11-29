using UnityEngine;

public class PuzzleInteractable : Interactable
{
    public Puzzle puzzle;
    private PuzzleSystem puzzleSystem;

    void Start()
    {
        base.Start();
        puzzleSystem = FindObjectOfType<PuzzleSystem>();
    }

    protected override void Interact()
    {
        if (puzzleSystem != null && puzzle != null)
        {
            puzzleSystem.StartPuzzle(puzzle, OnPuzzleComplete);
        }
    }

    private void OnPuzzleComplete(bool success)
    {
        if (success)
        {
            // Add to inventory or something
            InventoryManager.Instance.AddItem(new Item { itemName = "Solved Puzzle Item", description = "Reward for solving puzzle" });
            Destroy(gameObject); // Remove after solving
        }
    }
}
