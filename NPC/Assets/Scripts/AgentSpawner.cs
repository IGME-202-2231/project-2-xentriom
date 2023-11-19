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

    private void Spawn()
    {
        foreach (var item in agentPrefabs)
        {
            Instantiate(item,
                new Vector3(
                    Random.Range(-camSize.x, camSize.x),
                    Random.Range(-camSize.y, camSize.y),
                    -3f),
                Quaternion.Euler(new Vector3(0f, 90f, 0f)));
        }
    }
}