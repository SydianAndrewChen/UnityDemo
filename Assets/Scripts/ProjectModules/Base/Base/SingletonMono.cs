using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        return instance;
    }

    protected void Awake()
    {
        instance = this as T;
        // 继承时记住调用base.Awake()
    }
}
