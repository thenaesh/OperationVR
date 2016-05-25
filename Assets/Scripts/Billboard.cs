using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	void Update () {
		try
		{
			transform.LookAt(Camera.main.transform);
		} catch {
			Debug.Log ("Changing camera: " + Time.time);
		}
	}
}