using UnityEngine;
using System.Collections;

public class ToggleEnvironment : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EnvironmentToggle()
    {
         GameObject EnvironmentParent = GameObject.Find("EnvironmentParent");
         GameObject Island = EnvironmentParent.transform.Find("IslandEnvironmentPrefab").gameObject;
        

        if (Island.activeSelf == true)
        {
            Island.SetActive(false);
        }

        else 
        {
            Island.SetActive(true);
        }
            }
}

