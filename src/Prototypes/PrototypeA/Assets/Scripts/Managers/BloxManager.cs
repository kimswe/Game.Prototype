//BloxManager.cs
//Created on 01/05/2014
//Last Updated on 20/05/2014
//Version 0.84
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//This class is used to create the editor window and for manipulating the blox texture, spawning.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class BloxManager : EditorWindow { 
		
	GameObject __BloxManager;
	GameObject[] allBlox;
	Object prefabRoot;
	private Blox thisBlox;	
		
	[MenuItem("Easy Blox/Blox Manager")]
	
	static void Init()
	{
		BloxManager window = (BloxManager)EditorWindow.GetWindow (typeof (BloxManager));
		window.name = "Easy Blox Manager";
	}	
	
	private void PlaceNewBlox(float pos_x,float pos_y,float pos_z)
	{
		GameObject obj = null;
		prefabRoot = (GameObject)Instantiate(Resources.Load("Prefabs/Blox"));
								
		if (prefabRoot != null)
		{	
			PrefabUtility.InstantiatePrefab(prefabRoot);
			if (thisBlox.name == "Completed")
			{
				obj = GameObject.Find("Blox(Clone)");
				obj.name = "Master";
			}
			else
			{
				obj = GameObject.Find("Blox(Clone)");
				obj.name = "Selected";
			}
			
			Selection.activeGameObject = obj;
		}
		else 
		{
			Debug.Log("Could not place a new Blox,does the directory Prefabs/Blox/ contain the prefab called Blox?");
		}		
		
			if (thisBlox.name != "Master")
				thisBlox.name = "Completed";
		
		Transform root = thisBlox.transform.root;
						
		SetNewBloxPosition(root,obj,pos_x,pos_y,pos_z);
	}
	
	private void SetNewBloxPosition(Transform root, GameObject obj, float pos_x,float pos_y,float pos_z)
	{
		Blox newBlox = (Blox)obj.GetComponent<Blox>();
		
		//we copy the current attributes and apply them to the new blox object and then made a call to have it rendered
		newBlox.MyBloxMeshCategoryID = thisBlox.MyBloxMeshCategoryID;				
		newBlox.MyBloxMeshID = thisBlox.MyBloxMeshID;
		newBlox.DoRender = true;
		newBlox.RenderMyTexture();
		
		//Check to see if we have a root called Master and if we do, we assign this new blox as a child object
		if (root.name == "Master")
			newBlox.transform.parent = root;
		//we count all the blox in the scene and assign it a new ID
		newBlox.MyBloxID = GameObject.FindGameObjectsWithTag("Blox").Length;		
		//if this blox has been turned into a wall, depending on that we may need to shift the blox height
		if (thisBlox.IsWall != true)
			newBlox.SetMyXPosition(thisBlox.transform.position.x + pos_x, thisBlox.transform.position.y + pos_y, thisBlox.transform.position.z + pos_z);
		else
			newBlox.SetMyXPosition(thisBlox.transform.position.x + pos_x, thisBlox.transform.position.y + pos_y -1.5f , thisBlox.transform.position.z + pos_z);																		
	}
	
	private void GoBackOrGoNext(string sign)
	{
		var cnt = 1;		
		allBlox = GameObject.FindGameObjectsWithTag("Blox");
						
		foreach(GameObject blox in allBlox)
		{		
			bool bloxIdToFind;
			
			if (sign == "+")
				bloxIdToFind = FindThisBlox(thisBlox.MyBloxID + cnt);	//returns true or false as a check to see if the next blox actually excists, 
			else 														//if it does not we need to add +1 to the counter cnt
				bloxIdToFind = FindThisBlox(thisBlox.MyBloxID - cnt);
			
			//If we cant find a block with the ID we searched for we add +1 to the counter and we look again, until we find it
			if (bloxIdToFind == false)
			{
				cnt++;
			}
			
			if (sign == "+")
			{
				if (blox.GetComponent<Blox>().MyBloxID == (thisBlox.MyBloxID + cnt))
					Selection.activeGameObject = blox;
			}
			else
			{
				if (blox.GetComponent<Blox>().MyBloxID == (thisBlox.MyBloxID - cnt))
					Selection.activeGameObject = blox;
			}
			
			BloxSelection(blox);
		}			
	}
	
	//We look for a blox to excist to transit towards, true it excists, false it does not
	private bool FindThisBlox(int id)
	{
		allBlox = GameObject.FindGameObjectsWithTag("Blox");
		
		foreach(GameObject blox in allBlox)
		{			
			if (blox.GetComponent<Blox>().MyBloxID == id)
			{
				return true;
			}			
		}
		
		return false;
	}		
	
	private void BloxSelection(GameObject cube)
	{
		if (thisBlox.name != "Master")
			thisBlox.name = "Completed";
		
		if (cube.name != "Master")
			cube.name = "Selected";
	}
	
	//Everything about OnGUI
	void OnGUI()
	{
		GUILayout.BeginVertical("Box");
			BloxControls();
			Options();
			BloxEditor();		
		GUILayout.EndVertical();
	}
	
	private void CheckForBloxManager()
	{
		var blmanager = GameObject.Find("__BloxManager");
		
		if (blmanager != true)
		{
			__BloxManager = (GameObject)Instantiate(Resources.Load("Prefabs/__BloxManager"));
			__BloxManager.name = "__BloxManager";
		}
	}
	
	private void Options()
	{
		GUILayout.BeginHorizontal("Box");
			GUILayout.Label("Blox Options");
		GUILayout.EndHorizontal();
		
		if (GUILayout.Button("Player Spawn Location", GUILayout.Width(260)))
		{
			if (thisBlox != null)
			{
				CheckForOtherspawnPoints();	//reset all spawnpoints
				
				thisBlox.IsSpawnPoint = !thisBlox.IsSpawnPoint;
			}			
		}
	}
	
	//##On Gui related items
	private void BloxControls()
	{
		//We place a new Master Blox if none excists
		if (GUILayout.Button("Place Master Blox", GUILayout.Width(260)))
		{					
			//We check to see of this scene has a BloxManager before proceeding
			CheckForBloxManager();
			
			var masterBlox = GameObject.Find("Master");
			
			if (masterBlox != null)
			{
				Debug.Log("We already have a Master Blox on this scene, cannot spawn a second");
			}
			else
			{
				prefabRoot = (GameObject)Instantiate(Resources.Load("Prefabs/Blox"));
			
				if (prefabRoot != null)
				{	
					PrefabUtility.InstantiatePrefab(prefabRoot);
					GameObject.Find("Blox(Clone)").name = "Master";
					Selection.activeGameObject = GameObject.Find("Master");
					Selection.activeGameObject.GetComponent<Blox>().RenderMyTexture();
				}
				else
				{
					Debug.Log("Does the directory Resources/Prefabs/ contain a Prefab called Blox?");
				}
			}
		}
	}	
	
	private void BloxEditor()
	{		
		if (Selection.activeGameObject)
		{
			var myGameObject = Selection.activeGameObject;
			thisBlox = myGameObject.GetComponent<Blox>();
		}
		
		GUILayout.Label("Go back or next -> blox ");
		if (thisBlox != null)
		{
			GUILayout.BeginHorizontal("Box");
				
				if (GUILayout.Button("Back"))
				{
					GoBackOrGoNext("-");
				}
			
				GUILayout.Label("  ID  " + thisBlox.MyBloxID,GUILayout.Width(60));	
			
				if (GUILayout.Button("Next"))
				{
					GoBackOrGoNext("+");
				}		
			
			GUILayout.EndHorizontal();
					
			//GUILayout.BeginHorizontal("Box");
			//Disabled, for testing purposes only
			//	EditorGUILayout.Toggle(thisBlox._xPositive);
			//	EditorGUILayout.Toggle(thisBlox._xNegative);
			//	EditorGUILayout.Toggle(thisBlox._zPositive);
			//	EditorGUILayout.Toggle(thisBlox._zNegative);
			//GUILayout.EndHorizontal();
			
			GUILayout.BeginVertical("Box");
			GUILayout.Label("New Blox Placement Menu");
			
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("+"))
				{	
					if (thisBlox._xPositive == false)
					{
						PlaceNewBlox(+1,0,0);
					}
					else
					{
						Debug.Log ("There is already a Blox at that location!");
					}
				}
			
				GUILayout.Label("  X-Axis  ",GUILayout.Width(60));
				
				if (GUILayout.Button("-"))
				{	
					if (thisBlox._xNegative == false)
					{
						PlaceNewBlox(-1,0,0);
					}
					else
					{
						Debug.Log ("There is already a Blox at that location!");
					}
				}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("+"))
				{	
					if (thisBlox._zPositive == false)
					{
						PlaceNewBlox(0,0,+1);
					}
					else
					{
						Debug.Log ("There is already a Blox at that location!");
					}
				}
			
				GUILayout.Label("  Z-Axis  ",GUILayout.Width(60));
				
				if (GUILayout.Button("-"))
				{	
					if (thisBlox._zNegative == false)
					{
						PlaceNewBlox(0,0,-1);
					}
					else
					{
						Debug.Log ("There is already a Blox at that location!");
					}
				}
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			
			//Mesh Renderer GUI parts
			GUILayout.BeginVertical("Box");
				GUILayout.BeginHorizontal("Box");
					if (GUILayout.Button("<<",GUILayout.Width(25)))
					{
						if (thisBlox.MyBloxMeshCategoryID > 0)
							thisBlox.MyBloxMeshCategoryID--;
				
						thisBlox.MyBloxMeshID = 0;
						thisBlox.DoRender = true;
						thisBlox.RenderMyTexture();
					}
				
					GUILayout.Box(thisBlox.texManager.GetClassName(thisBlox.MyBloxMeshCategoryID),GUILayout.Width(175));		
					if (GUILayout.Button(">>",GUILayout.Width(25)))
					{
						if (thisBlox.MyBloxMeshCategoryID < thisBlox.texManager.arrayNames.Length -1)
							thisBlox.MyBloxMeshCategoryID++;
				
						thisBlox.MyBloxMeshID = 0;
						thisBlox.DoRender = true;
						thisBlox.RenderMyTexture();
					}
			
				GUILayout.EndVertical();				
			
				//Mesh Selection
				GUILayout.BeginHorizontal("Box");
					
						if (GUILayout.Button("<<",GUILayout.Height(100)))
						{
							if (thisBlox.MyBloxMeshID > 0)
								thisBlox.MyBloxMeshID--;	
							thisBlox.DoRender = true;
							thisBlox.RenderMyTexture();
						}	
				
					GUILayout.Box(thisBlox.texManager.GetTexture(thisBlox.MyBloxMeshCategoryID,thisBlox.MyBloxMeshID),GUILayout.Height(100),GUILayout.Width(100));
				
						if (GUILayout.Button(">>", GUILayout.Height(100)))
						{
							if (thisBlox.MyBloxMeshID < thisBlox.texManager.GetArrayLength(thisBlox.MyBloxMeshCategoryID,thisBlox.MyBloxMeshID))
								thisBlox.MyBloxMeshID++;	
							thisBlox.DoRender = true;
							thisBlox.RenderMyTexture();
						}							
				GUILayout.EndHorizontal();		
			GUILayout.EndHorizontal();
			
			BloxCombine();				
			}
		}
	
		//We look through allblox to see if there are any cubes that pose as a spawnpoint for the player
		private void CheckForOtherspawnPoints()
		{
			allBlox = GameObject.FindGameObjectsWithTag("Blox");
		
			foreach(GameObject blox in allBlox)
			{			
				blox.GetComponent<Blox>().IsSpawnPoint = false;			
			}			
		}
	
		private void BloxCombine()
		{
			GUILayout.BeginHorizontal("Box");
			if (GUILayout.Button("Combine Bloxes"))
			{							
				Selection.activeGameObject = GameObject.Find("Master");
			
				if (thisBlox.name == "Master")
				{	
					thisBlox.name = "Completed";
					thisBlox.CombineMeshes();					
				}				 
			}		
			GUILayout.EndHorizontal();
		}
}



















//
//
//private void SetNewBloxPosition(GameObject obj, float pos_x,float pos_y,float pos_z)
//	{
//		Blox newBlox = null;
//		GameObject selectedBlox = null;		
//		
////		if (thisBlox.newMaster != true)
////		{
//			//selectedBlox = GameObject.Find("Selected");		
//		
////		}
////		else
////		{			
////			selectedBlox = GameObject.Find("Master");
////			thisBlox.newMaster = false;			
////		}				
//		
//		Selection.activeGameObject = selectedBlox;
//		newBlox = (Blox)selectedBlox.GetComponent<Blox>();
//		
//		//Todo: fix transition exception in another way
//		if (GameObject.Find("Master"))
//			newBlox.transform.parent = GameObject.Find("Master").transform;
//																
//		newBlox.MyBloxID = GameObject.FindGameObjectsWithTag("Blox").Length;		//we count all the blox in the scene and assign it a new ID
//		
//		if (thisBlox.isWall != true)
//			newBlox.SetMyXPosition(thisBlox.transform.position.x + pos_x, thisBlox.transform.position.y + pos_y, thisBlox.transform.position.z + pos_z);		//We set our location based on the New Blox
//		else
//			newBlox.SetMyXPosition(thisBlox.transform.position.x + pos_x, thisBlox.transform.position.y + pos_y -1.5f , thisBlox.transform.position.z + pos_z);
//		
//		newBlox.MyBloxMeshCategoryID = thisBlox.MyBloxMeshCategoryID;				//We give it our current mesh Category
//		newBlox.MyBloxMeshID = thisBlox.MyBloxMeshID;								//And we also give it our mesh ID
//		newBlox.RenderMyTexture();													//We call to render our textur				
//	}