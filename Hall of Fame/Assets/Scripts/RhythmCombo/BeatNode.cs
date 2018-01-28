using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatNode : MonoBehaviour
{
    // Update is called once per frame
    private void Update ()
    {
        this.transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.right, 0.1f);
    }
}
