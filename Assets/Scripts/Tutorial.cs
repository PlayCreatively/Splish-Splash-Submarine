using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    CaptainManager captainManager;
    Transform spawnPoint;

    IEnumerator Start()
    {
        spawnPoint = GameObject.Find("Spawner").transform;
        spawnPoint.gameObject.SetActive(false);

        captainManager = FindAnyObjectByType<CaptainManager>();
        captainManager.captainText.transform.parent.gameObject.SetActive(true);
        
        // Showcase movemement //
        captainManager.Say("Move with left and right arrow keys");

        static bool isMoving() => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
                                  Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow);
        while (!isMoving())
            yield return null;

        yield return new WaitForSeconds(3);
        
        // Showcase shooting //
        captainManager.Say("Shoot with spacebar or up arrow");
        
        static bool isShooting() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ||
                                    Input.GetKeyDown(KeyCode.Space);
        while (!isShooting())
            yield return null;

        yield return new WaitForSeconds(2);

        // spawn enemies section //
        captainManager.Say("Enemies...");
        SpawningSettings spawningSettings = GlobalSettings.Current.level.spawningSettings;
        foreach (var spawn in spawningSettings.spawns)
        {
            yield return SpawnEnemyRoutine(spawn.prefab.gameObject);
        }

        yield return new WaitForSeconds(4);

        GameState.Get.level++;
        GameManager.RestartScene();
    }

    IEnumerator SpawnEnemyRoutine(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        
        // while enemy not dead
        while(enemy != null)
            yield return null;        
    }
}
