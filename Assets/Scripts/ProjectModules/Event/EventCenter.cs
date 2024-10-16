﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{ 
    
}
public class EventInfo<T>: IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action) 
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    //添加事件监听
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            Debug.Log("Event Added: " + name);
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            Debug.Log("Event Created: " + name);
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }

    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name)) 
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }

    //事件触发
    public void EventTrigger<T>(string name, T info) 
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo<T>).actions != null)
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
        }
    }

    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            Debug.Log("Event Added: " + name);
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            Debug.Log("Event Created: " + name);
            eventDic.Add(name, new EventInfo(action));
        }
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }

    //事件触发
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
                (eventDic[name] as EventInfo).actions.Invoke();
        }
    }

    public void Clear() 
    {
        eventDic.Clear();
    }

}
