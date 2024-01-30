using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject objectToBeSpawned;
    private GameObject spawnedObject;
    public Vector2 bounds;
    public int numberOfObjects = 1;

    // Spawn frequency in seconds
    public float spawnFrequency = 1f; 

    IEnumerator Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float x = Random.Range(-bounds.x, bounds.x);
            float y = Random.Range(-bounds.y, bounds.y);
            Vector3 randomPosition = new Vector3(x, y, transform.position.z);

            // Pauses the execution of the function for a given amount of seconds
            yield return new WaitForSeconds(spawnFrequency);

            spawnedObject = Instantiate(objectToBeSpawned, randomPosition, transform.rotation);
        }
    }
}