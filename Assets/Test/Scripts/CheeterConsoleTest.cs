using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtil;

public class CheeterConsoleTest : MonoBehaviour
{
	// Use this for initialization
	void Start () {

        CheeterConsole.AddCheeter("TestCheeter");
        CheeterConsole.AddCheeter("showmethemoney");
        CheeterConsole.AddCheeter("whosyourdaddy");

        CheeterConsole.GetInstance().OnCheet += (cheeterName) =>
        {
            switch (cheeterName)
            {
                case "TestCheeter":
                    CheeterConsole.RemoveCheeter("TestCheeter");
                    break;
                case "showmethemoney":
                    CheeterConsole.RemoveCheeter("showmethemoney");
                    break;
                case "whosyourdaddy":
                    CheeterConsole.RemoveCheeter("whosyourdaddy");
                    break;
                default:
                    break;
            }
        };
	}
}
