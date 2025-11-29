using UnityEngine;
using UnityEngine.UI;

public class PhoneSystem : MonoBehaviour
{
    public static PhoneSystem Instance { get; private set; }

    public GameObject phonePanel;
    public Text callerText;
    public Button answerButton;
    public Button hangupButton;

    private Dialogue currentCallDialogue;
    public Dialogue[] possibleCalls; // Assign in inspector

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
        phonePanel.SetActive(false);
        answerButton.onClick.AddListener(AnswerCall);
        hangupButton.onClick.AddListener(HangupCall);
    }

    public void IncomingCall(Dialogue callDialogue)
    {
        currentCallDialogue = callDialogue;
        callerText.text = callDialogue.speakerName + " is calling...";
        phonePanel.SetActive(true);
        // Play ring sound
        if (AudioManager.Instance != null)
        {
            // Assume ring clip
        }
    }

    private void AnswerCall()
    {
        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.StartDialogue(currentCallDialogue);
        }
        phonePanel.SetActive(false);
    }

    private void HangupCall()
    {
        phonePanel.SetActive(false);
        // Missed call consequences
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyApproval(-1f);
        }
    }

    public void TriggerRandomCall()
    {
        if (possibleCalls.Length > 0)
        {
            Dialogue randomCall = possibleCalls[Random.Range(0, possibleCalls.Length)];
            IncomingCall(randomCall);
        }
    }

    // Call this from events, like after quest or time-based
    public void ScheduleCall(float delay)
    {
        Invoke("TriggerRandomCall", delay);
    }
}
