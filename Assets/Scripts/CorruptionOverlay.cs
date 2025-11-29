using UnityEngine;
using UnityEngine.UI;

public class CorruptionOverlay : MonoBehaviour
{
    public Image overlayImage;
    public PlayerController player;

    void Start()
    {
        if (overlayImage == null)
        {
            overlayImage = GetComponent<Image>();
        }
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }

    void Update()
    {
        if (player != null && overlayImage != null)
        {
            float corruption = player.corruptionLevel / 100f;
            Color color = overlayImage.color;
            color.a = corruption * 0.5f; // Semi-transparent red overlay
            overlayImage.color = color;

            // Screen shake effect
            if (corruption > 0.5f)
            {
                transform.localPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f) * corruption;
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
