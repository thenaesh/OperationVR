using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;﻿
using UnityEngine.UI;

public class PlayerNetworkMover : Photon.MonoBehaviour {

	Text myScore;
	Text otherScore;

	Vector3 position;
	Quaternion rotation;
	float smoothing = 10f;
	int score = 0;

	public void IncreaseScore(int amount)
	{
		score += amount;
	}

	void Start () {
		if (photonView.isMine) {
			GetComponent<Rigidbody> ().useGravity = true;
			GetComponent<RigidbodyFirstPersonController> ().enabled = true;
			GetComponent<sb_PlayerScript> ().enabled = true;
			GetComponentInChildren<HeadBob> ().enabled = true;
			GetComponentInChildren<Camera> ().enabled = true;
			GetComponentInChildren<AudioListener> ().enabled = true;
			myScore = GameObject.FindGameObjectWithTag("myScore").GetComponent<Text>();
			otherScore = GameObject.FindGameObjectWithTag("otherScore").GetComponent<Text>();
			myScore.text = "Mine - X";
			otherScore.text = "Other - X";
		} else {
			StartCoroutine ("UpdateData");
		}
	}

	void FixedUpdate() {
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
			if (go.GetComponent<PlayerNetworkMover>().photonView.isMine) {
				myScore.text = "Mine - " + score;
			} else {
				otherScore.text = "Other - " + go.GetComponent<PlayerNetworkMover>().score;
			}
		}
	}

	IEnumerator UpdateData()
	{
		while(true)
		{
			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);
			yield return null;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(score);
		}
		else
		{
			position = (Vector3)stream.ReceiveNext();
			rotation = (Quaternion)stream.ReceiveNext();
			score = (int)stream.ReceiveNext();
		}
	}
}
