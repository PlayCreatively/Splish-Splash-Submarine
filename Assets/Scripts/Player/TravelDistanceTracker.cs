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
        GameState.Get.distanceTraveled += Time.deltaTime * GameState.Get.PlayerVerticalSpeed;

        // Check if level is complete
        if (GameState.Get.distanceTraveled >= GlobalSettings.Current.level.LevelLength)
        {
            GameState.Get.distanceTraveled = GlobalSettings.Current.level.LevelLength;
            GameState.Get.Level++;
            OnLevelComplete.Invoke();
            enabled = false;
        }
    }
}
