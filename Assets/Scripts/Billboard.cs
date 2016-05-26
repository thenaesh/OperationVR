using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	void Update () {
		try
		{
			//transform.LookAt(Camera.main.transform); //text is inverted
			transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
		} catch {
			Debug.Log ("Changing camera: " + Time.time);
		}
	}
}