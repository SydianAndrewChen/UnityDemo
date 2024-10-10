using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneMgr : MonoBehaviour
{
    // Start is called before the first frame update
    const string BGMname = "BackgroundMusic";
    void Start()
    {
        //UIMgr.GetInstance().ShowPanel<StartGamePanel>("StartGamePanel");
        AudioMgr.GetInstance().PlayBGM(BGMname);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
