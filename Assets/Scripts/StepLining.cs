using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepLining : MonoBehaviour
{
    // turn stair highlights son and off using Space and Tab

    public GameObject stairHighlight;

    void Update()
    {
        // FIXME change space and tab to whatever input we are going to get in the experiment
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stairHighlight.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            stairHighlight.SetActive(false);
        }
    }
}
