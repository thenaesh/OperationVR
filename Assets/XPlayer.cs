using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class XPlayer : MonoBehaviour {


	private GameObject ball;


	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag ("Ball");
	}

	void Update() {
		if (Input.GetKeyDown ("space")) { // RESET SCORE AND SWAP PLAYER
			GameObject goal1 = GameObject.FindGameObjectWithTag ("goal1");
			GameObject goal2 = GameObject.FindGameObjectWithTag ("goal2");
			Vector3 t = goal1.transform.position;
			goal1.transform.position = goal2.transform.position;
			goal2.transform.position = t;
			goal1.GetComponent<CollideScore> ().score = 0;
			goal1.GetComponent<CollideScore> ().setScore();
			goal2.GetComponent<CollideScore> ().score = 0;
			goal2.GetComponent<CollideScore> ().setScore();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.LookAt (ball.transform);
		transform.eulerAngles  = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
	}
}
