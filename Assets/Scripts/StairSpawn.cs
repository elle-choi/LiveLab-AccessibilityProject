using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSpawn : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;  // permanent list of possible objects to spawn
    public List<GameObject> spawnedObjects;  //  another list that stores all INSTANTIATED objects

    public List<Transform> location; 

    List<int> numList = new List<int>();        // our actual list we use
    List<int> randomNumList = new List<int>();  // a temporary list used for randomization!

    public bool isRandomized;


    public void Start()
    {
        RandomizeList(); // create a randomized list in the beginning
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int index = numList[0];

            Instantiate(objectsToSpawn[index], location[index].position, Quaternion.identity);
            Debug.Log("Object #" + index + " Instantiated.");

            // FIXME? record already spawned stairs
            spawnedObjects.Add(objectsToSpawn[index]);

            numList.RemoveAt(0);
        }
    }

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

    // FIXME temporary pseudocode funcion 
    public void DeleteWhenStepped()
    {
        if (triggerStepStepped)
        {
            Destroy(spawnedObjecs[0]); // it will always have 1 in list bc they will always be destroyed on each step 
        }
    }

}
