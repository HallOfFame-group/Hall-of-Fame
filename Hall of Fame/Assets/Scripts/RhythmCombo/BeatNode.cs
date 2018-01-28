using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatNode : MonoBehaviour
{
    public NodeButton keyCode;

    private void Awake()
    {
        // Initialize NodeButton to be displayed on screen
    }

    // Update is called once per frame
    private void Update ()
    {
        this.transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.right, 0.1f);
    }
}
