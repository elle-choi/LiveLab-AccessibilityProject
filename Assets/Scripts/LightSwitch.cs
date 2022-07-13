using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public int tapTimes;
    public float resetTimer;
    public Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>(); 
    }

    IEnumerator ResetTapTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        tapTimes = 0; 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            StartCoroutine("ResetTapTimes");
            tapTimes++; 
        }

        if(tapTimes == 1)
        {
            BrightMode();
        }else if(tapTimes == 2)
        {
            DimMode();
        }else if (tapTimes == 3)
        {
            DarkMode(); 
        }else{
            ResetTapTimes();
        }
    }

    private void BrightMode()
    {
        myLight.intensity = Mathf.PingPong(Time.time, 8);
    }

    private void DimMode()
    {
        myLight.intensity = Mathf.PingPong(Time.time, 4);
    }

    private void DarkMode()
    {
        myLight.intensity = Mathf.PingPong(Time.time, 1);
    }

 }
