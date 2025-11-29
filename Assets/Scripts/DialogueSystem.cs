using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    public Text dialogueText;
    public Button[] choiceButtons;
    public GameObject dialoguePanel;
    public AudioSource voiceSource;

    private Queue<string> sentences;
    private Dialogue currentDialogue;
    private int currentSentenceIndex = 0;
    private static bool firstDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        dialoguePanel.SetActive(true);
        sentences.Clear();

        // Customize sentences based on player stats for satire
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            foreach (string sentence in dialogue.sentences)
            {
                string customized = CustomizeSentence(sentence, player);
                sentences.Enqueue(customized);
            }
        }
        else
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        DisplayNextSentence();

        // Achievement
        if (!firstDialogue && AchievementManager.Instance != null)
        {
            firstDialogue = true;
            AchievementManager.Instance.UnlockAchievement("First Conversation");
        }
    }

    private string CustomizeSentence(string sentence, PlayerController player)
    {
        // Satirical customizations
        if (sentence.Contains("[APPROVAL]"))
        {
            if (player.approvalRating > 80f)
            {
                return sentence.Replace("[APPROVAL]", "Oh, Mr. President, you're so popular!");
            }
            else if (player.approvalRating < 20f)
            {
                return sentence.Replace("[APPROVAL]", "Sir, the polls are not looking good...");
            }
            else
            {
                return sentence.Replace("[APPROVAL]", "How's the approval rating holding up?");
            }
        }
        if (sentence.Contains("[CORRUPTION]"))
        {
            if (player.corruptionLevel > 50f)
            {
                return sentence.Replace("[CORRUPTION]", "I hear some... interesting rumors about you.");
            }
            else
            {
                return sentence.Replace("[CORRUPTION]", "You're a model of integrity, sir.");
            }
        }
        return sentence;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = currentDialogue.speakerName + ": " + sentence;
        // Play voice clip
        if (voiceSource != null && currentDialogue.audioClips != null && currentSentenceIndex < currentDialogue.audioClips.Length)
        {
            voiceSource.clip = currentDialogue.audioClips[currentSentenceIndex];
            voiceSource.Play();
        }
        currentSentenceIndex++;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        // Apply consequences based on choices
        if (currentDialogue.choices.Length > 0)
        {
            // Handle choice consequences
            foreach (DialogueChoice choice in currentDialogue.choices)
            {
                // Implement choice logic (e.g., modify player stats)
            }
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (choiceIndex < currentDialogue.choices.Length)
        {
            DialogueChoice choice = currentDialogue.choices[choiceIndex];
            // Apply choice effects
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.ModifyApproval(choice.approvalChange);
                player.ModifyInfluence(choice.influenceChange);
                player.ModifyCorruption(choice.corruptionChange);
            }
            // If next dialogue, start it
            if (choice.nextDialogue != null)
            {
                StartDialogue(choice.nextDialogue);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string speakerName;
    public string[] sentences;
    public AudioClip[] audioClips;
    public DialogueChoice[] choices;
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public float approvalChange;
    public float influenceChange;
    public float corruptionChange;
    public Dialogue nextDialogue;
}
