using System;
using UnityEngine;

[Serializable]
public struct SpawnItem
{
    public MovePattern prefab;
    [Min(0), Tooltip("Likelyhood of being spawned relative to all the other spawns in the list.")]
    public float likelyhood;
}
