using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    public SkinnedMeshRenderer playerRenderer;
    public Material[] skinMaterials;
    public Material[] suitMaterials;
    public GameObject[] hairStyles;

    private int currentSkin = 0;
    private int currentSuit = 0;
    private int currentHair = 0;

    void Start()
    {
        ApplyCustomization();
    }

    public void NextSkin()
    {
        currentSkin = (currentSkin + 1) % skinMaterials.Length;
        ApplyCustomization();
    }

    public void NextSuit()
    {
        currentSuit = (currentSuit + 1) % suitMaterials.Length;
        ApplyCustomization();
    }

    public void NextHair()
    {
        currentHair = (currentHair + 1) % hairStyles.Length;
        ApplyCustomization();
    }

    private void ApplyCustomization()
    {
        if (playerRenderer != null)
        {
            // Assuming materials are assigned to renderer
            playerRenderer.materials = new Material[] { skinMaterials[currentSkin], suitMaterials[currentSuit] };
        }
        // Hide/show hair
        foreach (GameObject hair in hairStyles)
        {
            hair.SetActive(false);
        }
        hairStyles[currentHair].SetActive(true);
    }
}