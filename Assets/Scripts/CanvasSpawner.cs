using UnityEngine;

public class CanvasSpawner : MonoBehaviour
{
    [SerializeField] float spawnWidth;

    float likelyhoodSum;
    float unitsTraveled;
    float unitsPerSpawn;

    void Awake()
    {
        likelyhoodSum = GlobalSettings.Current.spawnerSettings.GetLikelyhoodSum();
        unitsPerSpawn = (Camera.main.orthographicSize * 2) / GlobalSettings.Current.spawnerSettings.spawnsPerScreenHeight;
        unitsTraveled = unitsPerSpawn;
    }

    void Update()
    {
        unitsTraveled += GlobalSettings.Current.player.curVerticalSpeed * Time.deltaTime;
        int spawnCount = (int)(unitsTraveled / unitsPerSpawn);
        unitsTraveled -= spawnCount * unitsPerSpawn;

        for (int i = 0; i < spawnCount; i++)
            Instantiate(GetRandomSpawn().prefab, transform.position + new Vector3(Random.Range(-spawnWidth, spawnWidth), 0), Quaternion.identity);
    }

    public SpawnItem GetRandomSpawn()
    {
        float choice = Random.Range(0, likelyhoodSum);

        foreach (SpawnItem spawn in GlobalSettings.Current.spawnerSettings.spawns)
            if (choice < spawn.likelyhood)
                return spawn;
            else
                choice -= spawn.likelyhood;

        throw new System.Exception("Should not be able to reach this point, talk to Alex... not the Sasha one.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnWidth * 2, 0));
    }

}