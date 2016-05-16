using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtils;

public class ConsoleBasicTest : MonoBehaviour {

    // Use this for initialization
    void Start() {
        ConsoleBasic.AddCommand("debug", (list) =>
        {
            foreach (var item in list)
            {
                Debug.Log(item);
            }
        });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
