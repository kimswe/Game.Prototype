//BloxEditor.cs
//Created on 01/05/2014
//Last Updated on 21/05/2014
//Version 0.86
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//This class handles all the on Blox options for transforming, for now just works with making walls and positioning in world space

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(Blox))]
public class BloxEditor : Editor {
	
	public TextureManager texManager;
	
	private Blox thisBlox;
	UnityEngine.Object prefabRoot;
	Transform mytransform;	
		
	void Awake()
	{	
		var go = GameObject.Find("__BloxManager");
		texManager = go.GetComponent<TextureManager>();
				
		thisBlox = ((Blox)target);			//We get a referennce to our own Blox script
		
		if (thisBlox.IsFinished == false)
		{			
			SetNameOnSelection();			//We set our name on cube selection
			
			if (thisBlox != null)
				TargetBloxAwake();
		}		
	}	
	
	//Todo: make this a little more elegant
	private void SetNameOnSelection()
	{		
		if (thisBlox.name == "Completed Blox")
		{
			var go = GameObject.Find("Selected Blox");
				
			if (go != null)
			{
				go.GetComponent<Blox>().name = "Completed Blox";
			}				
				
			thisBlox.name = "Selected Blox";
		}
	}
	
	private void TargetBloxAwake()
	{		
		//Check all 4 surroundins around the Blox
		thisBlox._zPositive = CheckSuroundings(Vector3.forward);	//Z axis positive
		thisBlox._zNegative = CheckSuroundings(Vector3.back);		//Z axis negative
		thisBlox._xPositive = CheckSuroundings(Vector3.right);		//X axis positive
		thisBlox._xNegative = CheckSuroundings(Vector3.left);		//X axis negative		
	}
	
	private bool CheckSuroundings(Vector3 direction)
	{
		var myTransForm = thisBlox.transform;
		
		RaycastHit hit;
		Ray hitRay = new Ray(myTransForm.position,direction);
		
		Debug.DrawRay(myTransForm.position,direction);
		
		if (Physics.Raycast(hitRay, out hit, 1))
		{		
			return true;			
		}
		else
		{
			return false;
		}		
	}
	
	//On GUI stuff
	public override void OnInspectorGUI()
	{
		Gui();
		MoveBloxAround();
		ScaleBlox();
		ChangeBloxHeight();
		StretchBloxLenghtAndhWidth();
		ScaleTexture();
	}
	
	//to stuff here that we only want to be availble on the blox itself
	private void Gui()
	{
		if (thisBlox.IsCombined)
		{
			if (GUILayout.Button("Turn into Wall", GUILayout.Width(260)))
			{					
				ReDoScaleTo(0,3,0);
			}
		}
	}
	
	private void ScaleBlox()
	{
		var roundedBloxValue = RoundOfDecimal(texManager.BloxScale);
				
		GUILayout.BeginVertical("Box");
		GUILayout.Label("Scale Blox Size");		
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("Adjust"))
				{	
					AdjustBloxScale("+");			
				}
		
				if (GUILayout.Button("+"))
				{
					texManager.BloxScale += 0.1f;
				}
		
					GUILayout.Label("" + roundedBloxValue);	
				
				if (GUILayout.Button("-"))
				{
					texManager.BloxScale -= 0.1f;
				}
					
				if (GUILayout.Button("Adjust"))
				{	
					if (texManager.BloxScale > 0.0f)
						AdjustBloxScale("-");
				}
			GUILayout.EndHorizontal();			
		GUILayout.EndVertical();
	}
	
	private void ChangeBloxHeight()
	{		
		var myBloxHeight = texManager.BloxHeight;
		
		GUILayout.BeginVertical("Box");
		GUILayout.Label("Change Blox Height");		
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("Adjust"))
				{	
					ChangeHeight("+");
				}	
		
				if (GUILayout.Button("+"))
				{
					texManager.BloxHeight += 0.1f;
				}
		
					GUILayout.Label("" + myBloxHeight);	
				
				if (GUILayout.Button("-"))
				{
					texManager.BloxHeight -= 0.1f;
				}		
					
				if (GUILayout.Button("Adjust"))
				{	
					ChangeHeight("-");
				}
			GUILayout.EndHorizontal();			
		GUILayout.EndVertical();
	}
	
	private void StretchBloxLenghtAndhWidth()
	{		
		var myBloxStretch = texManager.BloxStretch;
		var myStretchSide = texManager.BloxStretchXorZ;
		
		GUILayout.BeginVertical("Box");
		
		GUILayout.BeginHorizontal("Box");
		GUILayout.Label("Stretch Blox X or Z");				
				if (GUILayout.Button("X"))
				{
					texManager.BloxStretchXorZ = true;
				}
		
					GUILayout.Label("" + GetStretchBool());	
				
				if (GUILayout.Button("Y"))
				{
					texManager.BloxStretchXorZ = false;
				}		
			GUILayout.EndHorizontal();
		
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("Adjust"))
				{	
					StretchBlox("+",myStretchSide);
				}	
		
				if (GUILayout.Button("+"))
				{
					texManager.BloxStretch += 0.1f;
				}
		
					GUILayout.Label("" + myBloxStretch);	
				
				if (GUILayout.Button("-"))
				{
					texManager.BloxStretch -= 0.1f;
				}		
					
				if (GUILayout.Button("Adjust"))
				{	
					StretchBlox("-",myStretchSide);
				}
			GUILayout.EndHorizontal();			
		GUILayout.EndVertical();
	}	
	
	private void ScaleTexture()
	{
		GUILayout.BeginVertical("Box");
		GUILayout.Label("Scale Texture");		
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("Scale Y+"))
				{	
					thisBlox.renderer.sharedMaterial.mainTextureScale = new Vector2(thisBlox.renderer.sharedMaterial.mainTextureScale.x, thisBlox.renderer.sharedMaterial.mainTextureScale.y +0.1f);					
				}
					
				if (GUILayout.Button("Scale Y-"))
				{	
					thisBlox.renderer.sharedMaterial.mainTextureScale = new Vector2(thisBlox.renderer.sharedMaterial.mainTextureScale.x, thisBlox.renderer.sharedMaterial.mainTextureScale.y -0.1f);
				}
			GUILayout.EndHorizontal();
		
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("Scale X+"))
				{	
					thisBlox.renderer.sharedMaterial.mainTextureScale = new Vector2(thisBlox.renderer.sharedMaterial.mainTextureScale.x +0.1f, thisBlox.renderer.sharedMaterial.mainTextureScale.y);
				}
					
				if (GUILayout.Button("Scale X-"))
				{	
					thisBlox.renderer.sharedMaterial.mainTextureScale = new Vector2(thisBlox.renderer.sharedMaterial.mainTextureScale.x -0.1f, thisBlox.renderer.sharedMaterial.mainTextureScale.y);
				}
			GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}
		
	private void MoveBloxAround()
	{
		var roundedBloxValue = RoundOfDecimal(texManager.BloxMove);
		
		GUILayout.BeginVertical("Box");
		
		GUILayout.BeginHorizontal("Box");
			GUILayout.Label("World Space Movement");		
		
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("+"))
				{
					texManager.BloxMove += 0.1f;
				}
		
					GUILayout.Label("" + roundedBloxValue);	
				
				if (GUILayout.Button("-"))
				{
					texManager.BloxMove -= 0.1f;
				}		
			GUILayout.EndHorizontal();
		GUILayout.EndHorizontal();		
		
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("+"))
				{	
					MoveBloxInWorldSpace(+texManager.BloxMove,0,0);
				}
					
				GUILayout.Label("  X-Axis  ",GUILayout.Width(60));
				
				if (GUILayout.Button("-"))
				{	
					MoveBloxInWorldSpace(-texManager.BloxMove,0,0);
				}			
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal("Box");
				if (GUILayout.Button("+"))
				{	
					MoveBloxInWorldSpace(0,+texManager.BloxMove,0);
				}
					
				GUILayout.Label("  Y-Axis  ",GUILayout.Width(60));
				
				if (GUILayout.Button("-"))
				{	
					MoveBloxInWorldSpace(0,-texManager.BloxMove,0);
				}			
			GUILayout.EndHorizontal();		
			
			GUILayout.BeginHorizontal("Box");		
				if (GUILayout.Button("+"))
				{	
					MoveBloxInWorldSpace(0,0,+texManager.BloxMove);
				}
				
				GUILayout.Label("  Z-Axis  ",GUILayout.Width(60));
			
				if (GUILayout.Button("-"))
				{	
					MoveBloxInWorldSpace(0,0,-texManager.BloxMove);
				}
			GUILayout.EndHorizontal();
		
		GUILayout.EndVertical();
	}
	
	//We turn the object into a wall
	private void ReDoScaleTo(float pos_x,float pos_y,float pos_z)
	{		
		var calculateY = pos_y/2;
		var calculateScale = (pos_y/3)*4;
		
		thisBlox.renderer.sharedMaterial.mainTextureScale = new Vector2(1, calculateScale);		
		thisBlox.transform.localScale = new Vector3(thisBlox.transform.localScale.x,thisBlox.transform.localScale.y + pos_y ,thisBlox.transform.localScale.z);
		thisBlox.transform.localPosition = new Vector3(thisBlox.transform.localPosition.x, thisBlox.transform.localPosition.y + calculateY, thisBlox.transform.localPosition.z);
		
		thisBlox.IsWall = true;
	}
	
	private void MoveBloxInWorldSpace(float pos_x,float pos_y,float pos_z)
	{
		thisBlox.transform.position = new Vector3(thisBlox.transform.position.x + pos_x, thisBlox.transform.position.y + pos_y, thisBlox.transform.position.z + pos_z);
	}
	
	private void AdjustBloxScale(string s)
	{
		if (s == "+")
			thisBlox.transform.localScale = new Vector3(thisBlox.transform.localScale.x + texManager.BloxScale,thisBlox.transform.localScale.y,thisBlox.transform.localScale.z + texManager.BloxScale);	
		else
			thisBlox.transform.localScale = new Vector3(thisBlox.transform.localScale.x - texManager.BloxScale,thisBlox.transform.localScale.y,thisBlox.transform.localScale.z - texManager.BloxScale);
	}
	
	private void ChangeHeight(string s)
	{
		if (s == "+")
		{
			thisBlox.transform.localScale = new Vector3(thisBlox.transform.localScale.x,thisBlox.transform.localScale.y + texManager.BloxHeight ,thisBlox.transform.localScale.z);
			thisBlox.transform.localPosition = new Vector3(thisBlox.transform.localPosition.x, thisBlox.transform.localPosition.y + texManager.BloxHeight/2, thisBlox.transform.localPosition.z);
		}
		else
		{
			thisBlox.transform.localScale = new Vector3(thisBlox.transform.localScale.x,thisBlox.transform.localScale.y - texManager.BloxHeight ,thisBlox.transform.localScale.z);
			thisBlox.transform.localPosition = new Vector3(thisBlox.transform.localPosition.x, thisBlox.transform.localPosition.y - texManager.BloxHeight/2, thisBlox.transform.localPosition.z);
		}		
	}
	
	private void StretchBlox(string s, bool xz)
	{
		var posX = 0.0f;
		var posZ = 0.0f;
		
		if (xz == true)
			posX = texManager.BloxStretch;
		else
			posZ = texManager.BloxStretch;
		
		if (s == "+")
		{
			thisBlox.transform.localScale = new Vector3(thisBlox.transform.localScale.x + posX,thisBlox.transform.localScale.y,thisBlox.transform.localScale.z + posZ);
			thisBlox.transform.localPosition = new Vector3(thisBlox.transform.localPosition.x + posX/2, thisBlox.transform.localPosition.y, thisBlox.transform.localPosition.z + posZ/2);
		}
		else
		{
			thisBlox.transform.localScale = new Vector3(thisBlox.transform.localScale.x - posX,thisBlox.transform.localScale.y,thisBlox.transform.localScale.z - posZ);
			thisBlox.transform.localPosition = new Vector3(thisBlox.transform.localPosition.x - posX/2, thisBlox.transform.localPosition.y, thisBlox.transform.localPosition.z - posZ/2);
		}		
	}
	
	private float RoundOfDecimal(float v)
	{
		return (float)(System.Math.Round(v, 1, MidpointRounding.ToEven));
	}
	
	private string GetStretchBool()
	{
		if (texManager.BloxStretchXorZ == true)
			return "X";
		
		return "Y";
	}
}
