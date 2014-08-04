//SnapToGrid.cs
//Created on 01/05/2014
//Last Updated on 21/05/2014
//Version 0.86
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//This class handles resetting a blox to an even world position

using UnityEngine;
using UnityEditor;
using System.Collections;
 
public class SnapToGrid : ScriptableObject
{
    [MenuItem ("Easy Blox/Snap to Grid %g")]
    static void MenuSnapToGrid()
    {
        Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);
 
        float gridx = 1.0f;
        float gridy = 1.0f;
        float gridz = 1.0f;
 
        foreach (Transform transform in transforms)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Round(newPosition.x / gridx) * gridx;
            newPosition.y = Mathf.Round(newPosition.y / gridy) * gridy;
            newPosition.z = Mathf.Round(newPosition.z / gridz) * gridz;
            transform.position = newPosition;
        }
    }
}