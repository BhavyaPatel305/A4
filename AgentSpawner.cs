using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;

public class AgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(agentPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
