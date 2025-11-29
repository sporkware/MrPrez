using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public enum NPCType { Citizen, Politician, Advisor, Antagonist }
    public enum Faction { Supporters, Opponents, Neutrals }
    public enum Behavior { Idle, Patrol, Follow, Flee, Socialize }
    public float relationshipLevel = 0f; // For romance/affairs

    public NPCType npcType;
    public Faction faction;
    public Behavior currentBehavior;
    public float moveSpeed = BalanceConstants.NPCSpeed;
    public float interactionDistance = 2f;
    public float health = BalanceConstants.NPCHealth;
    public float maxHealth = BalanceConstants.NPCHealth;

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
                faction = (Faction)Random.Range(0, 3);
                currentBehavior = Behavior.Patrol;
                StartCoroutine(PatrolBehavior());
                break;
            case NPCType.Politician:
                faction = Faction.Neutrals;
                currentBehavior = Behavior.Socialize;
                StartCoroutine(ScheduleBehavior());
                break;
            case NPCType.Advisor:
                faction = Faction.Supporters;
                currentBehavior = Behavior.Follow;
                StartCoroutine(FollowPlayer());
                break;
            case NPCType.Antagonist:
                faction = Faction.Opponents;
                currentBehavior = Behavior.Flee;
                StartCoroutine(FleeFromPlayer());
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

        // Update behavior based on player proximity and stats
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f && faction == Faction.Opponents)
        {
            currentBehavior = Behavior.Flee;
        }
        else if (distanceToPlayer > 20f)
        {
            // Resume default behavior
        }

        // React to player corruption
        if (player.corruptionLevel > 50f && faction == Faction.Supporters)
        {
            faction = Faction.Neutrals; // Turn neutral
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle NPC death
        Debug.Log(npcType + " died");
        Destroy(gameObject);
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
                    new DialogueChoice { choiceText = "Want to grab a drink later? (Flirt)", approvalChange = 1f, influenceChange = 1f, corruptionChange = 0.5f },
                    new DialogueChoice { choiceText = "Care to join me in the Oval Office? (Seduce)", approvalChange = -1f, influenceChange = 2f, corruptionChange = 2f },
                    new DialogueChoice { choiceText = "You look stunning tonight. Fancy a private tour? (Bold Flirt)", approvalChange = -2f, influenceChange = 3f, corruptionChange = 3f, nextDialogue = null } // Can lead to affair
                };
                break;
            case NPCType.Politician:
                dialogue.sentences = new string[] { "We need to discuss policy.", "What's your stance on this issue?" };
                dialogue.choices = new DialogueChoice[]
                {
                    new DialogueChoice { choiceText = "I support progressive reforms.", approvalChange = 3f, influenceChange = 2f },
                    new DialogueChoice { choiceText = "Let's cut taxes for the wealthy.", approvalChange = -2f, influenceChange = 3f, corruptionChange = 2f },
                    new DialogueChoice { choiceText = "Tell me a joke to lighten the mood.", approvalChange = 1f },
                    new DialogueChoice { choiceText = "How about we discuss this over dinner... and more? (Bribe)", approvalChange = -3f, influenceChange = 4f, corruptionChange = 3f },
                    new DialogueChoice { choiceText = "I have some compromising photos. Support my bill or else. (Blackmail)", approvalChange = -5f, influenceChange = 5f, corruptionChange = 4f }
                };
                break;
            case NPCType.Advisor:
                dialogue.sentences = new string[] { "Sir, we have a situation.", "How should we handle this scandal?" };
                dialogue.choices = new DialogueChoice[]
                {
                    new DialogueChoice { choiceText = "Cover it up discreetly.", approvalChange = -1f, corruptionChange = 3f },
                    new DialogueChoice { choiceText = "Come clean to the public.", approvalChange = 4f, influenceChange = -1f },
                    new DialogueChoice { choiceText = "Blame it on the media.", approvalChange = -3f, corruptionChange = 1f },
                    new DialogueChoice { choiceText = "Let's make this scandal... disappear. (Blackmail)", approvalChange = -2f, influenceChange = 3f, corruptionChange = 4f },
                    new DialogueChoice { choiceText = "I need your help with a personal matter. Keep this quiet. (Affair)", approvalChange = -4f, influenceChange = 4f, corruptionChange = 5f }
                };
                break;
            case NPCType.Antagonist:
                dialogue.sentences = new string[] { "You think you can get away with this?", "Your days are numbered!" };
                dialogue.choices = new DialogueChoice[]
                {
                    new DialogueChoice { choiceText = "Bring it on!", approvalChange = 1f, influenceChange = 1f },
                    new DialogueChoice { choiceText = "Let's negotiate.", influenceChange = 2f },
                    new DialogueChoice { choiceText = "Make my day. (Threaten)", corruptionChange = 1f },
                    new DialogueChoice { choiceText = "Perhaps we can... arrange something mutually beneficial. (Seduce)", approvalChange = -1f, influenceChange = 3f, corruptionChange = 2f },
                    new DialogueChoice { choiceText = "I've got dirt on you too. Let's call a truce... intimately. (Truce with Benefits)", approvalChange = 2f, influenceChange = 4f, corruptionChange = 3f }
                };
                break;
        }

        return dialogue;
    }

    System.Collections.IEnumerator PatrolBehavior()
    {
        while (currentBehavior == Behavior.Patrol)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * 20f;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 20f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            yield return new WaitForSeconds(Random.Range(10f, 30f));
        }
    }

    System.Collections.IEnumerator FollowPlayer()
    {
        while (currentBehavior == Behavior.Follow)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 5f)
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                agent.ResetPath();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    System.Collections.IEnumerator FleeFromPlayer()
    {
        while (currentBehavior == Behavior.Flee)
        {
            Vector3 fleeDirection = transform.position - player.transform.position;
            Vector3 fleePoint = transform.position + fleeDirection.normalized * 20f;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleePoint, out hit, 20f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    System.Collections.IEnumerator ScheduleBehavior()
    {
        // Simple schedule: work during day, socialize at night
        while (true)
        {
            float timeOfDay = (Time.time / 1440f) % 1f; // Assuming 24 min day
            if (timeOfDay > 0.25f && timeOfDay < 0.75f) // Daytime
            {
                currentBehavior = Behavior.Patrol;
                // Go to "work" location, e.g., Capitol
                agent.SetDestination(new Vector3(0, 0, 0)); // Placeholder
            }
            else
            {
                currentBehavior = Behavior.Socialize;
                // Socialize at random locations
                Vector3 socialPoint = transform.position + Random.insideUnitSphere * 10f;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(socialPoint, out hit, 10f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
            }
            yield return new WaitForSeconds(60f); // Check every minute
        }
    }
}