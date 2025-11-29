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
        LoadCustomization();
        ApplyCustomization();
    }

    public void NextSkin()
    {
        currentSkin = (currentSkin + 1) % skinMaterials.Length;
        ApplyCustomization();
        SaveCustomization();
    }

    public void NextSuit()
    {
        currentSuit = (currentSuit + 1) % suitMaterials.Length;
        ApplyCustomization();
        SaveCustomization();
    }

    public void NextHair()
    {
        currentHair = (currentHair + 1) % hairStyles.Length;
        ApplyCustomization();
        SaveCustomization();
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

    private void SaveCustomization()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkin);
        PlayerPrefs.SetInt("CurrentSuit", currentSuit);
        PlayerPrefs.SetInt("CurrentHair", currentHair);
        PlayerPrefs.Save();
    }

    private void LoadCustomization()
    {
        currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
        currentSuit = PlayerPrefs.GetInt("CurrentSuit", 0);
        currentHair = PlayerPrefs.GetInt("CurrentHair", 0);
    }
}
