using UnityEngine;
using UnityEngine.Events;

public class TravelDistanceTracker : MonoBehaviour
{    
    public UnityEvent OnLevelComplete;

    void Start()
    {
        GameState.Get.distanceTraveled = 0;

        // is tutorial
        if(GameState.Get.Level == 0)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Update distance traveled
        GameState.Get.distanceTraveled += Time.deltaTime * GameState.Get.PlayerVerticalSpeed;

        // Check if level is complete
        if (GameState.Get.distanceTraveled >= GlobalSettings.Current.level.LevelLength)
        {
            GameState.Get.distanceTraveled = GlobalSettings.Current.level.LevelLength;
            GameState.Get.OnLevelComplete?.Invoke();
            FindAnyObjectByType<CanvasSpawner>().gameObject.SetActive(false);
            OnLevelComplete.Invoke();
            enabled = false;
        }
    }
}
