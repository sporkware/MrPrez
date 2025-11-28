using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public enum NPCType { Citizen, Politician, Advisor, Antagonist }

    public NPCType npcType;
    public float moveSpeed = 3f;
    public float interactionDistance = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private PlayerController player;
    private DialogueSystem dialogueSystem;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        dialogueSystem = FindObjectOfType<DialogueSystem>();

        agent.speed = moveSpeed;

        // Set up behavior based on NPC type
        switch (npcType)
        {
            case NPCType.Citizen:
                StartCoroutine(RandomWalk());
                break;
            case NPCType.Politician:
                // Politicians might have schedules
                break;
            case NPCType.Advisor:
                // Advisors follow player or wait at White House
                break;
            case NPCType.Antagonist:
                // Antagonists plot against player
                break;
        }
    }

    void Update()
    {
        // Check for player interaction
        if (Vector3.Distance(transform.position, player.transform.position) < interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }
    }

    void Interact()
    {
        // Start dialogue based on NPC type
        Dialogue dialogue = CreateDialogueForType();
        if (dialogueSystem != null)
        {
            dialogueSystem.StartDialogue(dialogue);
        }
    }

    Dialogue CreateDialogueForType()
    {
        Dialogue dialogue = new Dialogue();

        switch (npcType)
        {
            case NPCType.Citizen:
                dialogue.sentences = new string[] { "Hello, Mr. President!", "How are you today?" };
                dialogue.choices = new DialogueChoice[]
                {
                    new DialogueChoice { choiceText = "I'm doing great, thank you!", approvalChange = 2f },
                    new DialogueChoice { choiceText = "Mind your own business.", approvalChange = -5f, corruptionChange = 1f },
                    new DialogueChoice { choiceText = "Want to grab a drink later? (Flirt)", approvalChange = 1f, influenceChange = 1f, corruptionChange = 0.5f }
                };
                break;
            case NPCType.Politician:
                dialogue.sentences = new string[] { "We need to discuss policy.", "What's your stance on this issue?" };
                dialogue.choices = new DialogueChoice[]
                {
                    new DialogueChoice { choiceText = "I support progressive reforms.", approvalChange = 3f, influenceChange = 2f },
                    new DialogueChoice { choiceText = "Let's cut taxes for the wealthy.", approvalChange = -2f, influenceChange = 3f, corruptionChange = 2f },
                    new DialogueChoice { choiceText = "Tell me a joke to lighten the mood.", approvalChange = 1f }
                };
                break;
            case NPCType.Advisor:
                dialogue.sentences = new string[] { "Sir, we have a situation.", "How should we handle this scandal?" };
                dialogue.choices = new DialogueChoice[]
                {
                    new DialogueChoice { choiceText = "Cover it up discreetly.", approvalChange = -1f, corruptionChange = 3f },
                    new DialogueChoice { choiceText = "Come clean to the public.", approvalChange = 4f, influenceChange = -1f },
                    new DialogueChoice { choiceText = "Blame it on the media.", approvalChange = -3f, corruptionChange = 1f }
                };
                break;
            case NPCType.Antagonist:
                dialogue.sentences = new string[] { "You think you can get away with this?", "Your days are numbered!" };
                dialogue.choices = new DialogueChoice[]
                {
                    new DialogueChoice { choiceText = "Bring it on!", approvalChange = 1f, influenceChange = 1f },
                    new DialogueChoice { choiceText = "Let's negotiate.", influenceChange = 2f },
                    new DialogueChoice { choiceText = "Make my day. (Threaten)", corruptionChange = 1f }
                };
                break;
        }

        return dialogue;
    }

    System.Collections.IEnumerator RandomWalk()
    {
        while (true)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10f;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            yield return new WaitForSeconds(Random.Range(5f, 15f));
        }
    }
}