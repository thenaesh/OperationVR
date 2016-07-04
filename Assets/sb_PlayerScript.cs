using UnityEngine;
using System.Collections;

public class sb_PlayerScript : MonoBehaviour {
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	[SerializeField] float liveTime;
	[SerializeField] float bulletDelay;


	// Use this for initialization
	void Start () {
		/*
		//INVIS BULLET, dosent work as well as RaycastHit
		liveTime = 2f;
		bulletDelay = 0.5f;
		StartCoroutine ("SpawnBullets");
		*/
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		RaycastHit hit;
		Transform cameraTransform = Camera.main.transform;

		//if(Physics.Raycast(transform.position, transform.forward, out hit, 50f))
		if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 50f))
		{
			if (hit.transform.tag == "Ball") {

				Transform ballTransform = hit.collider.gameObject.transform;
				hit.rigidbody.AddExplosionForce (3f, transform.position, 10f);
			}
		}

	}

	IEnumerator SpawnBullets() {
		while (true) {
			Fire ();
			yield return new WaitForSeconds (bulletDelay);
		}
	}

	void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Destroy the bullet after x seconds
		Destroy(bullet, liveTime);
	}
}
