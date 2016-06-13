using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sb_displayUserInput : MonoBehaviour {

	public InputField mainInputField;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Refresh() {
		GetComponent<Text> ().text = mainInputField.text;
	}
}
