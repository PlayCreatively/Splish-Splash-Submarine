using UnityEngine;
using UnityEngine.UI;

public class CaptainManager : MonoBehaviour
{
    //SwitchText switchText;
    public Text captainText;

    public void Say(string line)
    {
        captainText.text = line;
    }
}