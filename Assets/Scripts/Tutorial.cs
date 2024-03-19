using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    CaptainManager captainManager;
    Transform spawnPoint;
    [SerializeField]
    List<Sprite> tutorialSprites;

    IEnumerator Start()
    {
        spawnPoint = GameObject.Find("Spawner").transform;
        spawnPoint.gameObject.SetActive(false);

        captainManager = FindAnyObjectByType<CaptainManager>();
        captainManager.captainSpeech.transform.parent.gameObject.SetActive(true);

        // Showcase movemement //
        captainManager.Say(tutorialSprites[0]);

        static bool isMoving() => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
                                  Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow);
        while (!isMoving())
            yield return null;

        yield return new WaitForSeconds(3);
        
        // Showcase shooting //
        captainManager.Say(tutorialSprites[1]);
        
        static bool isShooting() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ||
                                    Input.GetKeyDown(KeyCode.Space);
        while (!isShooting())
            yield return null;

        yield return new WaitForSeconds(2);

        // spawn enemies section //
        captainManager.Say(tutorialSprites[2]);
        SpawningSettings spawningSettings = GlobalSettings.Current.level.spawningSettings;
        foreach (var spawn in spawningSettings.spawns)
        {
            yield return SpawnEnemyRoutine(spawn.prefab.gameObject);
        }

        yield return new WaitForSeconds(4);

        GameState.Get.Level++;
        GameManager.RestartScene();
    }

    IEnumerator SpawnEnemyRoutine(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        var visual = enemy.transform.GetChild(0);

        // while enemy not dead
        while(visual != null)
            yield return null;        
    }
}
