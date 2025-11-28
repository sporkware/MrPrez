using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public Quest[] questTemplates; // Assign in inspector

    public Quest GenerateRandomQuest()
    {
        if (questTemplates.Length == 0) return null;

        Quest template = questTemplates[Random.Range(0, questTemplates.Length)];
        Quest newQuest = Instantiate(template);
        newQuest.title = "Random " + template.title;
        newQuest.description = "A random event: " + template.description;
        // Randomize rewards
        newQuest.approvalReward = Random.Range(5, 15);
        newQuest.influenceReward = Random.Range(2, 8);
        newQuest.wealthReward = Random.Range(50, 200);
        // Randomize objectives
        foreach (QuestObjective obj in newQuest.objectives)
        {
            obj.requiredAmount = Random.Range(1, 5);
        }
        return newQuest;
    }
}