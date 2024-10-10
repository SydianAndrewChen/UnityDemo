using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolData 
{
    public GameObject fatherObj;
    public List<GameObject> poolList;
    public GameObject srcObj;

    public PoolData(GameObject obj, GameObject poolObj) 
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>() { };
        srcObj = obj;
        srcObj.transform.parent = fatherObj.transform;
        PushObj(srcObj);
    }

    public GameObject GetObj() 
    {
        GameObject obj = null;
        if (poolList.Count > 0)
        {
            obj = poolList[0];
            poolList.RemoveAt(0);
        }
        else 
        {
            obj = GameObject.Instantiate(srcObj);
            obj.name = srcObj.name;
        }
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }

    public void PushObj(GameObject obj)
    {
        poolList.Add(obj);
        obj.transform.parent = fatherObj.transform;
        obj.SetActive(false);
    }
}
public class PoolMgr : BaseManager<PoolMgr>
{
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    private GameObject poolObj;
    public void GetObj(string name, UnityAction<GameObject> callBack, Transform transform = null)
    {
        if (!poolDic.ContainsKey(name))
        {
            ResourceMgr.GetInstance().LoadAsync<GameObject>(name, (obj) =>
            {
                obj.name = name;
                callBack(obj);
            }, transform);
        }
        else 
        {
            callBack(poolDic[name].GetObj());
        }
    }

    public void PushObj(string name, GameObject obj) 
    {
        if (poolObj == null)
            poolObj = new GameObject("Pool");


        obj.SetActive(false);
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        else
        {
            poolDic.Add(name, new PoolData(obj, poolObj));
        }
        poolDic[name].fatherObj.transform.parent = poolObj.transform;
    }

    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
