using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSpawn : MonoBehaviour
{
    //private Collider spawnArea;

    public GameObject five_LeftUp;
    public GameObject seven_LeftUp;
    public GameObject nine_LeftUp;

    /*
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }


    private void OnEnable() // called when script is enabled
    {
        Debug.Log("Spawner Enabled");
        StartCoroutine(firstSpawn());
    }
    
    private void OnDisable()
    {
        Debug.Log("Spawner Disabled");
        StopAllCoroutines();
    }
    */

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(five_LeftUp, worldPosition, Quaternion.identity);
        }
    }


    /*
    private IEnumerator firstSpawn()
    {
        while (enabled)
        {
            Vector3 position1 = new Vector3();

            position1.x = -2.04f;
            position1.y = 1.329184f;
            position1.z = -0.632f;

            // Quaternion = Rotation
            Quaternion rotation1 = Quaternion.Euler(0f, 90f, 0f);

            Instantiate(five_LeftUp, position1, rotation1);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    */
}
