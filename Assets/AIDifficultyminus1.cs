using UnityEngine;
using System.Collections;

public class AIDifficultyminus1 : MonoBehaviour {

    private float delay = 0.25f;
    private GameObject xhand;
    private float chance;
    // Use this for initialization

    void Start()
    {
        xhand = GameObject.FindGameObjectWithTag("xhand");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Addchance()
    {
        chance = xhand.GetComponent<XHands>().chance;

        if (chance > 0.051)
        {

            chance = chance - 0.05f;
            xhand.GetComponent<XHands>().chance = chance;
        }
    }

    IEnumerator GazeDelay()
    {
        yield return new WaitForSeconds(delay);
        Addchance();
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
