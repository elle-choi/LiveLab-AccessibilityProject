using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSpawn : MonoBehaviour
{
    public Transform MovingWallsParent; 
    public Transform StairSpawnParent;
    Stair PreviousStair;                // Stair struct instance to store useful information 
    Stair NextStair;
    public float YSpawnOffset; 
    public float ZSpawnOffset; 

    public List<GameObject> ObjectsToSpawn;  // permanent list of possible objects to spawn
    //public List<GameObject> SpawnedObjects;  //  another list that stores all INSTANTIATED objects

    //public List<Transform> Location; 

    //List<int> NumList = new List<int>();        // our actual list we use
    //List<int> RandomNumList = new List<int>();  // a temporary list used for randomization!

    //public bool isRandomized;
    bool isWalkingClockwise;                // flag to check if player is walking clockwise or counterclockwise in stairwell
    public int totalTrials = 6;        // Total number of trials to complete. Set value here or in inspector
    public int currentTrial = 0;       // The current trial number (count)
    public int[] StairConditionArray;

    // Previous stair information
        // direction: up or down     
        // length: 5,7,9
        // my spawn point (y) 
        // ??  

     class Stair
    {
        public int direction;           // 0 is down, 1 is up
        public int numberOfSteps;       // 5, 7, or 9 steps

        public float stairHeight;       
        public float stairLength;
        public float spawnPointY;       // y pos for next stair spawn
        public float stairHeightDifference; 


        public Stair(Transform myTransform, int myDirection, int nextDirection, int nextNumberOfSteps) 
        {
            this.direction = myDirection;

            Transform StepsParent = myTransform.GetChild(1);
            this.numberOfSteps = StepsParent.childCount - 2; // 2 children are platforms. the rest are steps
            //Debug.Log("prev num steps: " + numberOfSteps);
            // Get references from stairs parent 
            Transform spawnPointsParent = myTransform.GetChild(0);
            Vector3 spawnPoint1 = spawnPointsParent.GetChild(0).position;
            Vector3 spawnPoint2 = spawnPointsParent.GetChild(1).position;

            SetStairHeightAndLength(spawnPoint1, spawnPoint2);
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
            if (spawn_point_difference > 0) {             
                top_pos = spawnPoint2.y;        // then 1 is bottom and 2 is top   
                bot_pos = spawnPoint1.y;
            }
            else {   
                top_pos = spawnPoint1.y;        // then 2 is bottom and 1 is top
                bot_pos = spawnPoint2.y;
            }
           

            // Based on the current and next stair directions, determine they y offset needed for the next spawn point            
            int directionIndex = direction + nextDirection; // recall: direction ( 0 = down, 1 = up)
            Debug.Log("Direction Index: " + directionIndex);

            stairHeightDifference = 0f;
            if (nextNumberOfSteps > numberOfSteps)
            {
                stairHeightDifference = (nextNumberOfSteps - numberOfSteps) / 2 * 0.3f;
                Debug.Log("Prev step count: " + numberOfSteps + ". Next step count: " + nextNumberOfSteps + ". HeightOffset: " + stairHeightDifference);
            }
            else if (nextNumberOfSteps < numberOfSteps)
            {
                stairHeightDifference = (nextNumberOfSteps - numberOfSteps) / 2 * 0.3f; // HALEY MAKE THIS NEGATIVE TO FIX BACK
                Debug.Log("Prev step count: " + numberOfSteps + ". Next step count: " + nextNumberOfSteps + ". HeightOffset: " + stairHeightDifference);
            }

            /*if (nextNumberOfSteps > numberOfSteps || nextNumberOfSteps < numberOfSteps)
            {
                stairHeightDifference = (nextNumberOfSteps - numberOfSteps) / 2 * 0.3f;
                Debug.Log("Prev step count: " + numberOfSteps + ". Next step count: " + nextNumberOfSteps + ". HeightOffset: " + stairHeightDifference);
            }*/
            // - , +
            // - , +

            if (directionIndex == 0)
            {         // both down
                this.spawnPointY = bot_pos - stairHeight;
                if (nextNumberOfSteps > numberOfSteps || nextNumberOfSteps < numberOfSteps)
                    this.spawnPointY = bot_pos - (stairHeight + stairHeightDifference);
                
                /*
                if (nextNumberOfSteps > numberOfSteps)
                    this.spawnPointY = bot_pos - (stairHeight + stairHeightDifference); 
                else if (nextNumberOfSteps < numberOfSteps) 
                    this.spawnPointY = bot_pos - (stairHeight + stairHeightDifference);  
                */

                /*
                if (nextNumberOfSteps > numberOfSteps)
                    this.spawnPointY = bot_pos - (stairHeight + Mathf.Abs(stairHeightDifference));
                else if (nextNumberOfSteps < numberOfSteps) // || nextNumberOfSteps > numberOfSteps)
                    this.spawnPointY = bot_pos - (stairHeight - Mathf.Abs(stairHeightDifference));  //(stairHeight - Mathf.Abs(stairHeightDifference)); 
                */
            }
            else if (directionIndex == 1)       // one down; one up 
            {
                if (direction == 0 && nextDirection ==1) // prev=down and next=up
                    this.spawnPointY = bot_pos;
                else
                    this.spawnPointY = bot_pos - stairHeightDifference; // - og
            }
            else if (directionIndex == 2)       // both up
                this.spawnPointY = top_pos;           
        }

    } // END OF STAIR CLASS 

    public void Start()
    {
        currentTrial = 0;
        YSpawnOffset = 0f; 
        ZSpawnOffset = 0f;        
        isWalkingClockwise = false;
        //StairConditionArray = new int[] { 1,1,1,3,3,3,1,1,1 }; // temporary static array 
        //StairConditionArray = new int[] { 1,4,3,1,2,4,2,1,3,2,3,4,1 }; // temporary static array 
        StairConditionArray = new int[] {4,1,3,2,4,4,4,1,3,2,4,4,3,3,1 }; // temporary static array 
        //StairConditionArray = new int[] { 1,1,2,2,1,1,2,2,1 }; // temporary static array 


        //sRandomizeIntegerList(); // create a randomized list in the beginning
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            RandomizeIntegerList(); 
        }

        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            ClockwiseToggle();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SpawnNextStair();
            MoveWalls(); // HALEY: Temporary! This should move with player camera

            /*
            int index = numList[0];
            Instantiate(objectsToSpawn[index], location[index].position, Quaternion.identity);
            Debug.Log("Object #" + index + " Instantiated.");

            // FIXME? record already spawned stairs
            spawnedObjects.Add(objectsToSpawn[index]);

            numList.RemoveAt(0);*/
        }

    }

    // Where: 
    // 1 = 5-step up        // 3 = 7-step up
    // 2 = 5-step down      // 4 = 7 step down
    public void RandomizeIntegerList() {
        Debug.Log("Randomization not yet implemented.");
    }

    // Function to Spawn randomized stairs in a sequence 
    public void SpawnNextStair()
    {

        if (currentTrial == totalTrials - 1)
            return;

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
            case 1:
                // 5-step up
                stair_setting += "5 step-up - ";
                objectIndex = 0;
                numberOfSteps = 5;
                if (currentTrial % 2 != 0)
                {
                    stair_setting += " odd. flip y.";
                    spawn_orientation *= Quaternion.Euler(0, 180, 0); // y flip -180 point down
                }
                break;
            case 2:
                // 5-step down
                stair_setting += "5 step-down - ";
                objectIndex = 0;
                numberOfSteps = 5;
                if (currentTrial % 2 == 0)
                {
                    stair_setting += " even. flip y.";
                    spawn_orientation *= Quaternion.Euler(0, 180, 0); // y flip -180 point down
                }
                break;
            case 3:                     // HALEY: REDUNDANT CODE... Is it needed? (e.g., for wall adjustment maybe)
                // 7-step up
                stair_setting += "7 step-up - ";
                objectIndex = 1;
                numberOfSteps = 7;
                if (currentTrial % 2 != 0)
                {
                    stair_setting += " odd. flip y.";
                    spawn_orientation *= Quaternion.Euler(0, 180, 0); // y flip -180 point down
                }
                break;
            case 4:
                // 7-step down
                stair_setting += "7 step-down - ";
                objectIndex = 1;
                numberOfSteps = 7;
                if (currentTrial % 2 == 0)
                {
                    stair_setting += " even. flip y.";
                    spawn_orientation *= Quaternion.Euler(0, 180, 0); // y flip -180 point down
                }
                break;
            default:
                objectIndex = 0;
                spawn_position.y = 0f;
                numberOfSteps = 5;
                Debug.Log("Error?");
                break;
        }

        // X position = -1.1 or +1.1        // Current CLOCKWISE / COUNTERCLOCKWISE SOLUTION 
        spawn_position.x = 1.1f;
            if (currentTrial % 2 != 0)    // JUST FLIP PARENT INSTEAD 
            {
                spawn_position.x *= -1;  // move -x to spawn left when walking counter-clockwise 
            }


        // PREVIOUS STAIR MEASURES
        if (currentTrial != 0)
        {

            // count children if >2 then delete the first (oldest) child
            if (StairSpawnParent.childCount >= 2)
                DestroyImmediate(StairSpawnParent.GetChild(0).gameObject);


            // Get current (previous) stair transform and data
            ////////////////////////////////////////////////////////////////////////////////////////////
            Transform prev_stair = StairSpawnParent.GetChild(0).transform;
            // Did prev stair point up or down? Even StairCondition indexes go down.
            // ?: operator works like   "condition ? consequent : alternative" 
            int prev_direction = (StairConditionArray[currentTrial - 1] % 2 == 0) ? 0 : 1;
            int next_direction = (StairConditionArray[currentTrial] % 2 == 0) ? 0 : 1;
            PreviousStair = new Stair(prev_stair, prev_direction, next_direction, numberOfSteps);

            spawn_position.y = PreviousStair.spawnPointY;
            //YSpawnOffset += PreviousStair.stairHeightDifference;
            YSpawnOffset = PreviousStair.spawnPointY; 

            float temp = (numberOfSteps - PreviousStair.numberOfSteps) / 2 * 0.3f;
            if (currentTrial % 2 != 0)    
                ZSpawnOffset += temp;
            else
                ZSpawnOffset -= temp;

            //Debug.Log("Add to ZOffset: " +temp);
            spawn_position.z = ZSpawnOffset;

            
        }

        // Shift ZSpawnOffset for stair spawns. Each stair length (5,7,9 steps) has an 0.3m difference     
        //PreviousStair.numberOfSteps
        // no... for walls I really need the actual length of the stairs... 
        // numberOfSteps - PreviousStair.numberOfSteps; 



        Debug.Log(stair_setting);
        Instantiate(ObjectsToSpawn[objectIndex], spawn_position, spawn_orientation, StairSpawnParent);
        //PreviousStair
        //int direction;          // 0 is down, 1 is up
        //int numberOfSteps;      // 5, 7, or 9 steps
        //float spawnPoint;       // y pos for next stair spawn


        currentTrial++; 

    }

    // Move walls as the player traverses the stairs
    public void MoveWalls()
    {
        float ypos = StairSpawnParent.GetChild(StairSpawnParent.childCount - 1).position.y;
        MovingWallsParent.position = new Vector3(0f, ypos, 0f);
        //Debug.Log(MovingWallsParent.position.y);

    }

    public void ClockwiseToggle()
    {
        isWalkingClockwise = !isWalkingClockwise; 
        Debug.Log("Toggle Clockwise! Counterclockise = " + isWalkingClockwise);
        Debug.Log("NOTICE: Now steps down = steps up. And vice versa!! ");

        // Flip StairSpawnParent 180 degrees
        StairSpawnParent.rotation *= Quaternion.Euler(0, 180, 0);  

        // Slide Blocking Walls in the opposite X direction by multiplying by -1
        Transform BlockingWallsParent = MovingWallsParent.GetChild(0);
        foreach (Transform child in BlockingWallsParent)
        {
            child.position = new Vector3(child.position.x*-1f, child.position.y, child.position.z);
        }

    }

    /*
    public void RandomizeList()
    {
        // Add index number values to our temporary array
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            randomNumList.Add(i);
        }

        while (randomNumList.Count != 0)
        {
            //Random.InitState((int)System.DateTime.Now.Ticks);
            int randomIndex = Random.Range(0, randomNumList.Count);

            numList.Add(randomNumList[randomIndex]);
            randomNumList.RemoveAt(randomIndex);
        }

        // checking list output 
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            Debug.Log(numList[i]);
        }
    }


    public void SpawnAllObjects()
    {
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            int index = numList[i];
            Instantiate(objectsToSpawn[index], location[i].position, Quaternion.identity);
            // Have an empty gameobject (a parent) in your scene.
            // make the instantiated object a CHILD of that parent.
            ///// protip: You can use parentTransform.GetChild(0) to get the first child
            ///// and then kill the child whenever the user shouldn't see it 
        }
    }
    */

    /*

    // FIXME temporary pseudocode funcion 
    public void DeleteWhenStepped()
    {
        if (triggerStepStepped)
        {
            Destroy(spawnedObjecs[0]); // it will always have 1 in list bc they will always be destroyed on each step 
        }
    }
    */

}
