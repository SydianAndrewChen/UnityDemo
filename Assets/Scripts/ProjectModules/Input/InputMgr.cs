using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseManager<InputMgr>
{
    private bool isStart = false;
    public InputMgr()
    {
        //通过公共Mono集中进行更新
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
        MonoMgr.GetInstance().AddFixedUpdateListener(MyFixedUpdate);
    }

    //是否开启或关闭输入检测
    public void StartOrEndCheck(bool flag)
    {
        isStart = flag;
    }

    private void UpdateKeyCode(KeyCode key) 
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventCenter.GetInstance().EventTrigger("Key_W_Pressed_Down_Update", KeyCode.W);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            EventCenter.GetInstance().EventTrigger("Key_W_Pressed_Up_Update", KeyCode.W);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            EventCenter.GetInstance().EventTrigger("Key_Escape_Pressed_Down_Update", KeyCode.Escape);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventCenter.GetInstance().EventTrigger("Key_Space_Pressed_Down_Update", KeyCode.Space);
        }

    }

    private void FixedUpdateKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventCenter.GetInstance().EventTrigger("Key_W_Pressed_Down_FixedUpdate", KeyCode.W);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            EventCenter.GetInstance().EventTrigger("Key_W_Pressed_Up_FixedUpdate", KeyCode.W);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            EventCenter.GetInstance().EventTrigger<KeyCode>("Key_Escape_Pressed_Up_FixedUpdate", KeyCode.Escape);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventCenter.GetInstance().EventTrigger<KeyCode>("Key_Space_Pressed_Down_FixedUpdate", KeyCode.Space);
        }

    }

    private void MyUpdate()
    {
        if (!isStart)
            return;
        UpdateKeyCode(KeyCode.W);
        UpdateKeyCode(KeyCode.A);
        UpdateKeyCode(KeyCode.S);
        UpdateKeyCode(KeyCode.D);
        UpdateKeyCode(KeyCode.Escape);
        UpdateKeyCode(KeyCode.Space);
    }
    private void MyFixedUpdate()
    {
        if (!isStart)
            return;
        FixedUpdateKeyCode(KeyCode.W);
        FixedUpdateKeyCode(KeyCode.A);
        FixedUpdateKeyCode(KeyCode.S);
        FixedUpdateKeyCode(KeyCode.D);
        FixedUpdateKeyCode(KeyCode.Escape);
        FixedUpdateKeyCode(KeyCode.Space);
    }
}
