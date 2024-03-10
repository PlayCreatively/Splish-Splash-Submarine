using UnityEngine;

public class CanvasSpawner : MonoBehaviour
{
    [SerializeField] float spawnRadius;
    [SerializeField] float margin = 1;

    float likelyhoodSum;
    float unitsTraveled;
    float unitsPerSpawn;

    void Awake()
    {
        likelyhoodSum = GlobalSettings.Current.level.spawningSettings.GetLikelyhoodSum();
        unitsPerSpawn = (Camera.main.orthographicSize * 2) / GlobalSettings.Current.level.spawningSettings.spawnsPerScreenHeight;
        unitsTraveled = unitsPerSpawn;
    }

    void Update()
    {
        unitsTraveled += GameState.Get.PlayerVerticalSpeed * Time.deltaTime;
        int spawnCount = (int)(unitsTraveled / unitsPerSpawn);
        unitsTraveled -= spawnCount * unitsPerSpawn;

        for (int i = 0; i < spawnCount; i++)
            Spawn(GetRandomSpawn());
    }

    void Spawn(MovePattern movePattern)
    {
        for (int i = 2; i >= 0; i--)
        {
            float space = spawnRadius - margin * i;
            if(space >= movePattern.halfBounds)
            {
                i = Random.Range(0, i + 1);
                i = Random.Range(0, 2) == 0 ? i : -i;
                Instantiate(movePattern, transform.position + new Vector3(margin * i, 0), Quaternion.identity);
                return;
            }
        }

        Debug.LogError("No space to spawn enemy, increase spawnRadius or decrease margin.", movePattern);
    }

    public MovePattern GetRandomSpawn()
    {
        float choice = Random.Range(0, likelyhoodSum);

        foreach (SpawnItem spawn in GlobalSettings.Current.level.spawningSettings.spawns)
            if (choice < spawn.likelyhood)
                return spawn.prefab;
            else
                choice -= spawn.likelyhood;

        throw new System.Exception("Should not be able to reach this point, talk to Alex... not the Sasha one.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRadius * 2, 0));

        Vector3 linePos = transform.position;
        Gizmos.DrawRay(linePos, Vector3.down);

        for (int i = 1; i <= 2; i++)
        {
            linePos.x += margin;
            Gizmos.DrawRay(linePos, Vector3.down);
            linePos.x = -linePos.x;
            Gizmos.DrawRay(linePos, Vector3.down);
            linePos.x = -linePos.x;
        }
    }

}