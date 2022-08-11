using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrialManager : MonoBehaviour
{
    public Transform WallsParent;           // Walls parent needed to move dynamically in scene
    public GameObject InitialPlatform;       // Hide once cue_trial = 2
    Stair PreviousStair;                    // Stair struct instances to store useful information 
    Stair NextStair;                            
    public float YSpawnOffset;              // Can remove later if want..
    public float ZSpawnOffset;              // Keep this

    public List<GameObject> ObjectsToSpawn; // Permanent list of possible objects to spawn
    bool isWalkingClockwise;                // Flag to check if player is walking clockwise or counterclockwise in stairwell
    public int totalTrials = 6;             // Total number of trials to complete. Set value here or in inspector
    public int currentTrial = 0;            // The current trial number (count)
    public int[] StairConditionArray;            // Array of conditions possible values: 1,2,3,4,5,6

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    class Stair
    {
        public int direction;           // 0 is down, 1 is up
        public int numberOfSteps;       // 5, 7, or 9 steps

        public Vector3 spawnPoint1;
        public Vector3 spawnPoint2;
        public float spawnPointY;       // y pos for next stair spawn

        public float stairHeight;
        public float stairLength;
        public float stairHeightDifference;

        public Stair(Transform myTransform, int myDirection, int nextDirection, int nextNumberOfSteps)
        {
            this.direction = myDirection;

            Transform StepsParent = myTransform.GetChild(1);
            this.numberOfSteps = StepsParent.childCount - 2; // 2 children are platforms. the rest are steps            
            // Get references from stairs parent 
            Transform spawnPointsParent = myTransform.GetChild(0);
            this.spawnPoint1 = spawnPointsParent.GetChild(0).position;
            this.spawnPoint2 = spawnPointsParent.GetChild(1).position;

            SetStairHeightAndLength(spawnPoint1, spawnPoint2);
            if (nextNumberOfSteps != 0) // if this is zero, then there is no next step yet. 
                SetSpawnPointY(spawnPoint1, spawnPoint2, nextDirection, nextNumberOfSteps);
        }

        public void SetStairHeightAndLength(Vector3 spawnPoint1, Vector3 spawnPoint2)
        {
            this.stairHeight = Mathf.Abs(spawnPoint2.y - spawnPoint1.y);
            this.stairLength = Mathf.Abs(spawnPoint2.z - spawnPoint1.z);
        }

        // Spawn the current stair relative to the spawn points of the previous stair.
        // Spawn points are at the top and bottom of each stair. 
        // positions for the current stair are set relative to its BOTTOM (which explains the offsets).
        void SetSpawnPointY(Vector3 spawnPoint1, Vector3 spawnPoint2, int nextDirection, int nextNumberOfSteps)
        {
            // take difference in height (y) between the two spawn points to determin which is the top and which is the bottom spawn point
            float spawn_point_difference = spawnPoint2.y - spawnPoint1.y;
            float top_pos, bot_pos;

            // Figure out which spawn point it the lowest
            if (spawn_point_difference > 0)
            {
                top_pos = spawnPoint2.y;        // then 1 is bottom and 2 is top   
                bot_pos = spawnPoint1.y;
            }
            else
            {
                top_pos = spawnPoint1.y;        // then 2 is bottom and 1 is top
                bot_pos = spawnPoint2.y;
            }

            // Get difference in height between prev and cur stair (required to position SpawnPointY
            stairHeightDifference = 0f;
            if (nextNumberOfSteps > numberOfSteps)
            {
                stairHeightDifference = (nextNumberOfSteps - numberOfSteps) / 2 * 0.3f;  // 0.3f = the height of 2 steps 
                //Debug.Log("Prev step count: " + numberOfSteps + ". Next step count: " + nextNumberOfSteps + ". HeightOffset: " + stairHeightDifference);
            }
            else if (nextNumberOfSteps < numberOfSteps)
            {
                stairHeightDifference = (nextNumberOfSteps - numberOfSteps) / 2 * 0.3f; 
                //Debug.Log("Prev step count: " + numberOfSteps + ". Next step count: " + nextNumberOfSteps + ". HeightOffset: " + stairHeightDifference);
            }


            // Update SpawnPointY based on prev and cur stair direction
            int directionIndex = direction + nextDirection;             // recall: direction ( 0 = down, 1 = up)
            //Debug.Log("Direction Index: " + directionIndex);

            if (directionIndex == 0)                                    // both down
            {         
                this.spawnPointY = bot_pos - stairHeight;
                if (nextNumberOfSteps > numberOfSteps || nextNumberOfSteps < numberOfSteps)
                    this.spawnPointY = bot_pos - (stairHeight + stairHeightDifference);

            }
            else if (directionIndex == 1)                               // one down; one up 
            {
                if (direction == 0 && nextDirection == 1)               // prev=down and next=up
                    this.spawnPointY = bot_pos;
                else
                    this.spawnPointY = bot_pos - stairHeightDifference; // - og
            }
            else if (directionIndex == 2)                               // both up
                this.spawnPointY = top_pos;
        }

    } // END OF STAIR CLASS 
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Start()
    {
        currentTrial = 0;
        YSpawnOffset = 0f;
        ZSpawnOffset = 0f;
        isWalkingClockwise = false;

        StairConditionArray = new int[] {1,2,3,4,5,6};   // stair condition array 
        RandomizeConditionArray();                  // randomized stair condition array
    }


    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SpawnNextStair();
            ScaleWalls();
            MoveWalls();
        }

    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Function to Spawn randomized stairs in a sequence 
    public void SpawnNextStair()
    {

        if (currentTrial == totalTrials - 1)
            return;

        GameObject spawnedStair; // temporary reference to new stair GO

        // Determine position of spawned item
        Vector3 spawn_position = new Vector3(1.1f, 0f, 0f);
        Quaternion spawn_orientation = Quaternion.identity;
        string stair_setting = "Trial [" + currentTrial + "] ";


        ///////////////////////////////////////////////////////////

        // Y position = same postiion as y position of prev spawn point
        // if NO prev stair, then spawn at 0
        // else if prev stair was going up, then grab the top spawn point. 
        // else if prev stair was going down, then grab the bottom spawn point.
        int objectIndex;
        int numberOfSteps;
        switch (StairConditionArray[currentTrial])
        {
            case 1: // case 2:
                stair_setting += "5 step-up - ";
                objectIndex = 0;
                numberOfSteps = 5;
                break;
            case 2:
                stair_setting += "5 step-down - ";
                objectIndex = 0;
                numberOfSteps = 5;
                break;
            case 3: // case 4:                    // HALEY: REDUNDANT CODE... Is it needed? (e.g., for wall adjustment maybe)
                stair_setting += "7 step-up - ";
                objectIndex = 1;
                numberOfSteps = 7;
                break;
            case 4:
                stair_setting += "7 step-down - ";
                objectIndex = 1;
                numberOfSteps = 7;
                break;
            case 5: // case 6:                    
                stair_setting += "9 step-up - ";
                objectIndex = 2;
                numberOfSteps = 9;
                break;
            case 6:
                stair_setting += "9 step-down - ";
                objectIndex = 2;
                numberOfSteps = 9;
                break;
            default:
                objectIndex = 0;
                spawn_position.y = 0f;
                numberOfSteps = 10;
                Debug.Log("Error?");
                break;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //  Set New Stair Spawn Orientation 
        ///////////////////////////////////////////////////////////////////////////////////////                
        // Rotate (Y) new stairs to continue stairwell circle 
        // Flip stairs by 180 degrees on even trials if steps go down
        if (currentTrial % 2 == 0 && StairConditionArray[currentTrial] % 2 == 0)
            spawn_orientation *= Quaternion.Euler(0, 180, 0);
        // Flip stairs by 180 degrees on odd trials if steps go up
        else if (currentTrial % 2 != 0 && StairConditionArray[currentTrial] % 2 != 0)
            spawn_orientation *= Quaternion.Euler(0, 180, 0);


        ///////////////////////////////////////////////////////////////////////////////////////
        //  Set New Stair Spawn Position 
        ///////////////////////////////////////////////////////////////////////////////////////       
        // Shift (X) position of new stairs to opposite side of stairwell. The size of the area between each stair pair is 1.1m        
        spawn_position.x = 1.1f;
        if (currentTrial % 2 != 0)    // JUST FLIP PARENT INSTEAD IF WANT CLOCKWISE/COUNTERCLOCKWISE 
        {
            spawn_position.x *= -1;  // move -x to spawn left when walking counter-clockwise 
        }

        // If this is not the first trial, then calculate Y and Z posiion offsets for the new stair
        // based on the previous stair's position, length, height, etc. 
        int prev_direction, next_direction;

        if (currentTrial != 0)
        {
            InitialPlatform.SetActive(false); // called multiple times (unnecessary) but that's fine. Remove initial platform after 1st trial completed

            // We only want 2 stairs to be in the scenet at a time. Count the current number of stair spawns.
            // Destroy the oldest child when >=2!
            if (transform.childCount >= 2)
                DestroyImmediate(transform.GetChild(0).gameObject);

            // Get the previous stair transform and relevant information. Use Stair Class'
            // to calculate spawn point information, stair height, stair length, etc. 
            Transform prev_stair = transform.GetChild(0).transform;
            prev_direction = (StairConditionArray[currentTrial - 1] % 2 == 0) ? 0 : 1;              // ?: operator works like   "condition ? consequent : alternative" 
            next_direction = (StairConditionArray[currentTrial] % 2 == 0) ? 0 : 1;
            PreviousStair = new Stair(prev_stair, prev_direction, next_direction, numberOfSteps);

            // Get current and cumulative Y Offset
            YSpawnOffset = PreviousStair.spawnPointY;           //  HALEY: Keep this. Use it to move walls later.. maybe
            spawn_position.y = YSpawnOffset;

            // Get current and cumulative Z Offset
            float currentZOffset = (numberOfSteps - PreviousStair.numberOfSteps) / 2 * 0.3f;
            if (currentTrial % 2 != 0)
                ZSpawnOffset += currentZOffset;
            else
                ZSpawnOffset -= currentZOffset;
            spawn_position.z = ZSpawnOffset;

            //Debug.Log("NumSteps: [" + PreviousStair.numberOfSteps + ", " + numberOfSteps + "] .. ZOffset: " + temp);
            spawnedStair = Instantiate(ObjectsToSpawn[objectIndex], spawn_position, spawn_orientation, transform);
        }
        else
        {
            spawnedStair = Instantiate(ObjectsToSpawn[objectIndex], spawn_position, spawn_orientation, transform);

            Transform prev_stair = transform.GetChild(0).transform;
            prev_direction = (StairConditionArray[0] % 2 == 0) ? 0 : 1;              // ?: operator works like   "condition ? consequent : alternative" 
            next_direction = (StairConditionArray[1] % 2 == 0) ? 0 : 1;
            PreviousStair = new Stair(prev_stair, prev_direction, next_direction, numberOfSteps);
        }

        NextStair = new Stair(spawnedStair.transform, next_direction, 0, 0);

        // Finally, we spawn the stair and increment the trial count
        Debug.Log(stair_setting);
        currentTrial++;

    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Not in use at present.. 
    public void ClockwiseToggle()
    {
        isWalkingClockwise = !isWalkingClockwise;
        Debug.Log("Toggle Clockwise! Counterclockise = " + isWalkingClockwise);
        Debug.Log("NOTICE: Now steps down = steps up. And vice versa!! ");

        // Flip transform 180 degrees
        transform.rotation *= Quaternion.Euler(0, 180, 0);

        // Slide Blocking Walls in the opposite X direction by multiplying by -1
        Transform BlockingWallsParent = WallsParent.GetChild(0);
        foreach (Transform child in BlockingWallsParent)
        {
            child.position = new Vector3(child.position.x * -1f, child.position.y, child.position.z);
        }

    }



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Warp walls to match stairwell dimensions. Discerte update when each stair is updated. 
    public void ScaleWalls()
    {
        // Update the scale of the outer walls that cusp the stair steps (X +,-) 
        Transform XWallsParent = WallsParent.GetChild(0);       // Name: Walls X
        Vector3 XScale1 = XWallsParent.GetChild(0).localScale;  // X+ 
        Vector3 XScale2 = XWallsParent.GetChild(1).localScale;  // X-

        float scaleFactor = NextStair.stairLength * 0.1f;
        XWallsParent.GetChild(0).localScale = new Vector3(scaleFactor, XScale1.y, XScale1.z);    // Z+ 
        XWallsParent.GetChild(1).localScale = new Vector3(scaleFactor, XScale2.y, XScale2.z);   // Z-

        // Update the Z scale of the inner walls (a large cube object) to cover the inside of the stairwell even when stair length varies
        Transform InnerWalls = WallsParent.GetChild(2);         // Name: Inner Walls
        scaleFactor = NextStair.stairLength - 2.4f;             // each plaform in the stairwell has a width of 1.2
        InnerWalls.localScale = new Vector3(InnerWalls.localScale.x, InnerWalls.localScale.y, scaleFactor);
    }

    // Move walls as the player traverses the stairs and as the stairs spawn. Continuous update.
    public void MoveWalls()
    {
        // Offset Walls parent Y and Z to compensate for stair spawning (up/down = Y , lateral drift = Z) 
        float ypos = transform.GetChild(transform.childCount - 1).position.y;
        WallsParent.position = new Vector3(0f, ypos, ZSpawnOffset);     // HALEY: MAY  NEED TO MOVE THIS ELSWHERE FOR VR

        // Ofset position of near and far Z walls to compensate for variable wall lengths
        // Update the position of the near and far outer walls (Z +,-) based on current stair length 
        Transform ZWallsParent = WallsParent.GetChild(1); // Name: Walls Z
        ZWallsParent.GetChild(0).localPosition = new Vector3(0f, 0f, NextStair.stairLength / 2f);    // Z+ 
        ZWallsParent.GetChild(1).localPosition = new Vector3(0f, 0f, -NextStair.stairLength / 2f);   // Z-
    }




    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Randomize Stair Condition Array 
    // Need to have all unique trials present on each side of the stairwell ("balanced")s
    // So we randomize 
    // Fisher–Yates Shuffle (aka Knuth Shuffle) to randomize finite list https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle    
    public void RandomizeConditionArray()
    {                
        for (int t = 0; t < StairConditionArray.Length; t++)
        {
            int tmp = StairConditionArray[t];
            int r = Random.Range(t, StairConditionArray.Length);
            StairConditionArray[t] = StairConditionArray[r];
            StairConditionArray[r] = tmp;
        }

        // Randomize again if step counts are not balanced 
        if (!IsStepCountBalanced())
            RandomizeConditionArray(); 
    }

    bool IsStepCountBalanced()
    {
        List<int> EvenArray = new List<int>();

        foreach (int i in StairConditionArray) {
            if (i % 2 == 0)
                EvenArray.Add(i); 
        }

        if (EvenArray.Contains(1) && EvenArray.Contains(2))
            return false;
        else if (EvenArray.Contains(3) && EvenArray.Contains(4))
            return false;
        else if (EvenArray.Contains(5) && EvenArray.Contains(6))
            return false; 
        else
            return true; 
    }


}
