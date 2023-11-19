using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> agentPrefabs;
    private Vector2 camSize;

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

    //private void Spawn()
    //{
    //    // Count of each agent type
    //    int sharkCount = 1;
    //    int octopusCount = 3;
    //    int doryCount = 8;
    //    int clownfishCount = 9;

    //    // Iterate over the agentPrefabs list and spawn agents based on counts
    //    foreach (GameObject agentPrefab in agentPrefabs)
    //    {
    //        int count = 0;

    //        switch (agentPrefab.tag)
    //        {
    //            case "Shark":
    //                count = sharkCount;
    //                sharkCount = 0;
    //                break;
    //            case "Octopus":
    //                count = octopusCount;
    //                octopusCount = 0;
    //                break;
    //            case "Dory":
    //                count = doryCount;
    //                doryCount = 0;
    //                break;
    //            case "Clownfish":
    //                count = clownfishCount;
    //                clownfishCount = 0;
    //                break;
    //            default:
    //                break;
    //        }

    //        // Spawn agents
    //        for (int i = 0; i < count; i++)
    //        {
    //            Vector2 spawnPosition = new Vector2(
    //                Random.Range(-camSize.x, camSize.x),
    //                Random.Range(-camSize.y, camSize.y));
    //            Instantiate(agentPrefab, spawnPosition, Quaternion.Euler(0f, 90f, 0f));
    //        }
    //    }
    //}

    private void Spawn()
    {
        // Count of each agent type
        int sharkCount = 1;
        int octopusCount = 3;
        int doryCount = 8;
        int clownfishCount = 9;

        // Iterate over the agentPrefabs list and spawn agents based on counts
        foreach (GameObject agentPrefab in agentPrefabs)
        {
            int count = 0;
            string sortingLayerName = "";  // Predefined sorting layer for each agent type

            switch (agentPrefab.tag)
            {
                case "Shark":
                    count = sharkCount;
                    sharkCount = 0;
                    sortingLayerName = "Farground";
                    break;
                case "Octopus":
                    count = octopusCount;
                    octopusCount = 0;
                    sortingLayerName = "Background";
                    break;
                case "Dory":
                    count = doryCount;
                    doryCount = 0;
                    sortingLayerName = "Midground";
                    break;
                case "Clownfish":
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
                Instantiate(agentPrefab, spawnPosition, Quaternion.Euler(0f, 90f, 0f));
            }
        }
    }
}