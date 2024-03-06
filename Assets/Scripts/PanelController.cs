using UnityEngine;

public class PanelController : MonoBehaviour
{
    public void OpenPanel(GameObject panel)
    {
        this.gameObject.SetActive(false);
        panel.SetActive(true);
    }
    public void OpenNextPanelInHierarchy()
    {
        this.gameObject.SetActive(false);
        this.transform.parent.GetChild(this.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
    }
}
