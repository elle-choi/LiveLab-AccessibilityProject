using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeature : MonoBehaviour
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


    private void Start()
    {
     
    }
  

    private void FixedUpdate()
    {
        RaycastOnStairs();
    }

    void RaycastOnStairs()
    {
       

        RaycastHit hitInfo;

        // FIXME: first problem: the first step trigger message differs
        // possible solution: should we just make it constant for all 3 steps? to just StepTrigger?

        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.blue);

            if (num % 2 == 0)
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

                if (hitInfo.collider.gameObject.tag == "Six")
                {
                    Six.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Seven")
                {
                    Seven.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Eight")
                {
                    Eight.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Nine")
                {
                    Nine.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "EndStairs")
                {
                    EndStairs.Play(0);
                }
            }

            // FIX ME: FLIPPING LIST & using if statements both don't work because
            // it plays one when the tag is nine (would be the same with flipping the list)
            // possible solution have it play the same for 1, 3, 5 cases
            // and have separate lists or if staements for 2, 4, 6 cases (flipped) 
            if (num % 2 != 0)
            {
                if (hitInfo.collider.gameObject.tag == "EndStairs")
                {
                    FiveStepStart.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "One")
                {
                    Nine.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Two")
                {
                    Eight.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Three")
                {
                    Seven.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Four")
                {
                    Six.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Five")
                {
                    Five.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Six")
                {
                    Four.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Seven")
                {
                    Three.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Eight")
                {
                    Two.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Nine")
                {
                    One.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "FiveStepTrigger")
                {
                    EndStairs.Play(0);
                }
            }
            
        }
    }
}
