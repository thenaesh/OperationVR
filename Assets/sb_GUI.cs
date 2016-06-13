using UnityEngine;
using System.Collections;

//[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class sb_GUI : MonoBehaviour {

	public string stringToEdit = "Hello World";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI() {
		stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20 ), stringToEdit, 15);
	}

	/*
	void OnGUI() {
		if (GUILayout.Button("Press Me"))
			Debug.Log("Hello!");
	}
	*/
}
