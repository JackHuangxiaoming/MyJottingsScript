using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 自定义延时类
/// </summary>
public class DelayToInvoke
{
    public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        action();
    }
    //使用方法
    //void OnClick()
    //{
    //    StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
    //    {
    //        SceneManager.LoadScene("ToSceneName");
    //    }, 0.1f));
    //}
}
