using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitch : MonoBehaviour
{
    public GameObject startendHighlights;
    public GameObject stairHighlights;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            // FIXME How do I know which inputs to assign this to?
            // I can't drag & drop because this changes depending on 5, 7, 9
            startendHighlights.SetActive(true);
            stairHighlights.SetActive(true);
        }

        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            // FIXME: what should I turn on & off?
            // the current script just creating a raycast in each FixedUpdate()
        }
        */

        /* // FIXME : this might work better for UI? 
        if (Input.GetButtonDown("Audio"))
        {

        }
        */
    }
}
