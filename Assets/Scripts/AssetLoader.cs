using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public static AssetLoader Instance { get; private set; }

    // Assign in inspector or load dynamically
    public GameObject[] buildingPrefabs;
    public Material[] materials;
    public AudioClip[] audioClips;
    public RuntimeAnimatorController[] animators;

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
    }

    // Methods to get assets by name or index
    public GameObject GetBuildingPrefab(string name)
    {
        foreach (GameObject prefab in buildingPrefabs)
        {
            if (prefab.name == name) return prefab;
        }
        return null;
    }

    public Material GetMaterial(string name)
    {
        foreach (Material mat in materials)
        {
            if (mat.name == name) return mat;
        }
        return null;
    }

    public AudioClip GetAudioClip(string name)
    {
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == name) return clip;
        }
        return null;
    }

    public RuntimeAnimatorController GetAnimator(string name)
    {
        foreach (RuntimeAnimatorController anim in animators)
        {
            if (anim.name == name) return anim;
        }
        return null;
    }
}
