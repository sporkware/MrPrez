using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public RawImage miniMapImage;
    public Transform player;
    public float mapSize = 100f;
    public float updateRate = 0.1f;
    public float zoomSpeed = 10f;
    public float minZoom = 50f;
    public float maxZoom = 200f;

    private RenderTexture renderTexture;
    private Camera miniMapCamera;
    private float lastUpdate;

    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 16);
        miniMapImage.texture = renderTexture;

        GameObject camObj = new GameObject("MiniMapCamera");
        miniMapCamera = camObj.AddComponent<Camera>();
        miniMapCamera.targetTexture = renderTexture;
        miniMapCamera.orthographic = true;
        miniMapCamera.orthographicSize = mapSize / 2f;
        miniMapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void Update()
    {
        // Zoom with mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            mapSize -= scroll * zoomSpeed;
            mapSize = Mathf.Clamp(mapSize, minZoom, maxZoom);
            miniMapCamera.orthographicSize = mapSize / 2f;
        }

        if (Time.time - lastUpdate > updateRate)
        {
            UpdateMiniMap();
            lastUpdate = Time.time;
        }
    }

    void UpdateMiniMap()
    {
        if (player != null && miniMapCamera != null)
        {
            Vector3 pos = player.position;
            pos.y = 50f; // Above terrain
            miniMapCamera.transform.position = pos;
        }
    }
}
