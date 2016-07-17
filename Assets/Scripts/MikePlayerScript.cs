using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class MikePlayerScript: MonoBehaviour {

    private Transform leapTransform;
    
	void Start () {
        //Transform local scene object:leap space as a local player child 
        leapTransform = GameObject.FindGameObjectWithTag("leap").GetComponent<Transform>();
        leapTransform.parent = transform;
        leapTransform.position = transform.position + transform.forward;
        leapTransform.rotation = transform.rotation;
	}

	void FixedUpdate() {
		
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
		
}
