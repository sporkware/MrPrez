using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject[] vehiclePrefabs;
    public GameObject[] npcPrefabs;
    public Terrain terrain;

    public int worldSize = 1000;
    public int buildingDensity = 50;
    public int vehicleDensity = 20;
    public int npcDensity = 100;

    void Start()
    {
        GenerateWorld();
    }

    void GenerateWorld()
    {
        // Generate terrain (placeholder - use Unity's Terrain system)
        if (terrain == null)
        {
            terrain = Terrain.CreateTerrainGameObject(new TerrainData()).GetComponent<Terrain>();
            terrain.transform.position = new Vector3(-worldSize/2, 0, -worldSize/2);
            TerrainData terrainData = terrain.terrainData;
            terrainData.size = new Vector3(worldSize, 100, worldSize);
        }

        // Generate buildings
        for (int i = 0; i < buildingDensity; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-worldSize/2, worldSize/2),
                0,
                Random.Range(-worldSize/2, worldSize/2)
            );
            GameObject building = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Length)], position, Quaternion.identity);
            // Adjust height to terrain
            float height = terrain.SampleHeight(position) + building.transform.localScale.y / 2;
            building.transform.position = new Vector3(position.x, height, position.z);
        }

        // Generate vehicles
        for (int i = 0; i < vehicleDensity; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-worldSize/2, worldSize/2),
                0,
                Random.Range(-worldSize/2, worldSize/2)
            );
            Instantiate(vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)], position, Quaternion.identity);
        }

        // Generate NPCs
        for (int i = 0; i < npcDensity; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-worldSize/2, worldSize/2),
                0,
                Random.Range(-worldSize/2, worldSize/2)
            );
            Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Length)], position, Quaternion.identity);
        }

        // Add dynamic events (placeholder)
        InvokeRepeating("SpawnRandomEvent", 60f, 300f); // Every 5 minutes
    }

    void SpawnRandomEvent()
    {
        // Random events like protests, accidents, etc.
        Debug.Log("Random event spawned!");
        // Implementation for specific events
    }
}