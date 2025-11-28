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
        }
    }
}