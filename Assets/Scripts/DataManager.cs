using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    // References to other Scripts. Keep DataManaer & TrialManager on the same GO. 
    private StairsTrialManager MyTrialManager;
    //public MeasurePerformance MyPerformanceMeasures;

    // UI / Filewriter vars
    public string filename;
    public GameObject StartUI;
    public GameObject ScotomaParent; 
    public List<GameObject> inputFields;

    // Participant info to write
    string subjectId;
    string height;
    string age;
    string gender;
    // Experiment info to write
    string scotomaCondition;
    public int assistiveCondition;

    // HALEY - CHANGE THIS
    // References to left and right eye cameras. Used to set low vision simulation shaders.  
    // public Transform lefteye;
    // public Transform righteye;



    void Start()
    {
        // Get trial and task performance script references 
        MyTrialManager = GetComponent<StairsTrialManager>();
        //MyPerformanceMeasures = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MeasurePerformance>();

        // Get UI gameojbect reference
        StartUI = GameObject.FindGameObjectWithTag("UI");

        // Set empty participant info.  Set with UI later. 
        subjectId = "";
        age = "0";
        height = "";
        gender = "";
        scotomaCondition = "0";
        assistiveCondition = 0;
    }


    // Set filename and path.
    // Create file if it doesn't exist. Toss error if file does exist. Don't let the user accidently overwrite existing files ;)
    public void CreateFile()
    {
        GetParticipantInfoFromUI(); 
        filename = Application.dataPath + "/Data/" + "STAIRSEXP_ID" + subjectId + "_VFL" + scotomaCondition + "_ASSIST" + assistiveCondition + ".txt"; // use subj & exp ids
        Debug.Log(filename);

        if (!File.Exists(filename))
        {
            // Create a file to write to 
            using (StreamWriter sw = File.CreateText(filename))
            {
                // Write header for Experiment Information 
                sw.WriteLine("SubjectID, ScotomaCondition, AssistiveCondition, Height, Gender, Age");
                sw.WriteLine(subjectId + ", " + scotomaCondition + ", " + assistiveCondition + ", " + height + ", " + gender + ", " + age);

                // Write second header for Data Labels 
                sw.WriteLine("Trial, [ConditionArray], StairCondition, AssistCondition , TravelTime"); // +
                    //"StepErrorLeft, StepErrorRight, " +     // step  measures        
                   // "TravelTime, ResponseTime");	        // time measures 
                sw.Close();
            }
        }
        else
        {
            Debug.LogError("ERR: FILE EXISTS");
            Application.Quit();
        }
        StartUI.SetActive(false);        // Hide UI (during the experiment)
    }




    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Get the text inputs from the UI input fields 
    //////////////////////////////////////////////////////////////////////////////////////////////////////////


    // Manually assign the correct GameObjects via the Inspector 
    public void GetParticipantInfoFromUI()
    {
        subjectId = inputFields[0].GetComponent<InputField>().text;
        gender = inputFields[1].GetComponent<InputField>().text; 
        height = inputFields[2].GetComponent<InputField>().text;
        age = inputFields[3].GetComponent<InputField>().text;
    }

    // Select Scotoma Condition based on dropdown option index
    // option.value = an integer
    public void SetScotomaCondition(Dropdown option)
    {
        scotomaCondition = "" + option.value; // see what this outputs? says integer.. 
        // 0,1,2
        Debug.Log("Scotoma: " + scotomaCondition);

        ////////////////////////////////////////////////////////////////////////////////
        // HALEY - UPDATE THIS 
        ////////////////////////////////////////////////////////////////////////////////        
        ScotomaParent.transform.GetChild(option.value).GetComponent<MeshRenderer>().enabled=true; 

    }

    // Select Assisitive Condition based on dropdown option index
    // option.value = an integer
    public void SetAssistiveCondition(Dropdown option)
    {
        assistiveCondition = option.value; 
        Debug.Log("Assisitve: " + assistiveCondition);

        // USE THIS INFO IN STAIRSTRIALMANAGER.CS 
    }




    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Get and write data when each trial is completed 
    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // WriteAngleData() is called at the end of the 1st response phase in TrialManager.cs
    // WriteDistanceTimeData() is called at the end of the 2nd response phase in TrialManager.cs


    // Write variables for trial (trial number, path array, path distance array)
    public void WriteTrialVariables()
    {
        // et current trial number and path information 
        int[] myConditionArray = MyTrialManager.GetConditionArray();
        int cur_trial = MyTrialManager.currentTrial;
        int currentStairCondition = MyTrialManager.GetCurrentStairCondition();

        using (StreamWriter sw = File.AppendText(filename))
        {
            // Write current trial number
            sw.Write(cur_trial + ", [");

            // Write Condition Variables
            sw.Write(myConditionArray[0]);
            for (int i = 1; i < myConditionArray.Length; i++)
                sw.Write("," + myConditionArray[i]);
            sw.Write("], " + currentStairCondition + ", " + assistiveCondition + ", ");


            sw.Close();
        }      
    }
    

    // For each trial, write  time measures. Called by StairTrialManager.cs
    public void WriteTimeData(float traversal_time)
    {
        //int target_id = MyTrialManager.GetTargetIndex();
        //string distance_measures = MyPerformanceMeasures.GetDistanceMeasures(pathOrigin, pathEnd, previousPlayerPosition);
        //string time_measures = MyTrialManager.GetTimeMeasures();
        string time_measure = "" + traversal_time; 

        using (StreamWriter sw = File.AppendText(filename))
        {
            sw.Write("" + time_measure);
            sw.Write("\n");
        }

    }


}


