//DataManager.cs
//Created on 01/05/2014
//Last Updated on 14/05/2014
//Version 0.82
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//This class is used for testing purposes and has no real use at this time

using UnityEngine;
using UnityEditor;
using System.Collections;

//	public void BloxDataManager(bool val)
//	{
//		DataManager manager = new DataManager(MyBloxID, MyBloxMeshCategoryID, MyBloxMeshID, mystartPosition, transform, EditorApplication.currentScene.ToString());
//		Debug.Log("test 1 " + this.MyBloxID + " " + MyBloxMeshCategoryID + " " + MyBloxMeshID + " " + mystartPosition + "Local Position: " + transform.localPosition + " " + EditorApplication.currentScene.ToString());
//		manager.CheckBloxSaveStatus(val);
//		
//		MyBloxID = manager.MyBloxId;
//		MyBloxMeshCategoryID = manager.MyMeshCategory;
//		MyBloxMeshID = manager.MyMeshId;		
//	}	

//this file is no longer used to save blox data (Beta only)
public class DataManager {
	
	private bool isChildValue;
	private int myBloxIdValue;
	private int myMeshCategoryValue;
	private int MyMeshIdValue;
	private Vector3 myStartPositionValue;
	private Transform myTransformValue;
	private string currentSceneNameValue;
	
	public DataManager(bool ischild, int mybloxid, int mymeshcategory, int mymeshid, Vector3 mystartposition, Transform mytransform, string currentscenename)
	{
		//Debug.Log(ischild + " " +  mybloxid + " " + mymeshcategory + " " + mymeshid + " " + mystartposition + " " + mytransform.localPosition + " " + currentscenename);
		
		IsChild = ischild;
		MyBloxId = mybloxid;
		MyMeshCategory = mymeshcategory;
		MyMeshId = mymeshid;
		MyStartPosition = mystartposition;
		MyTransform = mytransform;
		CurrentSceneName = currentscenename;
	}
	
	public bool IsChild
	{
		get{ return isChildValue; }
		set{ isChildValue = value; }
	}
	
	public int MyBloxId
	{
		get{ return myBloxIdValue; }
		set{ myBloxIdValue = value; }
	}
	
	public int MyMeshCategory
	{
		get{ return myMeshCategoryValue; }
		set{ myMeshCategoryValue = value; }
	}
	
	public int MyMeshId
	{
		get{ return MyMeshIdValue; }
		set{ MyMeshIdValue = value; }
	}
	
	public Vector3 MyStartPosition
	{
		get{ return myStartPositionValue; }
		set{ myStartPositionValue = value; }
	}
	
	public Transform MyTransform
	{
		get{ return myTransformValue; }
		set{ myTransformValue = value; }
	}
	
	public string CurrentSceneName
	{
		get{ return currentSceneNameValue; }
		set{ currentSceneNameValue = value; }
	}
	
	public void CheckBloxSaveStatus(bool overwritesave)
	{
		var data = CheckForData();
		
		if (data)
		{
			Load();
		}
		else
		{			
			if (MyStartPosition != MyTransform.position)
			{
				Delete();
			}
			
			Save();
		}
	}
	
	private bool foundData = false;
	private bool CheckForData()
	{
		if (EditorPrefs.HasKey("BloxEditorSave" + CurrentSceneName + MyTransform.ToString()))
		{
			foundData = true;
		}
		else
		{
			foundData = false;
		}
		
		return foundData;
	}
	
	private void Load()
	{
		if (IsChild)
		{
			
		}
		else
		{
			
		}
	}
	
	private void Save()
	{
		if (IsChild)
		{
			//Debug.Log("Saving Child Data: BloxEditorSave" + CurrentSceneName + MyTransform.position.ToString() + "_" + MyBloxId + "_" + MyMeshCategory + "_" + MyMeshId);
		}
		else
		{
			//Debug.Log("Saving Master Data: BloxEditorSave" + CurrentSceneName + MyTransform.position.ToString() + "_" + MyBloxId + "_" + MyMeshCategory + "_" + MyMeshId);
			//EditorPrefs.SetString("BloxEditorSave" + CurrentSceneName + MyTransform.position.ToString(), MyBloxId + "_" + MyMeshCategory + "_" + MyMeshId);
		}
	}
	
	private void Delete()
	{
		if (IsChild)
		{
			
		}
		else
		{
//			Debug.Log("I found a saved key but the location has changed / Deleting old key");
			
			//EditorPrefs.DeleteKey("BloxEditorSave" + CurrentSceneName + MyStartPosition);
		}
	}
	
	//We check to see if we have a save or not, but can overrule it by stating True or False when checking
//	public void CheckBloxSaveStatus(bool overwritesave)
//	{
//		if (overwritesave != true)
//		{			
//			if (EditorPrefs.HasKey("BloxEditorSave" + CurrentSceneName + MyTransform.ToString()))
//			{
//				Debug.Log("Loading data from prefs " + CurrentSceneName);
//				BloxLoad();
//			}
//			else
//			{
//				Debug.Log("Overwrite has been called / force saving data: " + CurrentSceneName);
//				BloxSave();
//			}
//		}
//		else
//		{
//			Debug.Log("Saving data :" + CurrentSceneName);
//			BloxSave();
//		}
//		
//		BloxLoad();
//	}
	
	//We save our blox to a string
//	private void BloxSave()
//	{		
//		if (MyTransform.position != MyStartPosition)
//		{
//			BloxDeleteSave();
//		}
//		
//		EditorPrefs.SetString("BloxEditorSave" + CurrentSceneName + MyTransform.position.ToString(), MyBloxId + "_" + MyMeshCategory + "_" + MyMeshId);
//	}
//	
//	//Delete old Blox data if i somehow had moved it while editing
//	private void BloxDeleteSave()
//	{		
//		if (EditorPrefs.HasKey("BloxEditorSave" + CurrentSceneName + MyStartPosition))
//		{
//			EditorPrefs.DeleteKey("BloxEditorSave" + CurrentSceneName + MyStartPosition);
//		}
//	}
//	
//	//Automatic load on level or editor start, but just once
//	private void BloxLoad()
//	{
//		Debug.Log("Loading data from prefs " + CurrentSceneName);
//		
//		string myData = EditorPrefs.GetString("BloxEditorSave" + CurrentSceneName + MyTransform.position.ToString());
//		string[] splitString = myData.Split(char.Parse("_"));
//			
//		MyBloxId = int.Parse(splitString[0]);
//		MyMeshCategory = int.Parse(splitString[1]);
//		MyMeshId = int.Parse(splitString[2]); 
//	}
}











//public void CheckBloxSaveStatus(bool overwritesave)
//	{
//		if (IsChild)
//		{
//			Debug.Log("Writng Child to Prefs");
//		}
//		else
//		{
//			Debug.Log("Writng Master to Prefs");
//			
//			EditorPrefs.SetString("BloxEditorSave" + CurrentSceneName + MyTransform.position.ToString(), MyBloxId + "_" + MyMeshCategory + "_" + MyMeshId);
//		}		
//	}
//	
//	private bool foundLoad = false;
//	public bool CheckBloxLoadStatus()
//	{
//		if (IsChild)
//		{
//		
//		}
//		else
//		{
//			//If we find a savepoint, load it
//			if (EditorPrefs.HasKey("BloxEditorSave" + CurrentSceneName + MyTransform.ToString()))
//			{
//				Debug.Log("Loading Master from prefs " + CurrentSceneName);
//		
//				string myData = EditorPrefs.GetString("BloxEditorSave" + CurrentSceneName + MyTransform.position.ToString());
//				string[] splitString = myData.Split(char.Parse("_"));
//			
//				MyBloxId = int.Parse(splitString[0]);
//				MyMeshCategory = int.Parse(splitString[1]);
//				MyMeshId = int.Parse(splitString[2]);
//			}
//			else //we save it
//			{
//				CheckBloxSaveStatus();
//				
//			}
//		}
//		
//		return foundLoad;
//	}