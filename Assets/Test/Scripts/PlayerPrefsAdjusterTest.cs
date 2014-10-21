using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtil;

public class PlayerPrefsAdjusterTest : MonoBehaviour
{

    private int TestPrefsUpDown { get; set; }
    private int TestPrefsLeftRight { get; set; }

    void Start()
    {
        TestPrefsUpDown = PlayerPrefs.GetInt("TestPrefsUpDown", 5);
        TestPrefsLeftRight = PlayerPrefs.GetInt("TestPrefsLeftRight", 10);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TestPrefsUpDown++;
            PlayerPrefsAdjuster.ChangeSetting("TestPrefsUpDown", TestPrefsUpDown, "Description:" + TestPrefsUpDown);
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TestPrefsUpDown--;
            PlayerPrefsAdjuster.ChangeSetting("TestPrefsUpDown", TestPrefsUpDown, "Description:" + TestPrefsUpDown);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TestPrefsLeftRight--;
            PlayerPrefsAdjuster.ChangeSetting("TestPrefsLeftRight", TestPrefsLeftRight, null);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TestPrefsLeftRight++;
            PlayerPrefsAdjuster.ChangeSetting("TestPrefsLeftRight", TestPrefsLeftRight, null);
        }
	}
}
