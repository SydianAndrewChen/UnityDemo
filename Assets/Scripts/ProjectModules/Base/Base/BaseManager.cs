using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T:new() // 泛型约束
{
    private static T instance;

    protected BaseManager() { }
    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
}

