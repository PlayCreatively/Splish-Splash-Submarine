using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ComicManager : MonoBehaviour
{
    public ComicAsset comic;

    public UnityEvent onComicEnd;
    public UnityEvent onChangePanel;

    int panelIndex = 0;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();

        if (comic.panels.Count > 0)
            LoadPanel(panelIndex);
        else
            GameManager.LoadScene(SceneType.Game);
    }

    void Update()
    {
        // Switch panel
        if (Input.GetKeyDown(KeyCode.Space) && LoadPanel(++panelIndex) == false)
        {
            if(GameState.Get.Level < 3)
                GameManager.LoadScene(SceneType.Game);
            else
                GameManager.LoadScene(SceneType.StartMenu);
        }
    }



    public static void InstantiateComicModal(int comicIndex)
    {
        GameState.Get.StartCoroutine(ComicModalRoutine(comicIndex));
    }

    static IEnumerator ComicModalRoutine(int comicIndex)
    {
        Time.timeScale = 0;
        var playerShooterScript =
        GlobalSettings.Current.player.Ref.GetComponent<Shooter>();
        playerShooterScript.enabled = false;

        var comicManager = Resources.Load<ComicManager>("Comics/Comic");
        comicManager = Instantiate(comicManager);
        comicManager.transform.SetParent(FindAnyObjectByType<Canvas>().transform, false);
        comicManager.comic = ComicAsset.Load(comicIndex);
        comicManager.enabled = false;
        comicManager.Start();

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                if (comicManager.IsMorePanels())
                    yield return comicManager.LoadPanelRoutine(++comicManager.panelIndex);
                else
                    break;
                yield return null;
        }

        Destroy(comicManager.gameObject);
        playerShooterScript.enabled = true;
        Time.timeScale = GlobalSettings.Current.timeScale;
    }

    bool IsMorePanels() => panelIndex+1 < comic.panels.Count;

    bool LoadPanel(int index)
    {
        if (index < comic.panels.Count)
        {
            StartCoroutine(LoadPanelRoutine(index));
            onChangePanel.Invoke();
            return true;
        }
        else
        {
            onComicEnd.Invoke();
            return false;
        }
    }

    IEnumerator FadePanel(float time, Color color)
    {
        Timer fadeTimer = new(time, true);
        var originalColor = image.color;

        while (!fadeTimer)
        {
            image.color = Color.Lerp(originalColor, color, fadeTimer);
            yield return null;
        }
        image.color = color;
    }

    IEnumerator LoadPanelRoutine(int index)
    {
        yield return FadePanel(.2f, Color.black);
        image.sprite = comic.panels[index];
        yield return FadePanel(.2f, Color.white);
    }
}
