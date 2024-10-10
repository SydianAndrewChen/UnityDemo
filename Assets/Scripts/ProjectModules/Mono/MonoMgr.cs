using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Internal;

public class MonoMgr : BaseManager<MonoMgr>
{
    private MonoController monoController;

    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoController");
        monoController = obj.AddComponent<MonoController>();
    }

    public void AddUpdateListener(UnityAction fun)
    {
        monoController.AddUpdateListener(fun);
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        monoController.RemoveUpdateListener(fun);
    }

    public void AddFixedUpdateListener(UnityAction fun)
    {
        monoController.AddFixedUpdateListener(fun);
    }

    public void RemoveFixedUpdateListener(UnityAction fun)
    {
        monoController.RemoveFixedUpdateListener(fun);
    }

    public Coroutine StartCoroutine(string methodName) 
    {
        return monoController.StartCoroutine(methodName);
    }
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return monoController.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value) 
    {
        return monoController.StartCoroutine(methodName, value);
    }

    public void StopAllCoroutines() 
    {
        monoController.StopAllCoroutines();
    }

    public void StopCoroutine(IEnumerator routine) 
    {
        monoController.StopCoroutine(routine);
    }

    public void StopCoroutine(Coroutine routine) 
    {
        monoController.StopCoroutine(routine);
    }
    
    public void StopCoroutine(string methodName) 
    {
        monoController.StopCoroutine(methodName);
    }
}
