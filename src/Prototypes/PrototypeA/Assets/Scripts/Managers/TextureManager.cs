//TextureManager.cs
//Created on 01/05/2014
//Last Updated on 21/05/2014
//Version 0.86
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//This class handles all of the texture loading 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
[ExecuteInEditMode]
public class TextureManager : MonoBehaviour {
	
	[SerializeField]
	private bool isLoaded = false;
	[SerializeField]
	private float bloxMove = 0.5f;
	[SerializeField]
	private float bloxScale = 1.0f;
	[SerializeField]
	private float bloxHeight = 0.1f;
	[SerializeField]
	private float bloxStretch = 0.5f;
	[SerializeField]
	private bool bloxStretchXorZ= false;
	
	//texture arrays
	public string[] arrayNames = new string[2] {"Base", "Misc"};
	public Texture2D[]	_baseTextures;		//Array to hold all the textures from the Abandoned Base textures
	public Texture2D[]	_miscTextures;		//Array to hold all the misc textures
	
	//Floor values
	public int _baseTexturesValue;
	public int _miscTexturesValue;
	
	void Awake()
	{
		LoadBloxData();		//We Load all textures from our texture library	
	}
	
	public float BloxMove
	{
		get{ return bloxMove; }
		set{ bloxMove = value; }
	}
	
	public float BloxScale
	{
		get{ return bloxScale; }
		set{ bloxScale = value; }
	}
	
	public float BloxHeight
	{
		get{ return bloxHeight; }
		set{ bloxHeight = value; }
	}
	
	public float BloxStretch
	{
		get{ return bloxStretch; }
		set{ bloxStretch = value; }
	}
	
	public bool BloxStretchXorZ
	{
		get{ return bloxStretchXorZ; }
		set{ bloxStretchXorZ = value; }
	}
	
	public bool IsLoaded
	{
		get{ return isLoaded; }
		set{ isLoaded = value;}
	}
	
	public void LoadBloxData()
	{			
		if (isLoaded != true)
		{						
			//Load Abandoned Base textures
			_baseTextures = LoadTextures(_baseTextures,"Textures/Base");
			_baseTexturesValue = _baseTextures.Length;
			
			//Load Misc textures
			_miscTextures = LoadTextures(_miscTextures,"Textures/Misc");			
			_miscTexturesValue = _miscTextures.Length;
									
			isLoaded = true;
		}
	}
	
	private Texture2D[] LoadTextures(Texture2D[] arrayID, string textureLocation)
	{	
		object[] TexturesResources = Resources.LoadAll(textureLocation, typeof(Texture2D));
		
		var arrayLength = TexturesResources.Length;		
		
		arrayID = new Texture2D[arrayLength];		
		
		for (int i = 0;i < arrayLength;i++)
		{
			arrayID[i] = (Texture2D)TexturesResources[i];
		}		
		
		return arrayID;
	}
	
	public Texture2D GetTexture(int arrayChoice, int id)
	{
		if (arrayChoice == 0)
			return _baseTextures[id];
		
			return _miscTextures[id];
	}
	
	public int GetArrayLength(int arrayChoice, int id)
	{
		if (arrayChoice == 0)
			return _baseTexturesValue -1;
		
			return _miscTexturesValue -1;
	}	
	
	public string GetClassName(int id)
	{
		return arrayNames[id];
	}
	
	void OnGUI()
	{
		GUILayout.Button("Reload Textures");
	}
}
