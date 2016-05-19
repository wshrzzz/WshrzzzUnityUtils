using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtils;

public class CheeterConsoleTest : MonoBehaviour
{
	// Use this for initialization
	void Start () {

        CheatInput.AddCheater("TestCheater", () => { Debug.Log("TestCheater is on. 1"); });

        CheatInput.AddCheater("TestCheater", () => { Debug.Log("TestCheater is on. 2"); });

        CheatInput.AddCheater("TestChea", () => { Debug.Log("TestChea is on."); });

        CheatInput.AddCheater("TestCheaterBIGBIG", () => { Debug.Log("TestCheaterBIGBIG is on."); });

        CheatInput.AddCheater("showmethemoney", () => { Debug.Log("showmethemoney is on."); });

        CheatInput.AddCheater("whosyourdaddy", WhosYourDaddy);
	}

    void WhosYourDaddy()
    {
        Debug.Log("whosyourdaddy is on.");
    }
}
