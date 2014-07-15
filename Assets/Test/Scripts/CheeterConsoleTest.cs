using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtil;

public class CheeterConsoleTest : MonoBehaviour
{
	// Use this for initialization
	void Start () {

        CheeterConsole.AddCheeter("TestCheeter", () => { Debug.Log("TestCheeter is on. 1"); });

        CheeterConsole.AddCheeter("TestCheeter", () => { Debug.Log("TestCheeter is on. 2"); });

        CheeterConsole.AddCheeter("TestChee", () => { Debug.Log("TestChee is on."); });

        CheeterConsole.AddCheeter("TestCheeterBIGBIG", () => { Debug.Log("TestCheeterBIGBIG is on."); });

        CheeterConsole.AddCheeter("showmethemoney", () => { Debug.Log("showmethemoney is on."); });

        CheeterConsole.AddCheeter("whosyourdaddy", WhosYourDaddy);

        CheeterConsole.AddCheeter("RemoveTest", () => { 
            CheeterConsole.RemoveCheeter("TestCheeter"); 
            CheeterConsole.RemoveCheeter("RemoveTest");
        });
	}

    void WhosYourDaddy()
    {
        Debug.Log("whosyourdaddy is on.");
    }
}
