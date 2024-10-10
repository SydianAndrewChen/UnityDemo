using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string SettingPanelinGameName = "SettingPanelinGame";
    const string TankName = "Tank";
    [SerializeField]
    private Transform birthPoint;
    private int group;
    private int pos;
    // Start is called before the first frame update
    void Start()
    {
        UIMgr.GetInstance().ShowPanel<BasePanel>(SettingPanelinGameName, E_UI_Layer.Mid, (panel) =>
        {
            panel.gameObject.SetActive(false);
            EventCenter.GetInstance().AddEventListener<KeyCode>("Key_Escape_Pressed_Down_Update", (key) =>
            {
                panel.gameObject.SetActive(true);
            });
            InputMgr.GetInstance().StartOrEndCheck(true);
        });

        
        ResourceMgr.GetInstance().LoadAsync<GameObject>(TankName, (obj) => 
        {
            //group = Random.Range(0, 2);
            //pos = Random.Range(0, 3);
            string birthPointName = string.Format("BirthPointTesting");
            Debug.Log(birthPointName + group + pos);
            birthPoint = transform.Find(birthPointName);
            obj.transform.position = birthPoint.position;
            obj.transform.eulerAngles = birthPoint.eulerAngles;
            EventCenter.GetInstance().EventTrigger<GameObject>("TankLoadComplete", obj);
        });

        AudioMgr.GetInstance().PlayBGM("BackgroundMusic");

    }

    // Update is called once per frame
    void Update()
    {
    }
}
