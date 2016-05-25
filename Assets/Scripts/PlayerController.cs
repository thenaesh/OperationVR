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
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

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
		//cameraTransform.localPosition = transform.position + new Vector3(0f, 1f, 0f);
		//cameraTransform.localRotation= transform.rotation;
		GameObject.FindGameObjectWithTag("Head").GetComponent<CardboardHead>().setTarget (transform);
	}

}