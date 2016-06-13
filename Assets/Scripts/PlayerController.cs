using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float dampTime = 0.3f;
	public float distance = 10f;

	private Transform cameraTransform;
	private NetworkStartPosition[] spawnPoints;

	public GameObject ballPrefab;
	public Vector3 ballSpawnPosition = new Vector3(0,2,0);
	public float speed = 5.0f;


	private GameObject ball;
	public float delay = 1;

	IEnumerator GazeDelay() {
		yield return new WaitForSeconds (delay);
		CmdSpawnBall();
	}

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("PlayerHorizontal") * Time.deltaTime * 3.0f;
		var z = Input.GetAxis("PlayerVertical") * Time.deltaTime * 3.0f;
		var h = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		var v = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Translate(x, 0, z);

		transform.FindChild ("Hand").Translate (h, v, 0);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
	}


	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	public void CmdSpawnBall()
	{
		// Create the ball from the ballPrefab
		ball = (GameObject)Instantiate(
			ballPrefab,
			ballSpawnPosition,
			Quaternion.identity);

		// Add velocity to the ball
		ball.GetComponent<Renderer> ().material.color = Color.yellow;
		//SB: MAKE THIS BETTER for 2nd or 3rd sharing
		ball.GetComponent<Rigidbody>().velocity = new Vector3 ( Mathf.Sign((float)Random.Range(-1,1)) * speed, 0.0f, 0.0f);

		// Spawn the ball on the Clients
		NetworkServer.Spawn(ball);
	}

	[Command]
	public void CmdDestroyBall()
	{
		// Destroy the ball on the Clients
		NetworkServer.Destroy(ball);
	}


	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 0.1f;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	public override void OnStartLocalPlayer ()
	{
		cameraTransform = GameObject.FindGameObjectWithTag("GvrMain").GetComponent<Transform>();
		GetComponent<MeshRenderer>().material.color = Color.blue;
		GameObject.FindGameObjectWithTag ("GvrMain").transform.parent = transform;
		cameraTransform.position = transform.position + new Vector3(0f, 0.6f, 0.3f);
		cameraTransform.rotation= transform.rotation;

		//makes player a child of his spawnpoint so CardboardHead localOrientation will work and player starts facing origin
		spawnPoints = FindObjectsOfType<NetworkStartPosition>();
		for (short i = 0; i < spawnPoints.Length; i++)
		{
			if (Vector3.Distance(transform.position, spawnPoints [i].transform.position)<0.5) {
				transform.parent = spawnPoints[i].transform;
				break;
			}
		}

		GameObject.FindGameObjectWithTag("Head").GetComponent<CardboardHead>().SetTarget (transform);
	}

	public void StartSpawnCountdown() {
		StartCoroutine ("GazeDelay");
	}

	public void StopSpawnCountdown() {
		StopCoroutine ("GazeDelay");
	}

	public void KillBall() {
		CmdDestroyBall ();
	}
}