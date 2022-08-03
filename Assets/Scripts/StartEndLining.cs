using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndLining : MonoBehaviour
{
    public GameObject startIndication;
    public GameObject endIndication;

    void Update()
    {
        // FIXME change space and tab to whatever input we are going to get in the experiment

        if (Input.GetKeyDown(KeyCode.S))
        {
            startIndication.SetActive(true);
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            endIndication.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            startIndication.SetActive(false);
            endIndication.SetActive(false);
        }
    }
}
