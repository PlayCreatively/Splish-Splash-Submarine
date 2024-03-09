using UnityEngine;
using UnityEngine.UI;

public class SwitchText : MonoBehaviour
{
    public string[] Lines;
    public void ChangeFromTo(Text text, string line)
    {
        text.text = line;
        Debug.Log("Changed to " + line);
    }
    public void ChangeFromToIf(Text text, string line, bool condition)
    {
        if (condition)
            ChangeFromTo(text, line);
    }
    public void ChangeTo(string text)
    {
        GetComponent<Text>().text = text;
        Debug.Log("Changed to " + text);
    }
    public void ChangeToIf(string text, bool condition)
    {
        if (condition)
            ChangeTo(text);
    }
    public void ChangeByIndex(int index)
    {
        GetComponent<Text>().text = Lines[index];
    }
    public void ChangeToNext()
    {
        ChangeByIndex(GetNextIndex());
    }
    public void ChangeToPrevious()
    {
        ChangeByIndex(GetPreviousIndex());
    }   
    int GetIndex()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            if (GetComponent<Text>().text == Lines[i])
                return i;
        }
        return -1;
    }
    int GetNextIndex()
    {
        return (GetIndex() + 1) % Lines.Length;
    }
    int GetPreviousIndex()
    {
        return (GetIndex() - 1) % Lines.Length;
    }
}
