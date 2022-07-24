using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSpawn : MonoBehaviour
{

    public GameObject myPrefab;
    public Transform location1;
    public Transform location2;
    public Transform location3;

    public float timeToSpawn;

    private float currentTimeToSpawn;


    private void Start()
    {
        /*
        Instantiate(myPrefab, location1.position, Quaternion.identity);
        Debug.Log("First Object Instantiated");

        Instantiate(myPrefab, location2.position, Quaternion.identity);
        Debug.Log("Second Object Instantiated");

        Instantiate(myPrefab, location3.position, Quaternion.Euler(new Vector3(-90, 0, 180)));
        Debug.Log("Third Object Instantiated");
        */
    }

    public void Update()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = timeToSpawn; 
        }
    }

    public void SpawnObject()
    {
        Instantiate(myPrefab, location1.position, Quaternion.identity);
        Debug.Log("First Object Instantiated");

        Instantiate(myPrefab, location2.position, Quaternion.identity);
        Debug.Log("Second Object Instantiated");

        Instantiate(myPrefab, location3.position, Quaternion.Euler(new Vector3(-90, 0, 180)));
        Debug.Log("Third Object Instantiated");
    }

    /*

    public void Update()
    {
        // FIXME (why is Get Key Down not working) 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Second Object Instantiated");
            Instantiate(myPrefab, location2.position, Quaternion.identity);
        }
    }
    */

    // FIXME THIS CODE COULD BE USED FOR SPAWNING WHEN STEPPED ON
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // probably won't be 2D
        // and the compare tag would be called sth else like 3rd step
        if (other.gameObject.CompareTag("Player"))
        {
            spawner.SpawnObject();
        }
    }
    */
}
