using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(SpawnItem), menuName = Path + nameof(SpawnItem), order = 0)]
public class SpawnItem : SettingsBase<SpawnItem>
{
    public GameObject prefab;
    [Min(0), Tooltip("Likelyhood of being spawned relative to all the other spawns in the list.")]
    public float likelyhood;
}
