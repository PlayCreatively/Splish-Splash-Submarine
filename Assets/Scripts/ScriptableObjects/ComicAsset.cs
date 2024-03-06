using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Comic", menuName = "Comic")]
public class ComicAsset : ScriptableObject
{
    public List<Sprite> panels;

    public static ComicAsset Load(int index) => Resources.Load<ComicAsset>($"Comics/Comic {index}");
}
