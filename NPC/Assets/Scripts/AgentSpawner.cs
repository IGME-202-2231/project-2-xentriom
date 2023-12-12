using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> agentPrefabs;
    [SerializeField] List<SpriteRenderer> obstacleList;
    private List<GameObject> spawnedCreatures = new List<GameObject>();

    public List<GameObject> SpawnedCreatures
    {
        get { return spawnedCreatures; }
        set { spawnedCreatures = value; }
    }

    private Vector2 camSize;

    public List<SpriteRenderer> ObstacleList { get => obstacleList; set { obstacleList = value; } } 

    // Start is called before the first frame update
    void Start()
    {
        camSize.y = Camera.main.orthographicSize;
        camSize.x = camSize.y * Camera.main.aspect;

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        // Count of each agent type
        int sharkCount = 3;
        int octopusCount = 3;
        int clownfishCount = 15;

        // Iterate over the agentPrefabs list and spawn agents based on counts
        foreach (GameObject agentPrefab in agentPrefabs)
        {
            int count = 0;
            string sortingLayerName = "";

            switch (agentPrefab.tag)
            {
                case "Swordfish":
                    count = sharkCount;
                    sharkCount = 0;
                    sortingLayerName = "Farground";
                    break;
                case "Octopus":
                    count = octopusCount;
                    octopusCount = 0;
                    sortingLayerName = "Background";
                    break;
                case "Eel":
                    count = clownfishCount;
                    clownfishCount = 0;
                    sortingLayerName = "Foreground";
                    break;
                default:
                    break;
            }

            // Spawn agents
            for (int i = 0; i < count; i++)
            {
                // Set the spawn position with the adjusted z value
                Vector3 spawnPosition = new Vector3(
                    UnityEngine.Random.Range(-camSize.x, camSize.x),
                    UnityEngine.Random.Range(-camSize.y, camSize.y),
                    (int)Enum.Parse(typeof(EnvLayers), sortingLayerName));

                // Instantiate the agent with the adjusted z value
                GameObject spawnedAgent = Instantiate(agentPrefab, spawnPosition, Quaternion.identity);

                spawnedCreatures.Add(spawnedAgent);
            }
        }
    }
}