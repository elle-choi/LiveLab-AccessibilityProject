using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureUse : MonoBehaviour
{
    // 1. KNUTH SHUFFLE CODE
    // for Object Spawning
    /*
    // Uses Knuth shuffle like everybody else *shrugs*
    // Pure random without constraints. Consecutive poles can appear next to each other

    // step 1: on top include
    // using System;


    // step 2: in Start ()
     /*
        RandomizedIndexList= new List<int>();

        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            RandomizedIndexList.Add(i);
        }
        RandomizeIndexList(); 
        */

    // step3: declare 
    // public List<int> RandomizedIndexList;


    // step 4: add the function "RandomizeIndexList()" function 
    /*
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
     */

    // 2. TIME STAMP CODE
    // making objects spawn every 3 seconds
    /*

    if (timePassed < timeToSpawn)
    {
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
