using UnityEngine;
using UnityEngine.UI;

public class CaptainManager : MonoBehaviour
{
    //SwitchText switchText;
    public Text captainText;

    void Start()
    {
        //switchText = captainText.gameObject.GetComponent<SwitchText>();
    }

    public void Say(string line)
    {
        captainText.text = line;
        Debug.Log("Changed to " + line);
    }
}