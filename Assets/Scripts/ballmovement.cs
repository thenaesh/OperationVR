using UnityEngine;
using System.Collections;

public class ballmovement : MonoBehaviour {
    private Rigidbody rb;
    public float speed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        Vector3 movement = new Vector3(0.0f, 0.0f, speed);
        rb.AddForce(movement * speed);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
