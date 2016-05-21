using UnityEngine;
using System.Collections;

public class kinematicmovement : MonoBehaviour {

    public Vector3 teleportPoint;
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //test
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0.5f*moveHorizontal, 0.0f, 0.5f*moveVertical);

        rb.MovePosition(transform.position + movement);
    }
}
