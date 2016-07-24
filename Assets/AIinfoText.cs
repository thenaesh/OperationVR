using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIinfoText : MonoBehaviour {

    private float chance;
    private Text AIInfoText;
    // Use this for initialization
    void Start()
    {

        
        AIInfoText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        chance = GameObject.FindGameObjectWithTag("xhand").GetComponent<XHands>().chance;
        AIInfoText.text = "AISkill: " + chance;
    }
}

