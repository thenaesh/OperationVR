using UnityEngine;
using System.Collections;

public class BallConstantSpeed : MonoBehaviour {

    public int constantSpeed = 4;



    private GameObject BallSlider;
    // Use this for initialization
    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = constantSpeed * (GetComponent<Rigidbody>().velocity.normalized);
    }

    
}
