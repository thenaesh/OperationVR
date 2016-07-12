using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MikePlayerScript: NetworkBehaviour {
    private bool isHost = false;

    [SyncVar]
	public int score = 0;

	private Text myScore;
	private Text otherScore;

    private Transform leapTransform;
    


	void Start () {
		if (isLocalPlayer) {
			GetComponent<RigidbodyFirstPersonController> ().enabled = true;
			GetComponentInChildren<HeadBob> ().enabled = true;
			GetComponentInChildren<Camera> ().enabled = true;
			GetComponentInChildren<AudioListener> ().enabled = true;
			myScore = GameObject.FindGameObjectWithTag("myScore").GetComponent<Text>();
			otherScore = GameObject.FindGameObjectWithTag("otherScore").GetComponent<Text>();
			myScore.text = "Me - X";
			otherScore.text = "Other - X";

			// Point the object at thw world origin
			transform.LookAt(Vector3.zero);

            //Transform local scene object:leap space as a local player child 
            leapTransform = GameObject.FindGameObjectWithTag("leap").GetComponent<Transform>();
            leapTransform.parent = transform;
            leapTransform.position = transform.position + transform.forward;
            leapTransform.rotation = transform.rotation;
        }
	}

	void FixedUpdate() {

        // FINDS THE HOST
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            isHost = true;
        }

        if (isHost)
        {
            // PERFORM RAYCAST
            RaycastHit hit;
            Transform cameraTransform = Camera.main.transform;

            //if(Physics.Raycast(transform.position, transform.forward, out hit, 50f))
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 50f))
            {
                if (hit.transform.tag == "Ball")
                {

                    Transform ballTransform = hit.collider.gameObject.transform;
                    //				hit.rigidbody.AddExplosionForce (3f, transform.position, 10f);
                    hit.rigidbody.AddForce(-transform.position);

                    GameObject.FindGameObjectWithTag("reticle").GetComponent<Image>().color = Color.red;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("reticle").GetComponent<Image>().color = Color.white;
                }
            }

        }

		// UPDATE SCOREBOARD
		try { // BAND-AID. might loop through some other stuff, idk what. 
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
				if (go.GetComponent<MikePlayerScript>().isLocalPlayer) {
					myScore.text = "Me - " + score;
				} else {
					otherScore.text = "Other - " + go.GetComponent<MikePlayerScript>().score;
				}
			}
		} catch {
			// do nothing
		}
	}

	public void IncreaseScore(int amount) 
	{
		score += amount;
	}

}
