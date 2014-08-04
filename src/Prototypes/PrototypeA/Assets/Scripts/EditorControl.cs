//EditorControl.cs
//Created on 01/05/2014
//Last Updated on 14/05/2014
//Version 0.85
//Weyns Peter

//Email any bugs to projecteasyblox@gemail.com or request features

//Comments:
//This class is not used yet but wil handle gizmos in the future, when i get around to adding gizmos

using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class EditorControl : MonoBehaviour {

	private Camera _sceneCamera = null;
	
	void Awake()
	{
		
		
//		if (Camera.current != null)
//			mySceneCamera = Camera.current.transform;
		
		
	}
	
	void Update()
	{
		CheckCameras();
	}
	
	private void CheckCameras()
	{
//		if (Camera.main == null)
//    	{
//    		Debug.Log("Main camera not found.");
//	
//			return;
//    	}
		
		if (_sceneCamera == null)
        {
            if (Camera.current == null || Camera.current.name != "SceneCamera")
            {
                Debug.Log("Scene camera not selected. First click on Scene tab before calling.");

                return;
            }
			
			Debug.Log("Found Scene Camera.");
            _sceneCamera = Camera.current;
			
//			if (Camera.current != null)
//			this.transform = _sceneCamera.transform;
        }
	}
	
	// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.



 
public void OnDrawGizmos(){
		
	var gizmoSize= 0.5f;
	var spherePoint= true;
	var sphereColor = new Color(0, 0, 0, 0.1f); 
	var sphereScale= 0.1f; 
		
		
	if (this.enabled == false) { return; }
 
	if (spherePoint)
	{
		Gizmos.color = sphereColor; 
		Gizmos.DrawSphere (transform.position, sphereScale * gizmoSize);
	}
 
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (transform.position, transform.position + (transform.forward * gizmoSize * 1.0f));
		Gizmos.DrawLine (transform.position + (transform.forward * gizmoSize * 1.0f), (transform.position + (transform.forward * gizmoSize * 0.8f) + (transform.up * gizmoSize * 0.2f)));
		Gizmos.DrawLine (transform.position + (transform.forward * gizmoSize * 1.0f), (transform.position + (transform.forward * gizmoSize * 0.8f) + (transform.up * gizmoSize * -0.2f)));
		Gizmos.DrawLine (transform.position + (transform.forward * gizmoSize * 1.0f), (transform.position + (transform.forward * gizmoSize * 0.8f) + (transform.right * gizmoSize * 0.2f)));
		Gizmos.DrawLine (transform.position + (transform.forward * gizmoSize * 1.0f), (transform.position + (transform.forward * gizmoSize * 0.8f) + (transform.right * gizmoSize * -0.2f)));
	 
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + (transform.up * gizmoSize));
		Gizmos.DrawLine (transform.position + (transform.up * gizmoSize * 1.0f), (transform.position + (transform.up * gizmoSize * 0.8f) + (transform.forward * gizmoSize * 0.2f)));
		Gizmos.DrawLine (transform.position + (transform.up * gizmoSize * 1.0f), (transform.position + (transform.up * gizmoSize * 0.8f) + (transform.forward * gizmoSize * -0.2f)));
		Gizmos.DrawLine (transform.position + (transform.up * gizmoSize * 1.0f), (transform.position + (transform.up * gizmoSize * 0.8f) + (transform.right * gizmoSize * 0.2f)));
		Gizmos.DrawLine (transform.position + (transform.up * gizmoSize * 1.0f), (transform.position + (transform.up * gizmoSize * 0.8f) + (transform.right * gizmoSize * -0.2f)));
	 
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + (transform.right * gizmoSize));
		Gizmos.DrawLine (transform.position + (transform.right * gizmoSize * 1.0f), (transform.position + (transform.right * gizmoSize * 0.8f) + (transform.up * gizmoSize * 0.2f)));
		Gizmos.DrawLine (transform.position + (transform.right * gizmoSize * 1.0f), (transform.position + (transform.right * gizmoSize * 0.8f) + (transform.up * gizmoSize * -0.2f)));
		Gizmos.DrawLine (transform.position + (transform.right * gizmoSize * 1.0f), (transform.position + (transform.right * gizmoSize * 0.8f) + (transform.forward * gizmoSize * 0.2f)));
		Gizmos.DrawLine (transform.position + (transform.right * gizmoSize * 1.0f), (transform.position + (transform.right * gizmoSize * 0.8f) + (transform.forward * gizmoSize * -0.2f)));	
	}
}
	
	
//	 [DrawGizmo (GizmoType.Selected | GizmoType.Pickable)]
//	static void RenderLightGizmo (Light light, GizmoType gizmoType) {
//            Vector3 position = light.transform.position;
//            // Draw the light icon
//            // (A bit above the one drawn by the builtin light gizmo renderer)
//            Gizmos.DrawIcon (position + Vector3.up, "Light Gizmo.tiff");
//            // Are we selected? Draw a solid sphere surrounding the light
//            if ((gizmoType & GizmoType.SelectedOrChild) != 0) {
//                // Indicate that this is the active object by using a brighter color.
//                if ((gizmoType & GizmoType.Active) != 0)
//                    Gizmos.color = Color.red;
//                else
//                    Gizmos.color = Color.red * 0.5F;
//                Gizmos.DrawSphere (position, light.range);
//            }
//	}
	
	
	//@DrawGizmo (GizmoType.NotSelected | GizmoType.Pickable)
//	static void RenderLightGizmo (light : Light, gizmoType : GizmoType) {
//	    var position = light.transform.position;
//	    // Draw the light icon
//	    // (A bit above the one drawn by the builtin light gizmo renderer)
//	    Gizmos.DrawIcon (position + Vector3.up, "Light Gizmo.tiff");
//		
	//}
	
//	public void OnDrawGizmos()
//	{
//		Gizmos.DrawCube(this.transform.position, Vector3.up);
//		if (Camera.current != null)
//			this.transform.position = mySceneCamera.transform.position;
//		
//		Debug.Log("Camera name is : " + Camera.current.name + " pos: " + Camera.current.transform.position.ToString());
//		//Gizmos.DrawCube = Color.white;
//		//Gizmos.DrawCube(Camera.current.transform.position, new Vector3(2,2,2));
//	}
//}



//    private Camera _sceneCamera = null;
//	
//	void awake()
//	{
//		SetMainToScene();
//	}
// 
//	public void SetMainToScene()
//    {
//        if (Camera.main == null)
//        {
//            Debug.Log("Main camera not found.");
//
//            return;
//        }
//
//        if (_sceneCamera == null)
//        {
//            if (Camera.current == null || Camera.current.name != "SceneCamera")
//            {
//                Debug.Log("Scene camera not selected. First click on Scene tab before calling.");
//
//                return;
//            }
//            _sceneCamera = Camera.current;
//        }
// 
//        Camera.main.transform.position = _sceneCamera.transform.position;
//
//        Camera.main.transform.rotation = _sceneCamera.transform.rotation;
//
//    }
