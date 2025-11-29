using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public GameObject mapPanel;
    public UnityEngine.UI.Image mapImage;
    public Transform playerIcon;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
        if (mapPanel.activeSelf)
        {
            UpdatePlayerPosition();
        }
    }

    void ToggleMap()
    {
        mapPanel.SetActive(!mapPanel.activeSelf);
        Time.timeScale = mapPanel.activeSelf ? 0f : 1f;
    }

    void UpdatePlayerPosition()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            // Assume map is scaled, position icon
            Vector3 pos = player.transform.position;
            playerIcon.localPosition = new Vector3(pos.x * 10f, pos.z * 10f, 0f); // Scale as needed
        }
    }
}
