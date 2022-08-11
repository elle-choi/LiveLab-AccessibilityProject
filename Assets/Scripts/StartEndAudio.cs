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

    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastOnStairs();
        //}
    }

    private void FixedUpdate()
    {
        RaycastOnStairs();
    }

    void RaycastOnStairs()
    {
        // for the "final" version of your code,
        // you'll just modify this ray function 
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hitInfo;

        //if (Physics.Raycast(transform.position, transform.forward, 10))
        //   print("There is something in front of the object!");


        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.blue);

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

        // wasn't sure what the tag was
        // for 5-Step stairs but do I need this all for 7 and 9?
        /* 
        if (Collider.gameobject.tag == "Up")
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
        if (Collider.gameobject.tag == "Down")
        {
            if (hitInfo.collider.gameObject.tag == "FiveStepTrigger")
            {
                EndStairs.Play(0);
            }

            if (hitInfo.collider.gameObject.tag == "One")
            {
                Five.Play(0);
            }

            if (hitInfo.collider.gameObject.tag == "Two")
            {
                Four.Play(0);
            }

            if (hitInfo.collider.gameObject.tag == "Three")
            {
                Three.Play(0);
            }

            if (hitInfo.collider.gameObject.tag == "Four")
            {
                Two.Play(0);
            }

            if (hitInfo.collider.gameObject.tag == "Five")
            {
                One.Play(0);
            }

            if (hitInfo.collider.gameObject.tag == "EndStairs")
            {
                FiveStepStart.Play(0);
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
}