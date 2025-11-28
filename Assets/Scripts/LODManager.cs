using UnityEngine;

public class LODManager : MonoBehaviour
{
    public LODGroup[] lodGroups;
    public float updateInterval = 1f;

    private float lastUpdate;

    void Update()
    {
        if (Time.time - lastUpdate > updateInterval)
        {
            UpdateLODs();
            lastUpdate = Time.time;
        }
    }

    private void UpdateLODs()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        foreach (LODGroup lod in lodGroups)
        {
            if (lod != null)
            {
                float distance = Vector3.Distance(cam.transform.position, lod.transform.position);
                lod.ForceLOD(Mathf.Clamp((int)(distance / 50f), 0, lod.lodCount - 1));
            }
        }
    }
}