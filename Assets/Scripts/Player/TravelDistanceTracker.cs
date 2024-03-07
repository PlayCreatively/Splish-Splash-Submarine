using UnityEngine;
using UnityEngine.Events;

public class TravelDistanceTracker : MonoBehaviour
{    
    public UnityEvent OnLevelComplete;

    void Start()
    {
        GameState.Get.distanceTraveled = 0;
    }

    void Update()
    {
        // Update distance traveled
        GameState.Get.distanceTraveled += Time.deltaTime * GameState.Get.playerVerticalSpeed;

        // Check if level is complete
        if (GameState.Get.distanceTraveled >= GlobalSettings.Current.LevelLength)
        {
            GameState.Get.distanceTraveled = GlobalSettings.Current.LevelLength;
            OnLevelComplete.Invoke();
            enabled = false;
        }
    }
}
