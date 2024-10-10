using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceMgr : BaseManager<ResourceMgr>
{
    /// <summary>
    /// 使用一个字符串列表实现简单的加锁，避免异步操作重复加载相同资源
    /// </summary>
    private List<string> Loading = new List<string>();

    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        if (res is GameObject)
        {
            //如果是GameObject类型，直接实例化，再返回出去
            return GameObject.Instantiate(res);
        }
        else
        {
            // TextAsset、AudioClip等等类型
            return res;
        }
    }

    public void LoadAsync<T>(string name, UnityAction<T> callback, bool isUnique = true) where T : Object
    {
        if (isUnique)
        {
            if (!Loading.Contains(name)) 
            {
                //加锁
                Loading.Add(name);
                MonoMgr.GetInstance().StartCoroutine(LoadAsyncCore<T>(name, callback));
            }
        }
        else 
        {
            MonoMgr.GetInstance().StartCoroutine(LoadAsyncCore<T>(name, callback));
        }
    }

    //真正的异步加载函数
    IEnumerator LoadAsyncCore<T>(string name, UnityAction<T> callback) where T:Object 
    {
        ResourceRequest rr = Resources.LoadAsync<T>(name);
        yield return rr;

        if (rr.asset is GameObject)
        {
            callback(GameObject.Instantiate<T>(rr.asset as T));
        }
        else 
        {
            callback(rr.asset as T);
        }
        //解锁
        Loading.Remove(name);
    }
}
