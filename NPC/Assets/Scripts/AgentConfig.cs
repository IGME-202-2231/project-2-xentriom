using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentConfig : MonoBehaviour
{
    [SerializeField] AgentSpawner agentSpawner;

    // Start is called before the first frame update
    void Start()
    {
        agentSpawner = GetComponent<AgentSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enrage()
    {
        Wander wander = agentSpawner.GetComponent<Wander>();
        Flee flee = agentSpawner.GetComponent<Flee>();
        Seek seek = agentSpawner.GetComponent<Seek>();

        foreach (var item in agentSpawner.SpawnedCreatures)
        {
            if (item.CompareTag("Eel"))
            {
                wander.enabled = false;
                flee.enabled = true;
            } else if (item.CompareTag("Swordfish"))
            {
                wander.enabled = false;
                seek.enabled = true;
            }
        }
    }

    public void Passive()
    {
        Wander wander = agentSpawner.GetComponent<Wander>();
        Flee flee = agentSpawner.GetComponent<Flee>();
        Seek seek = agentSpawner.GetComponent<Seek>();

        foreach (var item in agentSpawner.SpawnedCreatures)
        {
            if (item.CompareTag("Eel"))
            {
                wander.enabled = true;
                flee.enabled = false;
            }
            else if (item.CompareTag("Swordfish"))
            {
                wander.enabled = true;
                seek.enabled = false;
            }
        }
    }
}
