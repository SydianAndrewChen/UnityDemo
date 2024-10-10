using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class SceneMgr : BaseManager<SceneMgr>
{
    // 同步
    public void LoadScene(string sceneName, UnityAction action)
    {
        // 同步加载
        SceneManager.LoadScene(sceneName);

        action.Invoke();
    }

    // 异步
    public void LoadSceneAsync(string sceneName, UnityAction action = null)
    {
        // 异步加载
        MonoMgr.GetInstance().StartCoroutine(LoadSceneAsyncCore(sceneName, action));
    }

    private IEnumerator LoadSceneAsyncCore(string sceneName, UnityAction action)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        // 可以得到场景加载的一个进度
        while (!ao.isDone)
        {
            //这里面去更新进度条
            EventCenter.GetInstance().EventTrigger("进度条更新", ao.progress);
            yield return ao.progress;
        }

        if (action != null)
            action.Invoke();
    }
}
