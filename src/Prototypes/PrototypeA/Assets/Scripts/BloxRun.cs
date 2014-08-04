//BloxRun.cs
//Created on 01/05/2014
//Last Updated on 14/05/2014
//Version 0.85
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//Simple class for spawning the player and more in the future

using UnityEngine;
using System.Collections;

//We use this file to initialise anything that needs to be done in the game itself

public class BloxRun : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		this.GetComponent<Blox>().SpawnPlayer();
	}
}
