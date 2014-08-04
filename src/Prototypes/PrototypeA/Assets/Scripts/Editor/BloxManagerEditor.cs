//BloxManagerEditor.cs
//Created on 01/05/2014
//Last Updated on 21/05/2014
//Version 0.86
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//This class is used to reload textures


using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TextureManager))]
public class BloxManagerEditor : Editor {
	
	private TextureManager manager;
	
	void Awake()
	{
		manager = ((TextureManager)target);
	}	
	
	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("Reload Textures"))
		{
			manager.IsLoaded = false;
			manager.LoadBloxData();
		}
	}
}