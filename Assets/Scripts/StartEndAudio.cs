using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndAudio : MonoBehaviour
{
    private AudioSource x; 

    private void Start()
    {
        startSound = GetComponent<AudioSource>();
        endSound = GetComponent<AudioSource>();
    }

    private void startEndAudio()
    {
        // the start step should have a tag set to "Start"
        if (other.CompareTag("Start"))
        {
            startSound.Play();
        }

        if (other.CompareTag("End"))
        {
            endSound.Play();
        }
    }
}
