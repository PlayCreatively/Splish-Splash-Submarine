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
    AudioSource audioSource;

    void Awake()
    {
        image = GetComponent<Image>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    void Start()
    {
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
            if(GameState.Get.Level < 4)
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
        comicManager.image.sprite = comicManager.comic.panels[0].sprite;
        comicManager.enabled = false;

        Color clearColor = new (1,1,1,0);
        comicManager.image.color = clearColor;

        AudioListener.volume = .5f;
        yield return comicManager.FadePanel(.1f, Color.white);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                if (comicManager.IsMorePanels())
                    yield return comicManager.LoadPanelRoutine(++comicManager.panelIndex);
                else
                    break;
            yield return null;
        }

        AudioListener.volume = .5f;
        yield return comicManager.FadePanel(.2f, clearColor);

        Destroy(comicManager.gameObject);
        playerShooterScript.enabled = true;
        Time.timeScale = GlobalSettings.Current.timeScale;
    }

    bool IsMorePanels() => panelIndex+1 < comic.panels.Count;

    bool LoadPanel(int index)
    {
        if (index < comic.panels.Count)
        {
            if(comic.panels[index].music != null)
            {
                audioSource.clip = comic.panels[index].music;
                audioSource.Play();
            }

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

    IEnumerator FadePanel(float time, bool fadeIn)
    {
        Timer fadeTimer = new(time, true);

        while (!fadeTimer)
        {
            image.color = Color.Lerp(Color.black, Color.white, fadeIn ? fadeTimer : fadeTimer.Inverse);
            audioSource.volume = fadeIn ? fadeTimer : fadeTimer.Inverse;
            yield return null;
        }
        image.color = fadeIn ? Color.white : Color.black;
        audioSource.volume = fadeIn ? 1 : 0;

    }

    IEnumerator LoadPanelRoutine(int index)
    {
        yield return FadePanel(.2f, false);
        image.sprite = comic.panels[index].sprite;
        yield return FadePanel(.2f, true);
    }
}
