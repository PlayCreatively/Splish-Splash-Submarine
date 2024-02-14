using UnityEngine;

public class CanvasSpawner : MonoBehaviour
{
    [SerializeField] float spawnWidth;

    void Update()
    {
        if (GlobalSettings.Current.spawnerSettings.TrySpawn(out var spawnItem))
            Instantiate(spawnItem.prefab, transform.position + new Vector3(Random.Range(-spawnWidth, spawnWidth), 0), Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnWidth * 2, 0));
    }

}