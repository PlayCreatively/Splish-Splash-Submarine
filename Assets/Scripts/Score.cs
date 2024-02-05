using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float score = 0;
    // Update is called once per frame
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Score: " + ((int)score).ToString();
        score += Time.deltaTime * 10;
    }
}
