//Blox.cs
//Created on 01/05/2014
//Last Updated on 14/05/2014
//Version 0.85
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//this class is used on the Blox cubes themselves and contains all their data

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))] 
public class Blox : MonoBehaviour {
		
	//Texture variables
	public TextureManager texManager;
	private Texture2D tex;
	private Material mat;
	
	//Blox Info
	[SerializeField]
	private bool isFinished = false;	
	[SerializeField]
	private bool isCombined = false;	
	[SerializeField]
	private bool isWall = false;
	[SerializeField]
	private bool isSpawnPoint = false;
	[SerializeField]
	private int myBloxIdValue = 1;
	[SerializeField]
	private int myBloxMeshCategory = 0;
	[SerializeField]
	private int myBloxMeshValue = 0;
	[SerializeField]
	private bool doRender = true;
		
	//These bools are used for raycasting position checking
	public bool _xPositive = false;
	public bool _xNegative = false;
	public bool _zPositive = false;
	public bool _zNegative = false;
		
	void Awake()
	{		
//		if (GetComponent(typeof(SkinnedMeshRenderer)) == null)
//		{
//			gameObject.AddComponent(typeof(SkinnedMeshRenderer));
//			
//			
//		}
		
		Init();				
		RenderMyTexture();		//We render the texture for this blox
	}
	
	//Anything for initialization goes here
	private void Init()
	{		
		this.GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("Diffuse");
		//this.GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader = Shader.Find("Diffuse");
		
		var go = GameObject.Find("__BloxManager");
		texManager = go.GetComponent<TextureManager>();
	}
	
	public int MyBloxID
	{
		get{ return myBloxIdValue; }
		set{ myBloxIdValue = value;}
	}
	
	public int MyBloxMeshID
	{
		get{ return myBloxMeshValue; }
		set{ myBloxMeshValue = value;}
	}
	
	public int MyBloxMeshCategoryID
	{
		get{ return myBloxMeshCategory; }
		set{ myBloxMeshCategory = value;}
	}
	
	public bool DoRender
	{
		get{ return doRender; }
		set{ doRender = value;}		
	}
	
	public Texture2D myTexture
	{
		get{ return tex; }
		set{ tex = value;}	
	}
	
	public bool IsWall
	{
		get{ return isWall; }
		set{ isWall = value;}	
	}
	
	public bool IsFinished
	{
		get{ return isFinished; }
		set{ isFinished = value;}	
	}
	
	public bool IsCombined
	{
		get{ return isCombined; }
		set{ isCombined = value;}	
	}
	
	public bool IsSpawnPoint
	{
		get{ return isSpawnPoint; }
		set{ isSpawnPoint = value;}	
	}
			
	//Workaround for fixing an instantiate mainMaterial error into the Editor
	public void RenderMyTexture()
	{			
		if (DoRender)
		{	
			mat = new Material(renderer.sharedMaterial);
			myTexture = texManager.GetTexture(MyBloxMeshCategoryID,MyBloxMeshID);			
			mat.mainTexture = myTexture;			
			
			this.GetComponent<MeshRenderer>().sharedMaterial = mat;
			DoRender = false;
		}
	}
	
	//We take our meshes and combine them into one
	//then we destroy the Boxcollider and add a new one to the entire merged gameobject
	public void CombineMeshes()
	{	
		Transform[] transforms = GetComponentsInChildren<Transform>();
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();		
		BoxCollider[] collider = GetComponentsInChildren<BoxCollider>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		//Matrix4x4[] locations = new Matrix4x4[meshFilters.Length];
		
		int index = 0;
		
		for (int i = 0;i < meshFilters.Length;i++) 
		{
            combine[index].mesh = meshFilters[i].sharedMesh;
			combine[index++].transform = meshFilters[i].transform.localToWorldMatrix;
			//combine[index++].transform = Matrix4x4.TRS((meshFilters[i].transform.position) , Quaternion.Inverse(gameObject.transform.rotation), Vector3.one);
			//combine[index++].transform = Matrix4x4.TRS(gameObject.transform.InverseTransformPoint(transforms[i].position), Quaternion.Inverse(gameObject.transform.rotation), Vector3.one);
			meshFilters[i].gameObject.SetActive(false);
		
			DestroyImmediate(collider[i]);
        }	
				
		Vector3 newloc = new Vector3(0,0,0);
		this.transform.position = newloc;
        transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
				
		gameObject.AddComponent<MeshCollider>();
					
        transform.gameObject.SetActive(true);
		
		//We now look for all the children objects and delete them from the final result
		var children = new List<GameObject>();
    		foreach (Transform child in transform) children.Add(child.gameObject);
    		children.ForEach(child => DestroyImmediate(child));
		
		//We set the combined state to true
		this.isCombined = true;
	}
			
	//We take our new position or update our old one
	public void GetMyPosition()
	{
		Vector3 newLocation = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		transform.position = newLocation;
	}
	
	public void SetMyXPosition(float pos_x,float pos_y,float pos_z)
	{		
		Vector3 location;
		
		if (this.transform.IsChildOf(transform))
		{
			location = new Vector3(pos_x,pos_y,pos_z);
		}
		else
		{
			location = new Vector3(pos_x,pos_y,pos_z);
		}		
		
		transform.position = location;
	}
	
	//We get our player character and spawn it
	public void SpawnPlayer()
	{
		var first = Resources.Load("Standard Assets/Character Controllers/First Person Controller");	//This is the first person controller
		
		if (isSpawnPoint)
		{
			first = (GameObject)Instantiate(first,new Vector3(this.transform.position.x,this.transform.position.y+2,this.transform.position.z), Quaternion.identity);
		}
	}
}










//	public void CombineMeshes()
//	{	
//		Transform[] transforms = GetComponentsInChildren<Transform>();
//		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
//		BoxCollider[] collider = GetComponentsInChildren<BoxCollider>();
//		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
//		
//		for (int i = 0;i < meshFilters.Length;i++) 
//		{
//            combine[i].mesh = meshFilters[i].sharedMesh;
//			combine[i].transform = Matrix4x4.TRS(gameObject.transform.InverseTransformPoint(transforms[i].position), Quaternion.Inverse(gameObject.transform.rotation), Vector3.one);
//			meshFilters[i].gameObject.SetActive(false);
//		
//			DestroyImmediate(collider[i]);
//        }	
//		
//		if (GetComponent(typeof(SkinnedMeshRenderer)) == null)
//		{
//			gameObject.AddComponent(typeof(SkinnedMeshRenderer));
//		}
//		
//			
//		
//		
//		
//			
////        transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
////        transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
//				
//		gameObject.AddComponent<MeshCollider>();
//					
//        transform.gameObject.SetActive(true);
//		
//		//We now look for all the children objects and delete them from the final result
//		var children = new List<GameObject>();
//    		foreach (Transform child in transform) children.Add(child.gameObject);
//    		children.ForEach(child => DestroyImmediate(child));
//		
//		//We set the combined state to true
//		this.isCombined = true;
//	}