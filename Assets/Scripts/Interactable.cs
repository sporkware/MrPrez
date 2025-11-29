using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionText = "Interact";
    public float interactionDistance = 3f;

    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    protected virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        // Override in subclasses
    }
}
