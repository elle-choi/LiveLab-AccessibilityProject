using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public void Update()
    {
        //Debug.Log("function loaded");
        /*
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            // bring next scene 
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // bring the scene before 
        }
        */


        /*
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            // load high visibility scene 
            SceneManager.LoadScene(1);
        }
        */


        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // load low visibility scene (curved carpet)
            Debug.Log("curved Carpet");
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("glass");
            // load low visibility scene (glass)  
            SceneManager.LoadScene(1);
        }

        /*
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            // load low visibility scene (slate)  
            SceneManager.LoadScene(4);
        }
        */

    }
}
