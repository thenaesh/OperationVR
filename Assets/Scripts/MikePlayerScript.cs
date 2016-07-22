using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class MikePlayerScript: MonoBehaviour {

    private Transform leapTransform;
    private GameObject EyeGazeHandParent;
    private GameObject EyeGazeHand;
    private GameObject ShieldHand;
    private Vector3 LookPoint;
    
	void Start () {
        //Find EyeGazeHandParent
        EyeGazeHandParent = GameObject.FindGameObjectWithTag("EyeGazeParent");
        //Find EyeGazeHand 
        EyeGazeHand = EyeGazeHandParent.transform.GetChild(0).gameObject;
        //Find ShieldHand
        ShieldHand = EyeGazeHandParent.transform.GetChild(1).gameObject;



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

               
                //Set active EyeGazeHAnd plane and ShieldHand after ball starts
                EyeGazeHand.SetActive(true);
                ShieldHand.SetActive(true);
            }

               else if (hit.transform.tag == "EyeGazeHand")
                {
                //Adjust Handshield to be slightly in front of EyeGazeHand Plane
                ShieldHand.transform.position = hit.point - new Vector3(0.4f,0,0);   



                }
            }
	}
		
}
