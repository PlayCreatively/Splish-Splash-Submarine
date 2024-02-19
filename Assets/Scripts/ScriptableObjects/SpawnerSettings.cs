using System.Collections.Generic;
using UnityEngine;

//TODO: make property that tells makes it not be able uneditable during runtime
[CreateAssetMenu(fileName = "New" + nameof(SpawnerSettings), menuName = Path + nameof(SpawnerSettings), order = 0)]
public class SpawnerSettings : SettingsBase<SpawnerSettings>
{
    [Min(0), Tooltip("How many to spawn per unit screen height traveled.\n\nIn other words, how many enemies on screen at once.")]
    public float spawnsPerScreenHeight = 4;
    public List<SpawnItem> spawns;

    public float GetLikelyhoodSum()
    {
        float sum = 0;
        foreach (SpawnItem spawn in spawns)
            sum += spawn.likelyhood;
        return sum;
    }
}
