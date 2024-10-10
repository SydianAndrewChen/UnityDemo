using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_UI_Layer
{
    Bottom,
    Mid,
    Top,
    System
}
public class UIMgr : BaseManager<UIMgr>
{

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform bottom;
    private Transform mid;
    private Transform top;
    private Transform system;

    public RectTransform canvas;

    public UIMgr()
    {
        GameObject  obj = ResourceMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);

        bottom = canvas.Find("Bottom");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");

        obj = ResourceMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }
    public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callBack = null) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName)) 
        {
            panelDic[panelName].ShowMe();
            if (callBack != null)
            {
                callBack(panelDic[panelName] as T);
            }
            return;
        }
        ResourceMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            if (panelDic.ContainsKey(panelName))
                return;
            Debug.Log(panelName);
            //作为Canvas子对象
            //并且设置相对位置
            Transform father = bottom;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
            }
            if (obj == null) 
            {
                Debug.LogError("Requested file name: Resources/UI/" + panelName + " does not exist!");
                return;
            }
            obj.name = panelName;

            //设置父对象、相对位置和大小
            obj.transform.SetParent(father);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale= Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            T panel = obj.GetComponent<T>();
            panel.ShowMe();
            if (callBack != null) 
            {
                callBack(panel);
            }
            panelDic.Add(panelName, panel);
        });
    }
    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch (layer) 
        {
            case E_UI_Layer.Bottom:
                return this.bottom;
            case E_UI_Layer.Mid:
                return this.mid;
            case E_UI_Layer.Top:
                return this.top;
            case E_UI_Layer.System:
                return this.system;
        }
        return null;
    }
    public T GetPanel<T>(string panelName) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        Debug.Log("Required panel " + panelName + " does not exist");
        return null;
    }


    public void HidePanel(string panelName)
    {
        Debug.Log(panelName + " to be hided!");

        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HideMe();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
        else 
        {
            Debug.LogWarning("Panel not exist");
        }
    }

    public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack) 
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = type;
        entry.callback.AddListener(callBack);
        trigger.triggers.Add(entry);
    }
}
