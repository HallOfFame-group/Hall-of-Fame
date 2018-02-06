using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeButton
{
    Key1,
    Key2
}

public class BeatNode : MonoBehaviour
{
    public NodeButton keyCode;

    // Lerping time interval, determines the speed of lerping
    private float timeInterval;

    // Final destination relative to spawn point
    Vector3 destination;

    // Flag controlling lerping motion state
    private bool start = false;

    private float timeElapsed = 0;

    public void StartNode(float distance, float travelSpeed)
    {
        destination = this.transform.position + Vector3.right * distance;
        timeInterval = distance / travelSpeed * Time.fixedDeltaTime;
        start = true;
    }

    // Update is called once per frame
    private void FixedUpdate ()
    {
        if (start)
        {
            this.transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.right, timeInterval);
        }
    }
}
