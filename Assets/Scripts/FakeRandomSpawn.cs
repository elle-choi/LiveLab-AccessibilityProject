using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 


public class FakeRandomSpawn : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    public List<int> RandomizedIndexList; 
    public List<Transform> location; // = List<Transform>();

    public bool isRandomized;
    private float timeToSpawn = 3f;
    public float timePassed = 0f;


    public void Start()
    { 
        RandomizedIndexList= new List<int>();

        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            RandomizedIndexList.Add(i);
        }
        RandomizeIndexList(); 
    }

    public void Update()
    {
        // making objects spawn every 3 seconds


        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomizeIndexList();
        }

        /*
        if (timePassed < timeToSpawn)
        {
            //Debug.Log("updated timer with " + Time.deltaTime);
            timePassed += Time.deltaTime;
        }
        else
        {
            Debug.Log("Spawn objects!");
            spawnObject();
            timePassed = 0f;
        }
        */
    }


   // Uses Knuth shuffle like everybody else *shrugs*
   // Pure random without constraints. Consecutive poles can appear next to each other
   void RandomizeIndexList()
    {
        Debug.Log("IndexArray Size: " + RandomizedIndexList.Count);
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());


        for (int i = 0; i < RandomizedIndexList.Count; i++)
        { 
            rnd = new System.Random(); 
            int temp = RandomizedIndexList[i];
            int rand_i = rnd.Next(i, RandomizedIndexList.Count);
            RandomizedIndexList[i] = RandomizedIndexList[rand_i];
            RandomizedIndexList[rand_i] = temp;
        }

        for (int i = 0; i < RandomizedIndexList.Count; i++)
        {
            Debug.Log(RandomizedIndexList[i]);
        }

        // Random rnd = new Random();

        //for (int j = 0; j < 4; j++)
       // {
       //     Console.WriteLine(rnd.Next());
       // }
    }

    /*
    public void spawnObject()
    {
        List<int> numList = new List<int>();        // our actual list we use
        List<int> randomNumList = new List<int>();  // a temporary list used for randomization!

        Debug.Log("Objects to Spawn Count: " + objectsToSpawn.Count);

        // Add index number values to our temporary array
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            randomNumList.Add(i);
            //randomNumList[i] = i;
        }

        Debug.Log("Random num list count: " + randomNumList.Count);

        while (randomNumList.Count != 0)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            int randomIndex = Random.Range(0, randomNumList.Count);

            numList.Add(randomNumList[randomIndex]);
            Debug.Log("Random Index: " + randomIndex);

            randomNumList.RemoveAt(randomIndex);
            Debug.Log("Temporary Array now size: " + randomNumList.Count);
        }

        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            Debug.Log(numList[i]);
        }


        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            int index = numList[i];
            Instantiate(objectsToSpawn[index], location[i].position, Quaternion.identity);
            Debug.Log("Object Instantiated");
        }
   
    }
    */


}