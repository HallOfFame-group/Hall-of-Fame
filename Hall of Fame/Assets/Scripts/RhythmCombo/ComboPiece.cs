using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TimedNode
{
    public float timeStamp;
    public NodeButton nodeButton;
}

public class ComboPiece : MonoBehaviour
{
    #region Public Editable Members
    public AudioClip audio;
    public string musicName;
    public string artistName;
    public Image icon;
    public TimedNode[] timeNodeArray;
    #endregion

    #region Private Members

    #endregion

    #region Internal Methods

    #endregion
}
