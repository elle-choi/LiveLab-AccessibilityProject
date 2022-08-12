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
    public AudioSource Six;
    public AudioSource Seven;
    public AudioSource Eight;
    public AudioSource Nine;
    public AudioSource EndStairs;

    List<string> evenAudio = new List<string>();        
    List<string> oddAudio = new List<string>();
   
    /*
    public string[] evenAudioFeatures = new string[] { "FiveStepTrigger", "One", "Two", "Three",
                                                    "Four", "Five", "Six", "Seven", "Eight",
                                                    "Nine", "EndStairs"};

    public string[] oddAudioFeatures = new string[11];
    */
    

    private void Start()
    {

    }

    private void Update()
    {
        /* used when mouse is clicked to create a raycastxwww
        if (Input.GetMouseButtonDown(0))
        {
            RaycastOnStairs();
        }
        */
    }

    private void FlipList()
    {
        for (int i = 0; i < evenAudioFeatures.Length; i++)
        {
            evenAudio.Add(evenAudioFeatures[i]); 
        }
    }


    private void FixedUpdate()
    {
        RaycastOnStairs();
    }

    void RaycastOnStairs()
    {
        // For Mouse Position: 
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        // FIXME: first problem: the first step trigger message differs
        // possible solution: should we just make it constant for all 3 steps? to just StepTrigger?

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

            if(hitInfo.collider.gameObject.tag == "Six")
            {
                Six.Play(0);
            }

            if(hitInfo.collider.gameObject.tag == "Seven")
            {
                Seven.Play(0);
            }

            if(hitInfo.collider.gameObject.tag == "Eight")
            {
                Eight.Play(0);
            }

            if(hitInfo.collider.gameObject.tag == "Nine")
            {
                Nine.Play(0);
            }

            if (hitInfo.collider.gameObject.tag == "EndStairs")
            {
                EndStairs.Play(0);
            }
        }

    }
}