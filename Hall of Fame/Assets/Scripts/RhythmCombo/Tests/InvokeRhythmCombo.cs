using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeRhythmCombo : MonoBehaviour
{
    private void Start()
    {
        RhythmCombo.instance.Test();
        RhythmCombo.instance.Register(this.GetComponent<ComboPiece>());
    }
}
