using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSwitcher : MonoBehaviour
{
    int currentModeIndex = -1;
    static ModeSettings[] modes;

    Timer fadeOut;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        text.enabled = true;

        modes ??= Resources.LoadAll<ModeSettings>("Settings/Game Modes");
        currentModeIndex = Array.IndexOf(modes, GlobalSettings.Get._current);
        // Change name to full caps
        text.text = modes[currentModeIndex].name.ToUpper();

        fadeOut = new(2);
        StartCoroutine(FadeOutRoutine());
    }

    void Update()
    {
        if (!Application.isEditor && Input.GetKeyDown(KeyCode.Tab) && Time.timeSinceLevelLoad > .5f)
        {
            LoadMode(currentModeIndex + 1);
        }
    }

    IEnumerator FadeOutRoutine()
    {
        var color = text.color;

        while (!fadeOut)
        {
            yield return null;
            color.a = fadeOut.Inverse;
            text.color = color;
        }
    }

    void LoadMode(int index)
    {
        currentModeIndex = index % modes.Length;

        GlobalSettings.Get._current = modes[currentModeIndex];

        SceneManager.LoadScene(0);
    }
}
