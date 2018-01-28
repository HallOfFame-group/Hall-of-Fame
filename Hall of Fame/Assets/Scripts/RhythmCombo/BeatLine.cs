using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLine : MonoBehaviour
{
    #region Private Members
    // Using a list of game objects to track the nodes enter/exist the detection region
    List<GameObject> nodeList;
    #endregion

    #region Public Members
    // Callback function for when every time a node has been processed
    public delegate void BeatLineCallback();

    public BeatLineCallback callbackFunc;

    // Counter for total amount of nodes registered
    public int nodeCount;

    // Rhythm result from pressing the button
    public RhythmResult rhythmResult;
    #endregion

    #region Private Methods

    // Initializes the members on awake
    private void Awake()
    {
        nodeList = new List<GameObject>();
        rhythmResult = new RhythmResult();
        nodeCount = 0;
    }

    // When a node enters the detection region, register it to the list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        nodeList.Add(collision.gameObject);
    }

    // When a node leaves the detection region, destroy it, mark as miss, and increase total count
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remoe from list
        nodeList.Remove(collision.gameObject);
        Destroy(collision.gameObject);

        // Count as miss
        ++rhythmResult.missCount;

        // Increment total count and calls to caller
        ++nodeCount;
        callbackFunc();
    }

    private void Update()
    {
        // Right now just using space bar for detection
        if (Input.GetKeyDown(KeyCode.Space) && nodeList.Count > 0)
        {
            // Using the distance from the center to determine the score
            // Perfect - < 1
            // Good  - < 2
            // Bad - < 3
            // Miss everything else
            if (Mathf.Abs(nodeList[0].transform.position.x - this.transform.position.x) < 1)
            {
                ++rhythmResult.perfectCount;
            }
            else if (Mathf.Abs(nodeList[0].transform.position.x - transform.position.x) < 2)
            {
                ++rhythmResult.goodCount;
            }
            else if (Mathf.Abs(nodeList[0].transform.position.x - transform.position.x) < 3)
            {
                ++rhythmResult.badCount;
            }
            else
            {
                ++rhythmResult.missCount;
            }

            // Remove the processed node from the list
            GameObject obj = nodeList[0];
            nodeList.RemoveAt(0);
            Destroy(obj);

            // Increase the total count
            ++nodeCount;

            // Notify the caller a node has been processed
            callbackFunc();
        }
    }
    #endregion
}
