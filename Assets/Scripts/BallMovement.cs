using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour, ICardboardGazeResponder {

	public float speed = 5.0f;

	private float delay = 2;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}


	public void Reset() {
		transform.position = new Vector3 (0, 2, 0);
		rb.velocity = new Vector3 (0, 0, 0);
	}


	IEnumerator GazeDelay() {
		yield return new WaitForSeconds (delay);
		GetComponent<Renderer> ().material.color = Color.yellow;
		//SB: MAKE THIS BETTER for 2nd or 3rd sharing
		rb.velocity = new Vector3 ( Mathf.Sign((float)Random.Range(-1,1)) * speed, 0.0f, 0.0f);
	}

	#region ICardboardGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		Debug.Log ("Ball Gaze Detected");
		StartCoroutine("GazeDelay");
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		StopCoroutine ("GazeDelay");
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {

	}

	#endregion
}
