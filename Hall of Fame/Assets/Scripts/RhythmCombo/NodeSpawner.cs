using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    public int bpm;
    [SerializeField] private GameObject beatNode;
    private bool spawning;
    private int spawnCount;

    private float elapsedTime;

    private TimedNode[] timeNodes;

    private void ResetNodeSpawner()
    {
        spawning = false;
        spawnCount = 0;
        elapsedTime = 0.0f;
    }

    private void Awake()
    {
        ResetNodeSpawner();
    }

    private void Update()
    {
        // Start spawning beat nodes if enabled
        if (spawning)
        {
            // Increments the elasped timer to keep track of when to spawn node
            elapsedTime += Time.deltaTime;

            if (timeNodes[spawnCount].timeStamp <= elapsedTime)
            {
                // Instantiate beat node
                Instantiate(beatNode, this.transform.position, this.transform.rotation);

                // Increment spaw count
                ++spawnCount;
            }

            // When finished spawning, stop the process
            if (spawnCount >= timeNodes.Length)
            {
                spawning = false;
            }
        }
    }

    public void PrepareNodes(TimedNode[] timeNodes)
    {
        this.timeNodes = timeNodes;
        ResetNodeSpawner();
    }

    public void StartSpawning()
    {
        spawning = true;
        Debug.Log("NodeSpawner Enable");
    }
}
