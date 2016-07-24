using UnityEngine;
using System.Collections;

public class XHands : MonoBehaviour {

	public float timeDelta;
	public float chance = 1f;
	private GameObject ball;
	private bool returnBall = false;
	private bool inBlock = false;
	private float t;
	private Rigidbody rb;
	private float speed;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag ("Ball");
		rb = ball.GetComponent<Rigidbody> ();
		t = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		speed = rb.velocity.magnitude;
		timeDelta = 0.5f / speed;
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("Ball") && !returnBall) {
			if ((Time.time - t)>timeDelta) {
				if (Random.value < chance ) {
					rb.velocity = new Vector3 (-rb.velocity.x, Random.Range(-speed,speed), Random.Range(-speed,speed));
					returnBall = true;
				}
				//reset timer
				t = Time.time;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Ball")) {
			returnBall = false;
		}
	}
}
