using UnityEngine;
using System.Collections;

public class BallSpeedminus1 : MonoBehaviour, ICardboardGazeResponder
{

    private float delay = 0.5f;
    private GameObject Ball;
    private int ballspeed;
    // Use this for initialization

    void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void MinusBallSpeed()
    {

        ballspeed = Ball.GetComponent<BallConstantSpeed>().constantSpeed;

        //Prevent negative or zero speed
        if (ballspeed > 1)
        {
            ballspeed = ballspeed - 1;
            Ball.GetComponent<BallConstantSpeed>().constantSpeed = ballspeed;
        }
    }
    IEnumerator GazeDelay()
    {
        yield return new WaitForSeconds(delay);
        MinusBallSpeed();
        StartCoroutine("GazeDelay");

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
