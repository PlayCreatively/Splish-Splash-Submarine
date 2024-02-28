using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0;

    void Update()
    {
        this.GetComponent<Text>().text = ((int)score).ToString();
        score += Time.deltaTime;
    }
}
