using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollideScore : MonoBehaviour {

    private int score = 0;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        setScore();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
               
    }

    void setScore()
    {
		scoreText.text = scoreText.text.Substring(0,5) + score;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainBall"))
        {
            // don't forget activate is a Trigger
			GetComponent<AudioSource>().Play();
            score += 1;
            setScore();
			other.GetComponent<BallMovement>().Reset();
        }
    }
}
