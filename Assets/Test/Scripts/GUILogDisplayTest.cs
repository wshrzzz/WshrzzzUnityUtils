﻿using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtil;

///////////////////////////////////////////////////////
/*  
 * This test request add GUILogDisplay to the scene by yourself.
*/
///////////////////////////////////////////////////////

public class GUILogDisplayTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TestCount = 0;
        StartCoroutine(KeepPrintLog());
	}

    private int TestCount { get; set; }

    IEnumerator KeepPrintLog()
    {
        while (true)
        {
            GUILogDisplay.PrintLog("Log : " + TestCount++);
            GUILogDisplay.PrintLog(Vector3.one);
            GUILogDisplay.PrintLog(this);
            yield return new WaitForSeconds(1f);
        }
    }
}
