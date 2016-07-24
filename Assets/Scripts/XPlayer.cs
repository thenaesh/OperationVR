using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class XPlayer : MonoBehaviour, ICardboardGazeResponder {


	private GameObject ball;
    private float delay = 2;


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

   public void ResetScore()
    {
        GameObject goal1 = GameObject.FindGameObjectWithTag("goal1");
        GameObject goal2 = GameObject.FindGameObjectWithTag("goal2");
        goal1.GetComponent<CollideScore>().score = 0;
        goal1.GetComponent<CollideScore>().setScore();
        goal2.GetComponent<CollideScore>().score = 0;
        goal2.GetComponent<CollideScore>().setScore();
    }

    IEnumerator GazeDelay()
    {
        yield return new WaitForSeconds(delay);

        ResetScore();
    }

    #region ICardboardGazeResponder implementation

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter()
    {
        Debug.Log("Ball Gaze Detected");
        StartCoroutine("GazeDelay");
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit()
    {
        StopCoroutine("GazeDelay");
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
    public void OnGazeTrigger()
    {

    }

    #endregion

}
