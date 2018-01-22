using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ComboPiece))]
[CanEditMultipleObjects]
public class ComboPieceEditor : Editor
{
    #region SerializedProperty from ComboPiece.cs
    SerializedProperty audioProp;
    SerializedProperty musicNameProp;
    SerializedProperty aristNameProp;
    SerializedProperty iconProp;
    #endregion

    #region Override Editor Methods
    private void OnEnable()
    {
        // Setup serialized properties
        audioProp = serializedObject.FindProperty("audio");
        musicNameProp = serializedObject.FindProperty("musicName");
        aristNameProp = serializedObject.FindProperty("artistName");
        iconProp = serializedObject.FindProperty("icon");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializeProperty from inspector viewport
        serializedObject.Update();

        // Apply changes occurred in inspectorview
        serializedObject.ApplyModifiedProperties();
    }
    #endregion


}
