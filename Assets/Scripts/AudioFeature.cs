using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeature : MonoBehaviour
{
    public AudioSource FiveStepStart;
    public AudioSource SevenStepStart;
    public AudioSource NineStepStart;
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

    int num;


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

        // Case 1, 3, 5
        // Original 5, 7, 9 Step Stairs 

        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.blue);

            if (num % 2 != 0)
            {
                if (hitInfo.collider.gameObject.tag == "FiveStepTrigger")
                {
                    FiveStepStart.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "SevenStepTrigger")
                {
                    SevenStepStart.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "NineStepTrigger")
                {
                    NineStepStart.Play(0);
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

            // Case 2 (Flipped 5 Step Stairs) 
            if (num == 2)
            {
                if (hitInfo.collider.gameObject.tag == "EndStairs")
                {
                    FiveStepStart.Play(0);
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

                if (hitInfo.collider.gameObject.tag == "FiveStepTrigger")
                {
                    EndStairs.Play(0);
                }
            }

            // Case 4 (Flipped 7 Step Stairs) 
            if (num == 4)
            {
                if (hitInfo.collider.gameObject.tag == "EndStairs")
                {
                    SevenStepStart.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "One")
                {
                    Seven.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Two")
                {
                    Six.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Three")
                {
                    Five.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Four")
                {
                    Four.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Five")
                {
                    Three.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Six")
                {
                    Two.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "Seven")
                {
                    One.Play(0);
                }

                if (hitInfo.collider.gameObject.tag == "SevenStepTrigger")
                {
                    EndStairs.Play(0);
                }
            }

            // Case 6 (Flipped 9 Step Stairs) 
            if (num == 6)
            {
                if (hitInfo.collider.gameObject.tag == "EndStairs")
                {
                    NineStepStart.Play(0);
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

                if (hitInfo.collider.gameObject.tag == "NineStepTrigger")
                {
                    EndStairs.Play(0);
                }
            }
        }
    }
}
