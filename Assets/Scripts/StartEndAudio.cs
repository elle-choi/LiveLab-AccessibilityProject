using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEndAudio : MonoBehaviour
{
   
    public AudioSource startSound;
    public AudioSource endSound;

    private void Start()
    {
        startSound = GetComponent<AudioSource>();
        endSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // FIXME: temporary code for now
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            startSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            endSound.Play();
        }


        // FIXME: when bottom platform hit, start audio play; last step hit, end audio play
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Select stage    
                if (hit.transform.name == "BottomPlatform")
                {
                    startSound.Play();
                }

                if (hit.transform.name == "lastStair")
                {
                    endSound.Play();
                }
            }
        }
        */

    }
}
