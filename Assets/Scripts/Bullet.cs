using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public float lifetime = 5f;
    public float damage = 20f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Apply damage to NPCs or player
        NPCController npc = collision.gameObject.GetComponent<NPCController>();
        if (npc != null)
        {
            npc.TakeDamage(damage);
        }
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
