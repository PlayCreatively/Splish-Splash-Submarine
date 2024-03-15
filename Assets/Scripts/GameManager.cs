using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : ScriptableSingleton<GameManager>
{
    public static void LoadLevel(int index)
    {
        GameState.Get.Level = index;
        StartCoroutine(LoadSceneRoutine(1));
    }

    public static void LoadScene(int index)
    {
        StartCoroutine(LoadSceneRoutine(index));
    }

    /// <summary>
    /// Increments the level and loads the next comic
    /// </summary>
    public static void CompleteLevel()
    {
        GameState.Get.Level++;
        StartCoroutine(LoadSceneRoutine(2));
    }
    
    public static void LoadScene(SceneType type)
    {
        LoadScene((int)type);
    }

    public static void RestartScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    static Coroutine StartCoroutine(IEnumerator routine) => GameState.Get.StartCoroutine(routine);

    static IEnumerator LoadSceneRoutine(int index)
    {
        yield return Fade(DEFAULT_FADE_TIME, false);
        SceneManager.LoadScene(index);
    }

    public static IEnumerator Fade(float time, bool fadeIn)
    {
        var fadeImage = GetImage();
        fadeImage.raycastTarget = false;
        Timer fadeTimer = new (time, true);

        while (!fadeTimer)
        {
            // fade audio level
            AudioListener.volume = fadeIn ? fadeTimer : fadeTimer.Inverse;
            fadeImage.color = new Color(0, 0, 0, fadeIn ? fadeTimer.Inverse : fadeTimer);
            yield return null;
        }
        AudioListener.volume = fadeIn ? 1 : 0;
        fadeImage.color = new Color(0, 0, 0, fadeIn ? 0 : 1);
    }

    public static void SmallShakeCamera()
    {
        Camera.main.GetComponent<Shaker>().StartShakeRoutine(.2f, 10);
    }
    public static void ShakeCamera()
    {
        Camera.main.GetComponent<Shaker>().StartShakeRoutine(.2f, 25);
    }

    static Image GetImage()
    {
        var canvas = FindAnyObjectByType<Canvas>();
        var imageObj = canvas.transform.Find("Fade");
        Image img;
        if (imageObj && (img = imageObj.GetComponent<Image>()))
            return img;
        else
        {
            var fadeObj = new GameObject("Fade");
            fadeObj.transform.SetParent(canvas.transform, false);
            img = fadeObj.AddComponent<Image>();
            // make it fill the screen
            img.rectTransform.anchorMin = Vector2.zero;
            img.rectTransform.anchorMax = Vector2.one;
            img.rectTransform.sizeDelta = Vector2.zero;
            img.color = Color.black;

            return img;
        }
    }

    public const float DEFAULT_FADE_TIME = .1f;
    public static void FadeOut() => GameState.Get.StartCoroutine(Fade(DEFAULT_FADE_TIME, false));
    public static void FadeIn() => GameState.Get.StartCoroutine(Fade(DEFAULT_FADE_TIME, true));

    public static void Quit()
    {
        Application.Quit();
    }





#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Find/" + nameof(GameManager))]
    public static new void CreateAndShow()
    {
        ScriptableSingleton<GameManager>.CreateAndShow();
    }
#endif
}