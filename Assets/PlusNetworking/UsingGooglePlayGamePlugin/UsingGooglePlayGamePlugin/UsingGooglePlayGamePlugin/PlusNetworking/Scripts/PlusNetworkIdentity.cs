using UnityEngine;
using System.Collections;

public class PlusNetworkIdentity : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	public void ElementInUse()
	{
		for(int n = 0 ; n<= PlayerPrefs.GetInt("prefabLen") ; n++ )
		{
			if (gameObject.name == PlayerPrefs.GetString("prefabInGame"+n))
			{
				PlayerPrefs.SetInt("ElementInUse",n);

			}
		}
		//Debug.Log(PlayerPrefs.GetInt("ElementInUse"));
	}
}
