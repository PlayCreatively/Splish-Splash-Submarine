using UnityEngine;
using UnityEngine.Events;

public class ComicManager : MonoBehaviour
{
    public ComicAsset comic;

    public UnityEvent onComicEnd;
    public UnityEvent onChangePanel;

    int panelIndex = 0;

    void Start()
    {
        LoadPanel(panelIndex);
    }

    void Update()
    {
        // Switch panel
        if (Input.GetKeyDown(KeyCode.Space) && LoadPanel(++panelIndex) == false)
        {
            GameManager.LoadScene(SceneType.Game);
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
            GetComponent<SpriteRenderer>().sprite = comic.panels[index];
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
