using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {
	public Camera mainCamera;

	void Update () {
		try
		{
			//transform.LookAt(Camera.main.transform); //text is inverted
			transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
		} catch {
			Debug.Log ("Changing camera: " + Time.time);
		}
	}
}