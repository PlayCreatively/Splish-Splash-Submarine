using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(LevelAsset), menuName = nameof(LevelAsset), order = 0)]
public class LevelAsset : ScriptableObject
{
    [SerializeField, Min(0)]
    float levelLengthInMinutes = 1;
    [HideInInspector]
    public float LevelLength;
    public SpawningSettings spawningSettings;

    private void OnEnable()
    {
        LevelLength = levelLengthInMinutes * 60f * GlobalSettings.Current.player.verticalSpeed * Time.timeScale;
    }
}

[Serializable]
public class SpawningSettings
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

[Serializable]
public struct SpawnItem
{
    public MovePattern prefab;
    [Min(0), Tooltip("Likelyhood of being spawned relative to all the other spawns in the list.")]
    public float likelyhood;
}
