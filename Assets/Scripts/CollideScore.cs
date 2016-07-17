﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollideScore : MonoBehaviour {
	private Vector3 ballSpawnPos;
	public int score = 0;
	public Text scoreText;

	void Start()
	{
		ballSpawnPos = GameObject.FindGameObjectWithTag("ballSpawn").transform.position;
		setScore();
	}

	public void setScore()
	{
		scoreText.text = scoreText.text.Substring(0,5) + score;
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag("Ball"))
		{
			//RESET BALL POSITION AND STOP MOVEMENT
			//other.transform.position = new Vector3 (0, 0.5f, 0);
			other.transform.position = ballSpawnPos;
			other.GetComponent<Rigidbody> ().velocity = Vector3.zero;

			//UPDATE SCORE ON CORRECT PLAYER GAMEOBJECT
			// don't forget activate is a Trigger
			GetComponent<AudioSource>().Play();
			score += 1;
			setScore();

		}
	}


}
