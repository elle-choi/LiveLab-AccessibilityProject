using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEndAudio : MonoBehaviour
{ 
    public AudioSource FiveStepStart;
    public AudioSource One;
    public AudioSource Two;
    public AudioSource Three;
    public AudioSource Four;
    public AudioSource Five;
    public AudioSource EndStairs;

    private void Start()
    {
        //startSound = GetComponent<AudioSource>();
        //endSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "FiveStepTrigger")
                {
                    FiveStepStart.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "One")
                {
                    One.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Two")
                {
                    Two.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Three")
                {
                    Three.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Four")
                {
                   Four.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Five")
                {
                    Five.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "EndStairs")
                {
                    EndStairs.Play(0);
                }
            }
        }
    }

    /*
    private void OnMouseEnter()
    {
        
    }
    */

    /*
    private void Update()
    {
        // FIXME: temporary code for now
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("alpha 1 pressed");
            startSound.Play(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            endSound.Play(0);
        }

        // FIXME: when bottom platform hit, start audio play; last step hit, end audio play 
    }
    */

}
