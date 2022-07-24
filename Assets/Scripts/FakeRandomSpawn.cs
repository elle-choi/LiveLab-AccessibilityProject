using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeRandomSpawn : MonoBehaviour
{
    public List<GameObject> objectsToSpawn = new List<GameObject>();
    public List<Transform> location = new List<Transform>();

    /*
    public Transform location1;
    public Transform location2;
    public Transform location3;
    */

    public bool isRandomized;
    private float currentTimeToSpawn;
    private float timeToSpawn = 3;

    public void Start()
    {
        //currentTimeToSpawn = timeToSpawn; 
    }

    //FIXME time script not working!
    public void Update()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            spawnObject();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    public void spawnObject()
    {
        // right now it's randomizing the index number and is creating 3 at the same time
        int index = isRandomized ? Random.Range(0, objectsToSpawn.Count) : 0;


        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            Instantiate(objectsToSpawn[index], location[i].position, Quaternion.identity);
            Debug.Log(" Object Instantiated");
        }

    }
}
