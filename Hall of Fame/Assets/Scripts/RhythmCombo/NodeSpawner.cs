using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject beatNode;
    private bool spawning = false;

    private void Start()
    {
        if (spawning)
        {
            // Start spawning nodes
        }
    }
}
