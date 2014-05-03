using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtil;

public class TweenBasicTest : MonoBehaviour
{
    public Transform testCube;
    public Transform testCube2;

	// Use this for initialization
	void Start () {
        StartCoroutine(TestTween());
	}

    IEnumerator TestTween()
    {
        TweenBasic tweener = TweenBasic.InitTweener(testCube.gameObject);
        TweenBasic tweener2 = TweenBasic.InitTweener(testCube2.gameObject);
        tweener.StartTween(TweenBasic.TweenType.EasyInOut, 5f, loopType: TweenBasic.LoopType.PingPong);
        tweener2.StartTween(TweenBasic.TweenType.EasyInOut, 5f, loopType: TweenBasic.LoopType.PingPong, tweenFactor: 2, forward: false);
        do 
        {
            testCube.position = Vector3.Lerp(new Vector3(-6f, 1.5f, 0f), new Vector3(6f, 1.5f, 0f), tweener.TweenValue);
            testCube2.position = tweener2.SimpleLerp(new Vector3(-6f, -1.5f, 0f), new Vector3(6f, -1.5f, 0f));
            yield return new WaitForEndOfFrame();
        } while (tweener.IsTweening);
        tweener.DeinitTweener();
        tweener2.DeinitTweener();
    }
}
