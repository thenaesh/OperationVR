using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
	[SerializeField] Text connectionText;
	[SerializeField] Transform[] spawnPoints;
	[SerializeField] Camera sceneCamera;

	GameObject player;

	// Use this for initialization
	void Start () {
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
		PhotonNetwork.ConnectUsingSettings ("0.2");
	}
	
	// Update is called once per frame
	void Update () {
		connectionText.text = PhotonNetwork.connectionStateDetailed.ToString ();
	}

	void OnJoinedLobby()
	{
		RoomOptions ro = new RoomOptions () { isVisible = true, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom ("Alpha", ro, TypedLobby.Default);
	}

	void OnJoinedRoom()
	{
		StartSpawnProcess (0f);
	}

	void StartSpawnProcess (float respawnTime)
	{
		sceneCamera.enabled = true;
		StartCoroutine ("SpawnPlayer", respawnTime);

	}

	IEnumerator SpawnPlayer(float respawnTime)
	{
		yield return new WaitForSeconds (respawnTime);

		//int index = Random.Range (0, spawnPoints.Length);
		int index;
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Player");
		if (gos == null) {
			index = 0;
		} else {
			index = gos.Length%2;
		}
		player = PhotonNetwork.Instantiate ("FPSPlayer", spawnPoints [index].position, spawnPoints [index].rotation, 0); 
		sceneCamera.enabled = false;
	}
}
