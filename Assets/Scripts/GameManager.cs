using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

/// <summary>
/// This class was just made to assign the game speed on scene load. 
/// This may be for more general usecase later on or moved to another script.
/// </summary>
public class GameManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = GlobalSettings.Current.timeScale;
    }
}
