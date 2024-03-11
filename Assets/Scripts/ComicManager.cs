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

    void Start()
    {
        if(comic.panels.Count > 0)
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.LoadScene(SceneType.StartMenu);
        }
    }

    bool LoadPanel(int index)
    {
        if (index < comic.panels.Count)
        {
            var image = GetComponent<Image>();
            image.color = Color.white;
            image.sprite = comic.panels[index];
            onChangePanel.Invoke();
            return true;
        }
        else
        {
            onComicEnd.Invoke();
            return false;
        }
    }
}
