using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0;

    void Update()
    {
        GetComponent<Text>().text = ((int)score).ToString();
        score += Time.deltaTime;
    }
}
