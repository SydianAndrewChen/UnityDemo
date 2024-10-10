using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Scrollbar>();
        FindChildrenControl<Slider>();
        FindChildrenControl<InputField>();
    }

    public virtual void ShowMe() { }

    public virtual void HideMe() { }

    protected virtual void OnClick(string btnName) { }

    protected virtual void OnValueChanged(string tglName, bool flag) { }
    protected virtual void OnValueChanged(string tglName, float value) { }

    protected T GetControl<T>(string controlName) where T:UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            foreach (UIBehaviour child in controlDic[controlName]) 
            {
                if (child is T)
                    return child as T;
            }
        }
        Debug.Log("Required Control Invalid!");
        return null;
    }

    private void FindChildrenControl<T>() where T: UIBehaviour
    {
        T[] children = GetComponentsInChildren<T>();
        foreach (T child in children) 
        {
            string objName = child.gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(child);
            }
            else 
            {
                controlDic.Add(child.gameObject.name, new List<UIBehaviour>{ child });
            }

            if (child is Button)
            {
                (child as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }
            else if (child is Toggle)
            {
                (child as Toggle).onValueChanged.AddListener((flag) =>
                {
                    OnValueChanged(objName, flag);
                });
            }
            else if (child is Slider)
            {
                (child as Slider).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
        }
    }
}
