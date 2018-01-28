﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    #region Private Members
    // Controls the spawning timing offset and speed of node
    public int bpm;

    public delegate void NodeSpawnerCallback();

    // Total spawned count
    public int spawnCount;

    // Callback function registed
    public NodeSpawnerCallback callbackFunc;
    #endregion

    #region Private Members
    // BeatNode prefab object
    [SerializeField] private GameObject beatNode;

    // Boolean indicate start spawning according to the TimeNode registered
    private bool spawning;

    // Timer variable for spawning node as TimeNode array indicated
    private float elapsedTime;

    // TimeNode array from ComboPiece registered
    private TimedNode[] timeNodes;
    #endregion

    #region Private Methods
    private void ResetNodeSpawner()
    {
        spawning = false;
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
                Instantiate(beatNode, this.transform).GetComponent<BeatNode>().keyCode = timeNodes[spawnCount].nodeButton;

                // Increment spawn count
                ++spawnCount;
            }

            // When finished spawning, stop the process
            if (spawnCount >= timeNodes.Length)
            {
                spawning = false;
                callbackFunc();
            }
        }
    }
    #endregion

    #region Public Methods

    public void PrepareNodes(TimedNode[] timeNodes)
    {
        this.timeNodes = timeNodes;
        ResetNodeSpawner();
    }

    public void StartSpawning()
    {
        spawning = true;
        spawnCount = 0;
    }

    #endregion
}
