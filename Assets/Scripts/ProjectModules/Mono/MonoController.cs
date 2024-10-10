using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    private event UnityAction fixedUpdateEvent;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (fixedUpdateEvent != null)
        {
            fixedUpdateEvent.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null)
        {
            updateEvent.Invoke();
        }
    }

    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }

    public void AddFixedUpdateListener(UnityAction fun)
    {
        fixedUpdateEvent += fun;
    }

    public void RemoveFixedUpdateListener(UnityAction fun)
    {
        fixedUpdateEvent -= fun;
    }

}
