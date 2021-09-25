using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteAlways]
[CustomEditor(typeof(MapGenerator))]
public class GenerateMap : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;
        DrawDefaultInspector();
        if (mapGen.isAutoUpdate)
        {
            mapGen.GenerateMap();
        }
        if (GUILayout.Button("GenerateMap"))
        {
            mapGen.GenerateMap();
        }
    }
}
