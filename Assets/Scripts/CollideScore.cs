using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollideScore : MonoBehaviour {
	Vector3 ballSpawnPos;

	void Start()
	{
		ballSpawnPos = GameObject.FindGameObjectWithTag("ballSpawn").transform.position;
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
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag("Player");
			int furthest = 0;
			float furthestDistance = 0f;
			for(int i=0; i<gos.Length; i++) {
				float distance = (transform.position - gos[i].transform.position).magnitude;
				if (distance > furthestDistance) {
					furthest = i;
					furthestDistance = distance;
				}
			}
			gos[furthest].GetComponent<MikePlayerScript>().IncreaseScore(1);

		}
	}
}
