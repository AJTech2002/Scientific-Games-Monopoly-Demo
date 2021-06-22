using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardItem))]
[CanEditMultipleObjects]
public class BoardItemInspector : Editor
{
    BoardItem item;

    void OnEnable()
    {
        item = (BoardItem)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);

        if (GUILayout.Button("Setup Node"))
        {
            if (item.property != null)
            {
                item.gameObject.GetComponent<MeshRenderer>().material.SetColor("_PropertyColor", item.property.propertyColor);
            }
        }
    }

}
