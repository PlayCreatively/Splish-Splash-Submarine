using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
        GetComponent<Text>().text += " LEVEL " + GameState.Get.level;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = GlobalSettings.Current.timeScale;
            GameManager.RestartScene();
        }
    }
}
