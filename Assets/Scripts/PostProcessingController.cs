using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    public PostProcessVolume volume;
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (volume != null && player != null)
        {
            // Adjust vignette based on corruption
            Vignette vignette;
            if (volume.profile.TryGetSettings(out vignette))
            {
                vignette.intensity.value = player.corruptionLevel / 100f * 0.5f;
            }

            // Adjust color grading
            ColorGrading colorGrading;
            if (volume.profile.TryGetSettings(out colorGrading))
            {
                colorGrading.saturation.value = Mathf.Lerp(0f, -50f, player.corruptionLevel / 100f);
            }

            // Adjust bloom based on approval
            Bloom bloom;
            if (volume.profile.TryGetSettings(out bloom))
            {
                bloom.intensity.value = Mathf.Lerp(0f, 5f, player.approvalRating / 100f);
            }
        }
    }
}