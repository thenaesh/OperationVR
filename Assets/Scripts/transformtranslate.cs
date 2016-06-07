using UnityEngine;
using System.Collections;

public class transformtranslate : MonoBehaviour {

    public float speed=100;

    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f );

        rb.AddForce(movement * speed);
    }
}
