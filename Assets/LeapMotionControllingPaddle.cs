using Leap;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

using UnityEngine;

//using Vuforia;

public class LeapMotionControllingPaddle : NetworkBehaviour
{
    private LeapServiceProvider leapProvider;

    private float vectorx;
    private float vectory;
    private float vectorz;

    private bool Local;


    // Use this for initialization
    private void Start()
    {
        Local = transform.parent.gameObject.GetComponent<MikePlayerScript>().isLocalPlayer;
        leapProvider = FindObjectOfType<LeapServiceProvider>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Local)
        {
            Frame currentFrame = leapProvider.CurrentFrame;
            try
            {
                foreach (Hand hand in currentFrame.Hands)
                {
                    if (hand.IsLeft)
                    {
                        //Individual Allocation of vector components works
                        vectorx = hand.PalmPosition.x;
                        vectory = hand.PalmPosition.y;
                        vectorz = hand.PalmPosition.z;
                        transform.position = new Vector3(vectorx, vectory, vectorz);
                        // transform.position = hand.PalmPosition;
                        //  Debug.Log("Left hand is @ " + hand.PalmPosition);
                    }
                    else
                    {
                        vectorx = hand.PalmPosition.x;
                        vectory = hand.PalmPosition.y;
                        vectorz = hand.PalmPosition.z;
                        transform.position = new Vector3(vectorx, vectory, vectorz);
                        //  transform.position = hand.PalmPosition;
                        //  Debug.Log("Right hand is @ " + hand.PalmPosition);
                    }
                }
            }
            catch { }
            // Debug.Log("---Next frame---");
        }
    }
}