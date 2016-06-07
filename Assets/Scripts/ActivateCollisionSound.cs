using UnityEngine;
using System.Collections;

public class ActivateCollisionSound : MonoBehaviour
{
    AudioSource audio;
    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        audio.Play();
    }
}