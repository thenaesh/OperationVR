using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallSpeedInfoText : MonoBehaviour {

    private int Ballspeed;
    private Text BallInfoSpeed;
    // Use this for initialization
    void Start () {

       // Ballspeed = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallConstantSpeed>().constantSpeed;
        BallInfoSpeed = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {

        Ballspeed = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallConstantSpeed>().constantSpeed;
        BallInfoSpeed.text = "BallSpeed: " + Ballspeed;
    }
}
