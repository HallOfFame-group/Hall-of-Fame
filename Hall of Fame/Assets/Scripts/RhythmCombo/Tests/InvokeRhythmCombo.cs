using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeRhythmCombo : MonoBehaviour
{
    private void Start()
    {
        RhythmCombo.instance.Test();
        RhythmCombo.instance.Register(this.GetComponent<ComboPiece>());
        RhythmCombo.instance.Display();
        RhythmCombo.instance.callbackFunc = finished;
    }

    void finished()
    {
        Debug.Log("Finished");
        Debug.Log(RhythmCombo.instance.comboResult.perfectCount);
        Debug.Log(RhythmCombo.instance.comboResult.goodCount);
        Debug.Log(RhythmCombo.instance.comboResult.badCount);
        Debug.Log(RhythmCombo.instance.comboResult.missCount);
    }
}
