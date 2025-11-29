using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    private string savePath;

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

        savePath = Application.persistentDataPath + "/savegame.json";
    }

    public void SaveGame()
    {
        GameData data = new GameData();
        data.playerData = CreatePlayerData();
        data.questData = CreateQuestData();
        data.inventoryData = CreateInventoryData();
        data.settingsData = CreateSettingsData();
        data.achievementData = CreateAchievementData();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved");
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            LoadPlayerData(data.playerData);
            LoadQuestData(data.questData);
            LoadInventoryData(data.inventoryData);
            LoadSettingsData(data.settingsData);
            LoadAchievementData(data.achievementData);
            Debug.Log("Game loaded");
        }
        else
        {
            Debug.Log("No save file found");
        }
    }

    private PlayerData CreatePlayerData()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        return new PlayerData
        {
            position = player.transform.position,
            rotation = player.transform.rotation,
            approvalRating = player.approvalRating,
            influence = player.influence,
            wealth = player.wealth,
            corruptionLevel = player.corruptionLevel,
            currentHealth = player.currentHealth
        };
    }

    private void LoadPlayerData(PlayerData data)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.transform.position = data.position;
        player.transform.rotation = data.rotation;
        player.approvalRating = data.approvalRating;
        player.influence = data.influence;
        player.wealth = data.wealth;
        player.corruptionLevel = data.corruptionLevel;
        player.currentHealth = data.currentHealth;
    }

    private QuestData[] CreateQuestData()
    {
        QuestManager qm = FindObjectOfType<QuestManager>();
        QuestData[] quests = new QuestData[qm.mainQuests.Count + qm.sideQuests.Count];
        int index = 0;
        foreach (Quest q in qm.mainQuests)
        {
            quests[index++] = new QuestData { title = q.title, isActive = q.isActive, isCompleted = q.isCompleted };
        }
        foreach (Quest q in qm.sideQuests)
        {
            quests[index++] = new QuestData { title = q.title, isActive = q.isActive, isCompleted = q.isCompleted };
        }
        return quests;
    }

    private void LoadQuestData(QuestData[] data)
    {
        QuestManager qm = FindObjectOfType<QuestManager>();
        foreach (QuestData qd in data)
        {
            Quest q = qm.mainQuests.Find(q => q.title == qd.title) ?? qm.sideQuests.Find(q => q.title == qd.title);
            if (q != null)
            {
                q.isActive = qd.isActive;
                q.isCompleted = qd.isCompleted;
            }
        }
    }

    private InventoryData CreateInventoryData()
    {
        InventoryManager im = InventoryManager.Instance;
        string[] items = new string[im.inventory.Count];
        for (int i = 0; i < im.inventory.Count; i++)
        {
            items[i] = im.inventory[i].itemName;
        }
        return new InventoryData { itemNames = items };
    }

    private void LoadInventoryData(InventoryData data)
    {
        InventoryManager im = InventoryManager.Instance;
        im.inventory.Clear();
        // Assuming items are predefined, load by name
        // For simplicity, placeholder
    }

    private SettingsData CreateSettingsData()
    {
        SettingsManager sm = SettingsManager.Instance;
        return new SettingsData
        {
            masterVolume = sm.masterVolume,
            musicVolume = sm.musicVolume,
            sfxVolume = sm.sfxVolume,
            mouseSensitivity = sm.mouseSensitivity,
            graphicsQuality = sm.graphicsQuality
        };
    }

    private void LoadSettingsData(SettingsData data)
    {
        SettingsManager sm = SettingsManager.Instance;
        sm.masterVolume = data.masterVolume;
        sm.musicVolume = data.musicVolume;
        sm.sfxVolume = data.sfxVolume;
        sm.mouseSensitivity = data.mouseSensitivity;
        sm.graphicsQuality = data.graphicsQuality;
        sm.ApplySettings();
    }

    private AchievementData CreateAchievementData()
    {
        AchievementManager am = AchievementManager.Instance;
        bool[] unlocked = new bool[am.achievements.Count];
        for (int i = 0; i < am.achievements.Count; i++)
        {
            unlocked[i] = am.achievements[i].unlocked;
        }
        return new AchievementData { unlockedAchievements = unlocked };
    }

    private void LoadAchievementData(AchievementData data)
    {
        AchievementManager am = AchievementManager.Instance;
        for (int i = 0; i < Mathf.Min(data.unlockedAchievements.Length, am.achievements.Count); i++)
        {
            am.achievements[i].unlocked = data.unlockedAchievements[i];
        }
    }
}

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public QuestData[] questData;
    public InventoryData inventoryData;
    public SettingsData settingsData;
    public AchievementData achievementData;
}

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public Quaternion rotation;
    public float approvalRating;
    public float influence;
    public float wealth;
    public float corruptionLevel;
    public float currentHealth;
}

[System.Serializable]
public class QuestData
{
    public string title;
    public bool isActive;
    public bool isCompleted;
}

[System.Serializable]
public class InventoryData
{
    public string[] itemNames;
}

[System.Serializable]
public class SettingsData
{
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float mouseSensitivity;
    public int graphicsQuality;
}

[System.Serializable]
public class AchievementData
{
    public bool[] unlockedAchievements;
}
