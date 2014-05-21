using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtil;

public class TimerBasicTest : MonoBehaviour {

    TimerBasic timer1;
    TimerBasic timer2;
    TimerBasic timer3;

	// Use this for initialization
	void Start () {
        timer1 = TimerBasic.InitTimer(gameObject);
        timer2 = TimerBasic.InitTimer(gameObject);
        timer3 = TimerBasic.InitTimer(gameObject);

        timer1.StartCount(stopInTheEnd: false);
        timer2.StartCount(time: 3f);
        timer3.StartCount(stopInTheEnd: false);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Timer1 : " + timer1.CountTime);
        Debug.Log("Timer2 : " + timer2.CountTime);
        Debug.Log("Timer3 : " + timer3.CountTime);

        if (!timer2.IsCounting)
        {
            timer2.StartCount(time: 3f);
            if (timer3.IsCounting)
            {
                timer3.StopCount();
            }
            else
            {
                timer3.StartCount(reset: false);
            }
        }
	}
}
