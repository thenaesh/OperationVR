using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameController : MonoBehaviour, ICardboardGazeResponder
{
	public PlayerController player;

	#region ICardboardGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		Debug.Log ("Origin Gaze Detected");
		player.StartSpawnCountdown ();
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		player.StopSpawnCountdown ();
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {

	}

	#endregion
}