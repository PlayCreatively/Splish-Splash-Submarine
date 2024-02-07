using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: make property that tells makes it not be able uneditable during runtime
[CreateAssetMenu(fileName = "New" + nameof(SpawnerSettings), menuName = Path + nameof(SpawnerSettings), order = 0)]
public class SpawnerSettings : SettingsBase<SpawnerSettings>
{
    [Min(0), Tooltip("Spawn per second")]
    public float spawnFrequency;
    public List<SpawnItem> spawns;

    float likelyhoodSum;
    Timer timer;

    void OnEnable()
    {
        timer = new Timer(spawnFrequency);

        likelyhoodSum = 0;
        foreach (SpawnItem spawn in spawns)
            if(spawn != null)
                likelyhoodSum += spawn.likelyhood;

        SceneManager.sceneLoaded += (scene, mode) => timer.Restart();
    }

    public bool TrySpawn(out SpawnItem spawnItem)
    {
        if (timer)
        {
            timer.Offset(spawnFrequency);

            float choice = Random.Range(0, likelyhoodSum);

            foreach (SpawnItem spawn in spawns)
                if( choice < spawn.likelyhood)
                {
                    spawnItem = spawn;
                    return true;
                }
                else
                    choice -= spawn.likelyhood;

            Debug.LogError("Should not be able to reach this point, talk to Alex... not the Sasha one.");
        }

        spawnItem = null;
        return false;
    }
}
