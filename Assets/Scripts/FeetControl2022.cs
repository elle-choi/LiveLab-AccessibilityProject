/* FeetConrol.cs uses two Vive tracking pucks to track feet position
 * and translate the player vertically in a virtual environment based on 
 * the surface beneath them. This script allows players to climb and 
 * descend walkable surfaces in VR.
 * 
 * This script is written by Haley Adams (2019)
 * However, it is based off of a script originally created by
 * Noorin Asjad, Haley Adams, and Richard Paris
 *
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetControl2022 : MonoBehaviour
{
    // Set in inspector 
    public GameObject L_Foot_Parent, R_Foot_Parent;
    public Transform L_Tracked_Object, R_Tracked_Object;

    // admit de feet  
    public Transform L_Foot_Model, R_Foot_Model;

    // raycast vars
    Vector3 bufferHeight;           // bufferHeight: to translate start of raycast of foot upwards for accuracy 

    Vector3 L_Position_Displacement, R_Position_Displacement;
    Vector3 L_Rotation_Displacement, R_Rotation_Displacement;

    //private float trackerDisplacementY; // offset of tracker position from ground in RW (used shoe model y val as ref )
    private float noFallHeight;     // height: within which translation will occur
    private float epsilon;          // epsilon: to compensate for tracking error
    private float speed;            // speed: to smooth lerp of player translation up and down
    private float frozenFloorHeight;

    public bool isHeightTranslationFrozen;

    // Assign 
    void Start()
    {
        // SteamVR plugin turns off vive trackers on start if no vive controller are also on (how vexing)
        // fix this by turning the go's back on at start
        L_Foot_Parent.SetActive(true);
        R_Foot_Parent.SetActive(true);

        /* Tracked Sibling */
        L_Tracked_Object = L_Foot_Parent.transform.GetChild(0);
        R_Tracked_Object = R_Foot_Parent.transform.GetChild(0);

        L_Foot_Model = L_Foot_Parent.transform.GetChild(1);
        R_Foot_Model = R_Foot_Parent.transform.GetChild(1);

        SetTrackerDisplacement();
        //trackerDisplacementY = 0.055f;


        // Set constraints for vertical translation
        //isHeightTranslationFrozen = false;
        frozenFloorHeight = 0f;
        bufferHeight = new Vector3(0, 1, 0);
        noFallHeight = 0.5f;
        epsilon = 0.01f;
        speed = 0.05f;
    }



    //MAKE SURE THE CONTROLLERS HAVE IGNORE RAYCAST LAYER SELECTED
    void FixedUpdate()
    {

        // Bit shift the index of layer (9) "Ground" to get a bit mask
        // Allows cast rays only agains colliders in layer 9
        int layerMask = 1 << 9;

        //Detect ground beneth left foot via ray cast with a vive tracker. Set position. Freeze height if evaluation phase. 
        RaycastHit hitLeft;
        if (!isHeightTranslationFrozen && Physics.Raycast(L_Tracked_Object.transform.position + bufferHeight, transform.TransformDirection(Vector3.down), out hitLeft, Mathf.Infinity, layerMask))
        {
            L_Foot_Model.transform.position = new Vector3(L_Tracked_Object.position.x, L_Tracked_Object.position.y, L_Tracked_Object.position.z) + L_Position_Displacement;
            Debug.DrawLine(L_Tracked_Object.transform.position + bufferHeight, hitLeft.point, Color.red);
        }
        else
        {
            L_Foot_Model.transform.position = new Vector3(L_Tracked_Object.position.x, frozenFloorHeight, L_Tracked_Object.position.z) + L_Position_Displacement;
        }
        //Detect ground beneth right foot via ray cast with a vive tracker. Set position. Freeze height if evaluation phase. 
        RaycastHit hitRight;
        if (!isHeightTranslationFrozen && Physics.Raycast(R_Tracked_Object.transform.position + bufferHeight, transform.TransformDirection(Vector3.down), out hitRight, Mathf.Infinity, layerMask))
        {
            R_Foot_Model.transform.position = new Vector3(R_Tracked_Object.position.x, R_Tracked_Object.position.y, R_Tracked_Object.position.z) + R_Position_Displacement;
            Debug.DrawLine(R_Tracked_Object.transform.position + bufferHeight, hitRight.point, Color.red);
        }
        else
        {
            R_Foot_Model.transform.position = new Vector3(R_Tracked_Object.position.x, frozenFloorHeight, R_Tracked_Object.position.z) + R_Position_Displacement;
        }

        // Set foot orientation 
        //L_Foot_Model.eulerAngles = new Vector3(L_Foot_Model.eulerAngles.x, L_Tracked_Object.eulerAngles.y, L_Foot_Model.eulerAngles.z) //+ L_Rotation_Displacement;
        //R_Foot_Model.eulerAngles = new Vector3(R_Foot_Model.eulerAngles.x, R_Tracked_Object.eulerAngles.y, R_Foot_Model.eulerAngles.z) //+ R_Rotation_Displacement;
        L_Foot_Model.eulerAngles = new Vector3(L_Tracked_Object.eulerAngles.x, L_Tracked_Object.eulerAngles.y, L_Tracked_Object.eulerAngles.z); //+ L_Rotation_Displacement;
        R_Foot_Model.eulerAngles = new Vector3(R_Tracked_Object.eulerAngles.x, R_Tracked_Object.eulerAngles.y, R_Tracked_Object.eulerAngles.z); //+ R_Rotation_Displacement;


        // finite state machine for climbing based on foot height
        // To prevent falling: only translate the player if the height of hit points beneath each foot is within an 0.5m difference         
        /*float L_Foot_Height = L_Foot_Model.transform.position.y;
        float R_Foot_Height = R_Foot_Model.transform.position.y;
        if (!isHeightTranslationFrozen && (Mathf.Abs(L_Foot_Height - R_Foot_Height) < noFallHeight))
        {
            if (L_Foot_Height < R_Foot_Height - epsilon) // if left foot is lower
            {
                transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, L_Foot_Height - trackerDisplacement.y, speed), 0);
            }
            else if (L_Foot_Height > R_Foot_Height + epsilon) // if right foot is lower
            {
                transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, R_Foot_Height - trackerDisplacement.y, speed), 0);
            }
            else // else
            {
                transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, L_Foot_Height - trackerDisplacement.y, speed), 0);
            }

        }*/


    }

    void Update()
    {


        // calibrate feet orientation / rotation 
        if (Input.GetKeyDown("c"))
        {
            Debug.Log("Calibrate feet!");
            CalibrateFeetRotation();
            SetTrackerDisplacement(); 
        }
    }

    // Called in ExperimentInput (on input "s" or "h")
    // "s" = show stairwell (exploration phase)
    // "h" = hide stairwell (evaluation phase)
    // Freeze height translation when experiment is ongoing to prevent tracking err
    public void Set_IsHeightTranslationFrozen(bool isFrozen)
    {
        isHeightTranslationFrozen = isFrozen;
        if (isFrozen)
        {
            int layerMask = 1 << 9;
            RaycastHit hitRight;
            // note: right foot is more stable bc of stairlayout and camera placement
            if (Physics.Raycast(R_Tracked_Object.transform.position + bufferHeight, transform.TransformDirection(Vector3.down), out hitRight, Mathf.Infinity, layerMask))
            {
                frozenFloorHeight = hitRight.point.y;
            }
        }
    }



    // participants must point both feet forward in the positive z direction
    public void CalibrateFeetRotation()
    {
        Quaternion rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        L_Foot_Model.GetChild(0).transform.rotation = rotation;
        R_Foot_Model.GetChild(0).transform.rotation = rotation;
    }

    void SetTrackerDisplacement()
    {
        L_Position_Displacement = new Vector3(0f, -(L_Tracked_Object.position.y-0.05f), 0f); // 0.05 small buffer offset so not in floor 
        R_Position_Displacement = new Vector3(0f, -(R_Tracked_Object.position.y-0.05f), 0f);

        L_Rotation_Displacement = new Vector3(-L_Tracked_Object.eulerAngles.x, -L_Tracked_Object.eulerAngles.y, -L_Tracked_Object.eulerAngles.z);
        R_Rotation_Displacement = new Vector3(-R_Tracked_Object.eulerAngles.x, -R_Tracked_Object.eulerAngles.y, -R_Tracked_Object.eulerAngles.z);


        //L_Foot_Model.eulerAngles = new Vector3(L_Foot_Model.eulerAngles.x, L_Tracked_Object.eulerAngles.y, L_Foot_Model.eulerAngles.z);
    }

}
