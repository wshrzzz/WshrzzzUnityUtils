using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtils;

public class TweenBasicTest : MonoBehaviour
{
    public Transform testCube;
    public Transform testCube2;
    public Transform testCube3;

	// Use this for initialization
	void Start () {
        StartCoroutine(TestTween());
	}

    IEnumerator TestTween()
    {
        TweenBasic tweener = TweenBasic.InitTweener(testCube.gameObject);
        TweenBasic tweener2 = TweenBasic.InitTweener(testCube2.gameObject);
        TweenBasic tweener3 = TweenBasic.InitTweener(testCube3.gameObject);
        tweener.StartTween(TweenBasic.TweenType.EasyInOut, 5f, loopType: TweenBasic.LoopType.PingPong);
        tweener2.StartTween(TweenBasic.TweenType.EasyInOut, 5f, loopType: TweenBasic.LoopType.PingPong, tweenFactor: 2, forward: false);
        tweener3.StartTween(TweenBasic.TweenType.Linear, 5f, loopType: TweenBasic.LoopType.PingPong, tweenTimes: 4);
        do 
        {
            testCube.position = Vector3.Lerp(new Vector3(-6f, 0f, 0f), new Vector3(6f, 0f, 0f), tweener.TweenValue);
            testCube2.position = tweener2.SimpleLerp(new Vector3(-6f, -1.5f, 0f), new Vector3(6f, -1.5f, 0f));
            testCube3.position = tweener3.SimpleLerp(new Vector3(-6f, 1.5f, 0f), new Vector3(6f, 1.5f, 0f));
            yield return new WaitForEndOfFrame();
        } while (tweener.IsTweening);
        tweener.DeinitTweener();
        tweener2.DeinitTweener();
        tweener3.DeinitTweener();
    }
}
