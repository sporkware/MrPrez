using UnityEngine;
using Unity.AI.Navigation;

public class WorldGenerator : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject[] vehiclePrefabs;
    public GameObject[] npcPrefabs;
    public Terrain terrain;
    public Material roadMaterial;
    public Material sidewalkMaterial;

    public int worldSize = 1000;
    public int buildingDensity = 50;
    public int vehicleDensity = 20;
    public int npcDensity = 100;
    public float roadWidth = 10f;
    public float sidewalkWidth = 2f;

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

        // Generate roads and sidewalks
        GenerateRoads();

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

        // Bake NavMesh
        NavMeshSurface navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();

        // Add dynamic events (placeholder)
        InvokeRepeating("SpawnRandomEvent", 60f, 300f); // Every 5 minutes
    }

    void GenerateRoads()
    {
        int gridSize = 20; // Number of roads in each direction
        float spacing = worldSize / gridSize;

        for (int i = 0; i <= gridSize; i++)
        {
            // Horizontal roads
            CreateRoadSegment(new Vector3(-worldSize/2, 0.1f, -worldSize/2 + i * spacing), new Vector3(worldSize/2, 0.1f, -worldSize/2 + i * spacing), roadWidth, roadMaterial);
            // Sidewalks
            CreateRoadSegment(new Vector3(-worldSize/2, 0.1f, -worldSize/2 + i * spacing - roadWidth/2 - sidewalkWidth/2), new Vector3(worldSize/2, 0.1f, -worldSize/2 + i * spacing - roadWidth/2 - sidewalkWidth/2), sidewalkWidth, sidewalkMaterial);
            CreateRoadSegment(new Vector3(-worldSize/2, 0.1f, -worldSize/2 + i * spacing + roadWidth/2 + sidewalkWidth/2), new Vector3(worldSize/2, 0.1f, -worldSize/2 + i * spacing + roadWidth/2 + sidewalkWidth/2), sidewalkWidth, sidewalkMaterial);

            // Vertical roads
            CreateRoadSegment(new Vector3(-worldSize/2 + i * spacing, 0.1f, -worldSize/2), new Vector3(-worldSize/2 + i * spacing, 0.1f, worldSize/2), roadWidth, roadMaterial);
            // Sidewalks
            CreateRoadSegment(new Vector3(-worldSize/2 + i * spacing - roadWidth/2 - sidewalkWidth/2, 0.1f, -worldSize/2), new Vector3(-worldSize/2 + i * spacing - roadWidth/2 - sidewalkWidth/2, 0.1f, worldSize/2), sidewalkWidth, sidewalkMaterial);
            CreateRoadSegment(new Vector3(-worldSize/2 + i * spacing + roadWidth/2 + sidewalkWidth/2, 0.1f, -worldSize/2), new Vector3(-worldSize/2 + i * spacing + roadWidth/2 + sidewalkWidth/2, 0.1f, worldSize/2), sidewalkWidth, sidewalkMaterial);
        }
    }

    void CreateRoadSegment(Vector3 start, Vector3 end, float width, Material material)
    {
        GameObject roadSegment = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 direction = end - start;
        float length = direction.magnitude;
        roadSegment.transform.position = start + direction / 2;
        roadSegment.transform.rotation = Quaternion.LookRotation(direction);
        roadSegment.transform.localScale = new Vector3(width, 0.1f, length);
        if (material != null)
        {
            roadSegment.GetComponent<Renderer>().material = material;
        }
        roadSegment.tag = "Road";
    }

    void SpawnRandomEvent()
    {
        int eventType = Random.Range(0, 3);
        switch (eventType)
        {
            case 0:
                SpawnProtest();
                break;
            case 1:
                SpawnAccident();
                break;
            case 2:
                BroadcastNews();
                break;
        }
    }

    void SpawnProtest()
    {
        Debug.Log("Protest spawned! Approval decreased.");
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyApproval(-10f);
        }
        // Spawn protester NPCs
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-worldSize/2, worldSize/2), 0, Random.Range(-worldSize/2, worldSize/2));
            if (npcPrefabs.Length > 0)
            {
                Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Length)], pos, Quaternion.identity);
            }
        }
    }

    void SpawnAccident()
    {
        Debug.Log("Car accident! Traffic chaos.");
        // Spawn wrecked car, affect traffic
        if (vehiclePrefabs.Length > 0)
        {
            Vector3 pos = new Vector3(Random.Range(-worldSize/2, worldSize/2), 0, Random.Range(-worldSize/2, worldSize/2));
            Instantiate(vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)], pos, Quaternion.identity);
        }
    }

    void BroadcastNews()
    {
        Debug.Log("News broadcast: Scandal revealed! Corruption increased.");
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyCorruption(5f);
        }
        // Play news audio if available
        if (AudioManager.Instance != null)
        {
            // Assume a news clip
        }
    }
}
