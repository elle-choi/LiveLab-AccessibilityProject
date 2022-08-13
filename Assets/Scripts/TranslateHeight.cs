using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateHeight : MonoBehaviour
{

    private float noFallHeight;     // height: within which translation will occur
    private float frozenFloorHeight;
    private bool isHeightTranslationFrozen;
    private float speed;            // speed: to smooth lerp of player translation up and down
    public float FloorHeight;
    Vector3 bufferHeight;
    Camera m_MainCamera;

    // Check to prevent falling off stairwell 
    public bool isSafetyOn = true;
    float maxFallDistance; 

    // Start is called before the first frame update
    void Start()
    {
        // Set constraints for vertical translation
        isHeightTranslationFrozen = false;
        frozenFloorHeight = 0f;
        noFallHeight = 0.5f;
        speed = 0.05f;
        FloorHeight = 0f;
        bufferHeight = new Vector3(0, 1, 0);
        m_MainCamera = Camera.main;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Bit shift the index of layer (9) "Ground" to get a bit mask
        // Allows cast rays only agains colliders in layer 9
        int layerMask = 1 << 9;


        // Detect ground beneth player. Set position. Freeze height if evaluation phase. 
        // Use the camera's position. But point down from the player transform (since the player transform never rotates)
        RaycastHit hit;
        if (!isHeightTranslationFrozen
            && Physics.Raycast(m_MainCamera.transform.position, transform.TransformDirection(Vector3.down),
            out hit,
            Mathf.Infinity,
            layerMask))
        {
            // if saftey mode is on  (true), then prevent falling
            // else the user can fall!! 
            if (isSafetyOn == true) 
                maxFallDistance = 1.0f; 
            else 
                maxFallDistance = 999999f;           

            // To prevent falling: only translate the player if the height of hit points beneath is < 2m      
            if (Mathf.Abs(transform.position.y - hit.point.y) < maxFallDistance)
            {
                FloorHeight = hit.point.y;
            }
           Debug.DrawLine(m_MainCamera.transform.position, hit.point, Color.red);
            
        }
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, FloorHeight, speed), transform.position.z);
        //transform.position = new Vector3(transform.position.x, FloorHeight, transform.position.z);

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
            if (Physics.Raycast(m_MainCamera.transform.position, transform.TransformDirection(Vector3.down), out hitRight, Mathf.Infinity, layerMask))
            {
                frozenFloorHeight = hitRight.point.y;
            }
        }
    }

}
