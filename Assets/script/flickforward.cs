using UnityEngine;
using System.Collections;

public class flickforward : MonoBehaviour {
    private Rigidbody rb;
    public float flickdistance=1000.0f;
    public float power=50.0f;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 movement = new Vector3( 0.0f, 0.0f, flickdistance);
        if (Input.GetButtonDown("Fire1"))
        {
            rb.AddForce(power*movement);
            
        }
        if (Input.GetButtonUp("Fire1")) { rb.AddForce(-power * movement); }
    }
}
