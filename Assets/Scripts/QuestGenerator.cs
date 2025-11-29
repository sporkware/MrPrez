using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public Quest[] questTemplates; // Assign in inspector

    public enum QuestType { Political, Criminal, Social, LeisureSuitLarry, Hidden, Random }

    public Quest GenerateQuest(QuestType type)
    {
        bool adultEnabled = SettingsManager.Instance != null && SettingsManager.Instance.adultContentEnabled;
        switch (type)
        {
            case QuestType.Political:
                return GeneratePoliticalQuest();
            case QuestType.Criminal:
                return GenerateCriminalQuest();
            case QuestType.Social:
                return GenerateSocialQuest();
            case QuestType.LeisureSuitLarry:
                if (adultEnabled)
                    return GenerateLeisureSuitLarryQuest();
                else
                    return GenerateSocialQuest(); // Fallback
            default:
                return GenerateRandomQuest();
        }
    }

    public Quest GenerateRandomQuest()
    {
        if (questTemplates.Length == 0) return null;

        Quest template = questTemplates[Random.Range(0, questTemplates.Length)];
        Quest newQuest = Instantiate(template);
        newQuest.title = "Random " + template.title;
        newQuest.description = "A random event: " + template.description;
        RandomizeQuest(newQuest);
        return newQuest;
    }

    private Quest GeneratePoliticalQuest()
    {
        Quest quest = ScriptableObject.CreateInstance<Quest>();
        quest.title = "Political Maneuver";
        quest.description = "Lobby for a bill or campaign in a district.";
        quest.objectives = new QuestObjective[] {
            new QuestObjective { type = QuestObjective.ObjectiveType.Dialogue, target = "Politician", requiredAmount = 3 }
        };
        RandomizeQuest(quest);
        return quest;
    }

    private Quest GenerateCriminalQuest()
    {
        Quest quest = ScriptableObject.CreateInstance<Quest>();
        quest.title = "Criminal Activity";
        quest.description = "Smuggle contraband or eliminate a rival.";
        quest.objectives = new QuestObjective[] {
            new QuestObjective { type = QuestObjective.ObjectiveType.Kill, target = "Rival", requiredAmount = 1 }
        };
        RandomizeQuest(quest);
        return quest;
    }

    private Quest GenerateSocialQuest()
    {
        Quest quest = ScriptableObject.CreateInstance<Quest>();
        quest.title = "Social Event";
        quest.description = "Attend a gala or host a dinner.";
        quest.objectives = new QuestObjective[] {
            new QuestObjective { type = QuestObjective.ObjectiveType.Collect, target = "Invitation", requiredAmount = 1 }
        };
        RandomizeQuest(quest);
        return quest;
    }

    private Quest GenerateLeisureSuitLarryQuest()
    {
        Quest quest = ScriptableObject.CreateInstance<Quest>();
        string[] titles = { "Seduce the Secretary", "Find the Lost Underwear", "Hack the Love Letter", "Party Crash Scandal" };
        string[] descriptions = {
            "Charm your way to get classified info from the attractive aide.",
            "Retrieve embarrassing items from a rival's hotel room.",
            "Intercept and decode a romantic message.",
            "Infiltrate a wild party and cause chaos."
        };
        int rand = Random.Range(0, titles.Length);
        quest.title = titles[rand];
        quest.description = descriptions[rand];
        quest.objectives = new QuestObjective[] {
            new QuestObjective { type = QuestObjective.ObjectiveType.Dialogue, target = "NPC", requiredAmount = 5 },
            new QuestObjective { type = QuestObjective.ObjectiveType.Collect, target = "Item", requiredAmount = 1 }
        };
        RandomizeQuest(quest);
        return quest;
    }

    private void RandomizeQuest(Quest quest)
    {
        // Randomize rewards
        quest.approvalReward = Random.Range(5, 15);
        quest.influenceReward = Random.Range(2, 8);
        quest.wealthReward = Random.Range(50, 200);
        // Randomize objectives
        foreach (QuestObjective obj in quest.objectives)
        {
            obj.requiredAmount = Random.Range(1, 5);
        }
    }
}
